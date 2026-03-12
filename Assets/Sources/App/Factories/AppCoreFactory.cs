using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Contexts;
using Sources.App.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.Curtains.Presentation.Implementation;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Implementation;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.App.Factories
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();

            ProjectContext projectContext = Object.FindObjectOfType<ProjectContext>();
            CurtainView curtainView =
                Object.Instantiate(Resources.Load<CurtainView>(ResourcesPrefabPath.Curtain)) ??
                throw new NullReferenceException(nameof(CurtainView));
            projectContext.Container.Bind<ICurtainView, CurtainView>(curtainView);
            ISceneLoaderService sceneLoaderService = projectContext.Container.Resolve<ISceneLoaderService>();
            curtainView.Hide();
            
            Dictionary<string, Func<object, SceneContext, UniTask<IScene>>> sceneFactories =
                new Dictionary<string, Func<object, SceneContext, UniTask<IScene>>>();
            SceneService sceneService = new SceneService(sceneFactories);
            projectContext.Container.Bind<ISceneService, SceneService>(sceneService);

            sceneFactories[IdsConst.MainMenu] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<ISceneFactory>().Create(payload);
            sceneFactories[IdsConst.Gameplay] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<ISceneFactory>().Create(payload);            

            sceneService.AddBeforeSceneChangeHandler(async _ => await curtainView.ShowAsync());
            sceneService.AddBeforeSceneChangeHandler(async sceneName => await sceneLoaderService.Load(sceneName));

            appCore.Construct(sceneService);

            return appCore;
        }
    }
}