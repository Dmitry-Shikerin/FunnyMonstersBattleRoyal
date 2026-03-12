using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.Frameworks.GameServices.SceneLoaderServices.Implementation;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;

namespace Sources.App.DIContainers.Projects
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ISceneLoaderService, SceneLoaderService>();
            //container.Bind<ISceneLoaderService, AddressableSceneLoaderService>();
        }
    }
}