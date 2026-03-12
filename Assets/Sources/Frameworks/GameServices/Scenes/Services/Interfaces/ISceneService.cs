using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces.Methods;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.Frameworks.GameServices.Scenes.Services.Interfaces
{
    public interface ISceneService : IUpdatable, IFixedUpdatable, ILateUpdatable, IDisable
    {
        string CurrentSceneName { get; }
        UniTask ChangeSceneAsync(string sceneName, object payload = null);
    }
}