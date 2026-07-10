using Reflex.Core;
using Sources.App.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
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

            //ProjectContext projectContext = Object.FindObjectOfType<ProjectContext>();            
            //Container sceneContainer = SceneManager.GetActiveScene().GetSceneContainer();
            Container projectContainer = Container.RootContainer;
            // CurtainView curtainView =
            //     Object.Instantiate(Resources.Load<CurtainView>(ResourcesPrefabPath.Curtain)) ??
            //     throw new NullReferenceException(nameof(CurtainView));
            //projectContext.Container.Bind<ICurtainView, CurtainView>(curtainView);
            //curtainView.Hide();
            ISceneLoaderService sceneLoaderService = projectContainer.Resolve<ISceneLoaderService>();
            
            // Dictionary<string, Func<object, SceneContext, UniTask<IScene>>> sceneFactories =
            //     new Dictionary<string, Func<object, SceneContext, UniTask<IScene>>>();
            // SceneService sceneService = new SceneService(sceneFactories);
            //projectContext.Container.Bind<ISceneService, SceneService>(sceneService);
            ISceneService sceneService = projectContainer.Resolve<ISceneService>();
            
            sceneService.AddFactory(IdsConst.MainMenu, (payload, container) => container.Resolve<ISceneFactory>().Create(payload));
            sceneService.AddFactory(IdsConst.Gameplay, (payload, container) => container.Resolve<ISceneFactory>().Create(payload));

            // sceneFactories[IdsConst.MainMenu] = (payload, sceneContext) => sceneContext.Container.Resolve<ISceneFactory>().Create(payload);
            // sceneFactories[IdsConst.Gameplay] = (payload, sceneContext) => sceneContext.Container.Resolve<ISceneFactory>().Create(payload);            

            //sceneService.AddBeforeSceneChangeHandler(async _ => await curtainView.ShowAsync());
            sceneService.AddBeforeSceneChangeHandler(async sceneName => await sceneLoaderService.Load(sceneName));

            appCore.Construct(sceneService);

            return appCore;
        }
    }
}