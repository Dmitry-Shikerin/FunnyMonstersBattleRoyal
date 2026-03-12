using Sources.Frameworks.GameServices.UpdateServices.Interfaces.Methods;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.EcsBoundedContexts.Core
{
    public interface IEcsGameStartUp : IInitialize, IUpdatable, IDestroy
    {
    }
}