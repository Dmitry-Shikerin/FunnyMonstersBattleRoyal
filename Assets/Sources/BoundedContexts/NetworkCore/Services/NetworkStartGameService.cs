using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Menu;
using Fusion.Photon.Realtime;
using Sources.BoundedContexts.Hud.Presentations.MainMenu;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.DeepWrappers.Curtains;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.BoundedContexts.NetworkCore.Services
{
    public class NetworkStartGameService
    {
        private readonly ISceneService _sceneService;
        private readonly IUiViewService _uiViewService;
        private readonly FusionMenuConfig _config;
        private readonly ICurtainService _curtainService;
        private readonly IUiPopUpService _popUpService;
        private string _region;
        private bool _connectingSafeCheck;
        private NetworkRunner _runner;
        private string _sessionName;
        private int _maxPlayerCount;
        private CancellationTokenSource _tokenSource;
        private string _appVersion;

        public NetworkStartGameService(
            ICurtainService curtainService,
            IUiPopUpService popUpService, 
            ISceneService sceneService,
            IUiViewService uiViewService,
            FusionMenuConfig config)
        {
            _curtainService = curtainService;
            _popUpService = popUpService;
            _sceneService = sceneService;
            _uiViewService = uiViewService;
            _config = config;
        }

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
        
        public async UniTask StartGameAsync(FusionMenuConnectArgs connectArgs, string session, bool isCreating)
        {
            connectArgs.Session = session;
            connectArgs.Creating = isCreating;
            connectArgs.Region = connectArgs.PreferredRegion;
            connectArgs.MaxPlayerCount = 10;
            
            // _uiViewService.Show(UiViewId.Loading);
            Debug.Log($"ShowAsync");
            await _curtainService.ShowAsync();
            string sceneName = IdsConst.Lobby;
            ConnectResult result = await ConnectAsync(connectArgs, sceneName);

            await HandleConnectionResult(result, sceneName);
        }

        private async UniTask HandleConnectionResult(ConnectResult result, string sceneName) 
        {
            if (result.CustomResultHandling)
                return;
            
            if (result.Success) 
            {
                await UniTask.WaitUntil(() => NetworkRunnerProvider.Runner.IsRunning);
                await UniTask.Delay(TimeSpan.FromSeconds(1));

                await _sceneService.ChangeSceneAsync(sceneName);
            } 
            else if (result.FailReason != ConnectFailReason.ApplicationQuit)
            {
                _curtainService.HideAsync();
                _popUpService.Get<MainMenuUiPopUp>().SetMassage("Connection Failed");
            }
        }

        private async UniTask<ConnectResult> ConnectAsync(FusionMenuConnectArgs connectionArgs, string sceneName)
        {
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
                return GetCustomHandlingConnectResult();

            _connectingSafeCheck = true;

            if (_runner && _runner.IsRunning)
                await _runner.Shutdown();

            // Create and prepare Runner object
            _runner = NetworkRunnerProvider.Runner;
            NetworkSceneManagerDefault sceneManager = NetworkRunnerProvider.SceneManagerDefault;
            sceneManager.IsSceneTakeOverEnabled = false;

            // Solve StartGameArgs
            StartGameArgs startGameArgs = GetGameArgs(connectArgs, sceneName);
            StartGameResult startGameResult = await _runner.StartGame(startGameArgs);
            ConnectResult connectResult = GetConnectResult(startGameResult);
            _connectingSafeCheck = false;

            if (connectResult.Success)
                _sessionName = _runner.SessionInfo.Name;

            return connectResult;
        }

        private ConnectResult GetCustomHandlingConnectResult()
        {
            return new ConnectResult
            {
                CustomResultHandling = true,
                Success = false,
                FailReason = ConnectFailReason.None,
            };
        }

        private ConnectResult GetConnectResult(StartGameResult startGameResult)
        {
            return new ConnectResult()
            {
                Success = startGameResult.Ok,
                FailReason = ResolveConnectFailReason(startGameResult.ShutdownReason),
            };
        }

        private StartGameArgs GetGameArgs(FusionMenuConnectArgs connectArgs, string sceneName)
        {
            FusionAppSettings appSettings = CopyAppSettings(connectArgs);
            NetworkSceneInfo sceneInfo = new NetworkSceneInfo();
            sceneInfo.AddSceneRef(GetSceneRef(sceneName));
            _tokenSource?.Dispose();
            _tokenSource = new CancellationTokenSource();
            int regionIndex = _config.AvailableRegions.IndexOf(connectArgs.Region);
            
            return new StartGameArgs()
            {
                CustomPhotonAppSettings = appSettings,
                GameMode = GameMode.AutoHostOrClient,
                SessionName = _sessionName = connectArgs.Session,
                PlayerCount = _maxPlayerCount = connectArgs.MaxPlayerCount,
                Scene = sceneInfo,
                StartGameCancellationToken = _tokenSource.Token,
                SessionNameGenerator = () => _config.CodeGenerator.EncodeRegion(_config.CodeGenerator.Create(), regionIndex),
            };
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
    }
}