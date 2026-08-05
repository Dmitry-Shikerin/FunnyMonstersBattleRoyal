using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Sources.Frameworks.DeepFramework.DeepCores.Domain.Constants;
using Sources.Frameworks.GameServices.Scenes.Domain.Implementation;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.NetworkCore;
using Sources.EcsBoundedContexts.NetworkCore.Services;

namespace Sources.App.Core
{
    [DefaultExecutionOrder(ExeOrder.AppCore)]
    public class AppCore : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Awake() =>
            DontDestroyOnLoad(this);

        private async void Start()
        {
            try
            {
#if UNITY_EDITOR
                string sceneName = SceneManager.GetActiveScene().name;
                
                if (sceneName == IdsConst.Gameplay || sceneName == IdsConst.Lobby)
                {
                    NetworkRunner runner = NetworkRunnerProvider.Runner;
                    runner.ProvideInput = true;
            
                    await runner.StartGame(new StartGameArgs
                    {
                        GameMode = GameMode.AutoHostOrClient,
                        SceneManager = runner.SceneManager,
                        Scene = GetSceneRef(sceneName),
                        SessionName = "SampleSession",
                    });
                    //Иначе загружает мейн меню сцену
                    await UniTask.WaitUntil(() => NetworkRunnerProvider.Runner.IsRunning);
                    await UniTask.Delay(TimeSpan.FromSeconds(1));

                    await _sceneService.ChangeSceneAsync(sceneName);
                    
                    return;
                }
                
                await _sceneService.ChangeSceneAsync(
                    sceneName, new ScenePayload(SceneManager.GetActiveScene().name, false, false));  
#else
                await _sceneService.ChangeSceneAsync(
                    IdsConst.MainMenu,
                    new ScenePayload(IdsConst.MainMenu, false, false));
#endif
            }
            catch(ArgumentNullException)
            {
            }
            catch (OperationCanceledException)
            {
            }
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

        private void Update() =>
            _sceneService.Update(Time.deltaTime);

        private void LateUpdate() =>
            _sceneService.UpdateLate(Time.deltaTime);

        private void FixedUpdate() =>
            _sceneService.UpdateFixed(Time.fixedDeltaTime);

        private void OnDestroy() =>
            _sceneService?.Disable();

        public void Construct(ISceneService sceneService) =>
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
    }
}