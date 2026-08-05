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
        private bool _isRunned;

        public async UniTask Load(string sceneName)
        {
            _isRunned = true;

            if (sceneName == IdsConst.Gameplay)
                return;
            
            if (sceneName == IdsConst.Lobby)
                return;
            
            await SceneManager.LoadSceneAsync(sceneName);
        }

        public UniTask Unload()
        {
            if (_isRunned == false)
                return UniTask.CompletedTask;
            
            return UniTask.CompletedTask;
        }
    }
}