using Cysharp.Threading.Tasks;
using Fusion;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Frameworks.GameServices.SceneLoaderServices.Implementation
{
    public class PhotonSceneLoaderService : ISceneLoaderService
    {
        private readonly NetworkRunner _runner;
        private bool _isRunned;

        public PhotonSceneLoaderService(NetworkRunner runner)
        {
            _runner = runner;
        }

        public async UniTask Load(string sceneName)
        {
            await UniTask.WaitWhile(() => _runner.IsRunning == false);
            
            if (sceneName == IdsConst.Gameplay)
            {
                // if (_runner.IsServer)
                // {
                //     await _runner.LoadScene(
                //         SceneRef.FromIndex(1),
                //         new LoadSceneParameters
                //         {
                //             loadSceneMode = LoadSceneMode.Additive
                //         });
                // }
                _runner.ProvideInput = true;
            
                await _runner.StartGame(new StartGameArgs
                {
                    GameMode = GameMode.AutoHostOrClient,
                    SceneManager = _runner.SceneManager,
                    Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
                    SessionName = "SampleSession",//Доработать
                });
                Debug.Log($"Load gameplay");

                _isRunned = true;

                return;
            }

            Debug.Log($"Load main menu");
            await _runner.LoadScene(
                SceneRef.FromIndex(0),
                new LoadSceneParameters
                {
                    loadSceneMode = LoadSceneMode.Single
                });
        }

        public async UniTask Unload()
        {
            if (_isRunned == false)
                return;
            
            await _runner.UnloadScene(SceneManager.GetActiveScene().name);
        }
    }
}