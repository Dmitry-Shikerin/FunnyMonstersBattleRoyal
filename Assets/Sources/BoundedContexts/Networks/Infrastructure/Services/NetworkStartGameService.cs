using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine.SceneManagement;

namespace Sources.BoundedContexts.Networks.Infrastructure.Services
{
    public class NetworkStartGameService
    {
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
    }
}