using Cysharp.Threading.Tasks;
using Photon.Pun;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Frameworks.GameServices.SceneLoaderServices.Implementation
{
    public class PhotonSceneLoaderService : ISceneLoaderService
    {
        public async UniTask Load(string sceneName)
        {
            if (sceneName == IdsConst.Gameplay)
            {
                PhotonNetwork.AutomaticallySyncScene = false;
                PhotonNetwork.SendRate = 60;
                PhotonNetwork.SerializationRate = 60;
                PhotonNetwork.LoadLevel(IdsConst.Gameplay);
                await UniTask.WaitUntil(() => Mathf.Approximately(PhotonNetwork.LevelLoadingProgress, 1) == false);
                Debug.Log($"Load gameplay");
                
                return;
            }

            Debug.Log($"Load main menu");
            await SceneManager.LoadSceneAsync(sceneName);
        }

        public UniTask Unload() =>
            UniTask.CompletedTask;
    }
}