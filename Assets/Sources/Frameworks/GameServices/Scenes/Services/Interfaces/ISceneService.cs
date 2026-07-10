using System;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces.Methods;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.Frameworks.GameServices.Scenes.Services.Interfaces
{
    public interface ISceneService : IUpdatable, IFixedUpdatable, ILateUpdatable, IDisable
    {
        string CurrentSceneName { get; }
        void AddBeforeSceneChangeHandler(Func<string, UniTask> handler);
        void AddFactory(string key, Func<object, Container, UniTask<IScene>> value);
        UniTask ChangeSceneAsync(string sceneName, object payload = null);
    }
}