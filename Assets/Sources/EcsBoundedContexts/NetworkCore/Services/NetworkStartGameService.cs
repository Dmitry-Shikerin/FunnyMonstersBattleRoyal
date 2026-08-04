using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Menu;
using Fusion.Photon.Realtime;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class NetworkStartGameService
    {
        private string _region;
        private bool _connectingSafeCheck;
        private NetworkRunner _runner;
        private string _sessionName;
        private int _maxPlayerCount;
        private CancellationTokenSource _tokenSource;
        private string _appVersion;
        private FusionMenuConfig _config;

        public event Action<FusionMenuConnectArgs> OnBeforeConnect;

        public async UniTask StartSimulationAsync(GameMode gameMode, string sessionName)
        {
            NetworkRunner runner = NetworkRunnerProvider.Runner;
            runner.ProvideInput = true;

            await runner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                SceneManager = runner.SceneManager,
                Scene = SceneRef.FromIndex(1),
                SessionName = sessionName,
            });
        }

        private SceneRef GetScene()
        {
            SceneRef sceneRef = SceneRef.FromIndex(1);
            NetworkSceneInfo sceneInfo = new NetworkSceneInfo();
            sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Additive);
            return sceneRef;
        }

        public async UniTask<ConnectResult> ConnectAsync(FusionMenuConnectArgs connectionArgs, FusionMenuConfig config, string sceneName)
        {
            _config = config;
            
            if (OnBeforeConnect != null)
            {
                try
                {
                    OnBeforeConnect.Invoke(connectionArgs);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);

                    return new ConnectResult()
                    {
                        FailReason = ConnectFailReason.Disconnect,
                        DebugMessage = e.Message
                    };
                }
            }

            return await ConnectAsyncInternal(connectionArgs, sceneName);
        }

        private async UniTask<ConnectResult> ConnectAsyncInternal(FusionMenuConnectArgs connectArgs, string sceneName)
        {
            // Safety
            if (_connectingSafeCheck)
            {
                return new ConnectResult
                {
                    CustomResultHandling = true,
                    Success = false,
                    FailReason = ConnectFailReason.None,
                };
            }

            _connectingSafeCheck = true;

            if (_runner && _runner.IsRunning)
                await _runner.Shutdown();

            // Create and prepare Runner object
            _runner = NetworkRunnerProvider.Runner;
            NetworkSceneManagerDefault sceneManager = NetworkRunnerProvider.SceneManagerDefault;
            NetworkRunnerProvider.SceneManagerDefault.IsSceneTakeOverEnabled = false;

            // Copy and update AppSettings
            FusionAppSettings appSettings = CopyAppSettings(connectArgs);

            // Solve StartGameArgs
            StartGameArgs startGameArgs = new StartGameArgs();
            startGameArgs.CustomPhotonAppSettings = appSettings;
            //TODO потом придумать что то
            //startGameArgs.GameMode = connectArgs.GameMode ?? ResolveGameMode(connectArgs);
            startGameArgs.GameMode = GameMode.AutoHostOrClient;
            startGameArgs.SessionName = _sessionName = connectArgs.Session;
            Debug.Log($"Session Name {_sessionName}");
            startGameArgs.PlayerCount = _maxPlayerCount = connectArgs.MaxPlayerCount;

            // Scene info
            NetworkSceneInfo sceneInfo = new NetworkSceneInfo();
            //sceneInfo.AddSceneRef(sceneManager.GetSceneRef(connectArgs.Scene.ScenePath), LoadSceneMode.Additive);
            sceneInfo.AddSceneRef(GetSceneRef(sceneName));
            startGameArgs.Scene = sceneInfo;

            // Cancellation Token
            _tokenSource?.Dispose();
            _tokenSource = new CancellationTokenSource();
            startGameArgs.StartGameCancellationToken = _tokenSource.Token;

            int regionIndex = _config.AvailableRegions.IndexOf(connectArgs.Region);
            startGameArgs.SessionNameGenerator = () =>
                _config.CodeGenerator.EncodeRegion(_config.CodeGenerator.Create(), regionIndex);
            StartGameResult startGameResult = default(StartGameResult);
            ConnectResult connectResult = new ConnectResult();
            startGameResult = await _runner.StartGame(startGameArgs);

            connectResult.Success = startGameResult.Ok;
            connectResult.FailReason = ResolveConnectFailReason(startGameResult.ShutdownReason);
            _connectingSafeCheck = false;

            if (connectResult.Success)
                _sessionName = _runner.SessionInfo.Name;

            return connectResult;
        }

        private SceneRef GetSceneRef(string sceneName)
        {
            int index = sceneName switch
            {
                IdsConst.MainMenu => 0,
                IdsConst.Lobby => 1,
                IdsConst.Gameplay => 2,
                _ => throw new InvalidOperationException("Not enough scene name")
            };
            
            return SceneRef.FromIndex(index);
        }

        private FusionAppSettings CopyAppSettings(FusionMenuConnectArgs connectArgs)
        {
            FusionAppSettings appSettings = new FusionAppSettings();
            PhotonAppSettings.Global.AppSettings.CopyTo(appSettings);
            appSettings.FixedRegion = _region = connectArgs.Region;
            appSettings.AppVersion = _appVersion = connectArgs.AppVersion;

            return appSettings;
        }

        private GameMode ResolveGameMode(FusionMenuConnectArgs args)
        {
            bool isSharedSession = args.Scene.SceneName.Contains("Shared");

            if (args.Creating)
            {
                // Create session
                return isSharedSession ? GameMode.Shared : GameMode.Host;
            }

            if (string.IsNullOrEmpty(args.Session))
            {
                // QuickJoin
                return isSharedSession ? GameMode.Shared : GameMode.AutoHostOrClient;
            }

            // Join session
            return isSharedSession ? GameMode.Shared : GameMode.Client;
        }

        private int ResolveConnectFailReason(ShutdownReason reason)
        {
            return reason switch
            {
                ShutdownReason.Ok => ConnectFailReason.UserRequest,
                ShutdownReason.OperationCanceled => ConnectFailReason.UserRequest,
                ShutdownReason.DisconnectedByPluginLogic => ConnectFailReason.Disconnect,
                ShutdownReason.Error => ConnectFailReason.Disconnect,
                _ => ConnectFailReason.None,
            };
        }
    }
}