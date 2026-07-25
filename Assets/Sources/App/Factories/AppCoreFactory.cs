using Reflex.Core;
using Sources.App.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;
using UnityEngine;

namespace Sources.App.Factories
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();
            
            Container projectContainer = Container.RootContainer;
            ISceneLoaderService sceneLoaderService = projectContainer.Resolve<ISceneLoaderService>();
            
            ISceneService sceneService = projectContainer.Resolve<ISceneService>();
            sceneService.AddFactory(IdsConst.MainMenu, (payload, container) => container.Resolve<ISceneFactory>().Create(payload));
            sceneService.AddFactory(IdsConst.Gameplay, (payload, container) => container.Resolve<ISceneFactory>().Create(payload));

            //sceneService.AddBeforeSceneChangeHandler(async _ => await curtainView.ShowAsync());
            sceneService.AddBeforeSceneChangeHandler(async _ => await sceneLoaderService.Unload());
            sceneService.AddBeforeSceneChangeHandler(async sceneName => await sceneLoaderService.Load(sceneName));

            appCore.Construct(sceneService);

            return appCore;
        }
    }
}