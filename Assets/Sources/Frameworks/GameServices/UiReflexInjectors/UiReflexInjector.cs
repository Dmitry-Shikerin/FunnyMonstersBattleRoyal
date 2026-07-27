using Reflex.Core;
using Reflex.Injectors;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.GameServices.UiReflexInjectors
{
    public class UiReflexInjector
    {
        private readonly Container _container;
        private readonly IUiViewService _uiViewService;
        private readonly IUiPopUpService _popUpService;

        public UiReflexInjector(
            Container container,
            IUiViewService uiViewService,
            IUiPopUpService popUpService)
        {
            _container = container;
            _uiViewService = uiViewService;
            _popUpService = popUpService;
        }

        public void InjectUiViews()
        {
            foreach (UiView view in _uiViewService.GetAll())
            {
                AttributeInjector.Inject(view, _container);
                
                foreach (MonoBehaviour mono in view.InjectedMonoBehaviours)
                    AttributeInjector.Inject(mono, _container);
            }
            
            foreach (UiPopUpView view in _popUpService.GetAll())
            {
                AttributeInjector.Inject(view, _container);
                
                foreach (MonoBehaviour mono in view.InjectedMonoBehaviours)
                    AttributeInjector.Inject(mono, _container);
            }
        }
    }
}