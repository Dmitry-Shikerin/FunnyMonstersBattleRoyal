using Sources.Frameworks.GameServices.UpdateServices.Interfaces.Methods;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.Frameworks.GameServices.InputServices.InputServices
{
    public interface IInputServiceUpdater : IUpdatable, IInitialize, IDestroy
    {
    }
}