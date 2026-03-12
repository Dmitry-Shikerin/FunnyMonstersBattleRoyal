using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.Frameworks.MyGameCreator.SkyAndWeathers.Infrastructure.Services.Interfaces
{
    public interface ISkyAndWeatherService : IInitialize, IDestroy
    {
    }
}