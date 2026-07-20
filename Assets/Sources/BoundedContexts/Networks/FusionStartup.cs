using Fusion;
using Fusion.Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.BoundedContexts.Networks
{
    public class FusionStartup : MonoBehaviour
    {
        [SerializeField] private NetworkSceneManagerDefault _sceneManager;
        [SerializeField] private NetworkRunner _runner;
        [SerializeField] private string _sessionName = "SampleSession";

        private void Start()
        {
            StartSimulation(GameMode.AutoHostOrClient);
        }

        private async void StartSimulation(GameMode gameMode)
        {
            _runner.ProvideInput = true;
            
            await _runner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                SceneManager = _sceneManager,
                Scene = GetScene(),
                SessionName = _sessionName,
            });
        }

        private SceneRef GetScene()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            return SceneRef.FromIndex(buildIndex);
        }
    }
}