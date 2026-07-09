using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces.Methods;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.EcsBoundedContexts.Core
{
    public interface IEcsGameStartUp : IUpdatable, IDestroy
    {
        UniTask Initialize();
    }
}