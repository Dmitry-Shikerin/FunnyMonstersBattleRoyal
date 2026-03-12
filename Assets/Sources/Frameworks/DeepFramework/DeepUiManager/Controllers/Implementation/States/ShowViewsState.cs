using System.Collections.Generic;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using UiActionId = Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums.UiActionId;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.States
{
    [Category("Custom/UI")]
    public class ShowViewsState : FSMState
    {
        public bool IsHideAllViewsEntered = false;
        public List<UiViewId> EnterShowViews = new ();
        public List<UiViewId> EnterHideViews = new ();
        public bool IsHideAllViewsExited = false;
        public List<UiViewId> ExitShowViews = new ();
        public List<UiViewId> ExitHideViews = new ();
        public List<UiActionId> EnterUiActions = new ();
        public List<UiActionId> ExitUiActions = new ();

        protected override void OnEnter()
        {
            UiViewManager manager = DeepUiBrain.ViewManager;
            
            if (IsHideAllViewsEntered)
                manager.HideAll();
            
            manager.Show(EnterShowViews);
            manager.Hide(EnterHideViews);
            DeepUiBrain.SignalBus.Handle(new UiActionSignal(EnterUiActions));
        }

        protected override void OnExit()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            UiViewManager manager = DeepUiBrain.ViewManager;
            
            if (IsHideAllViewsExited)
                manager.HideAll();
            
            manager.Show(ExitShowViews);
            manager.Hide(ExitHideViews);
            DeepUiBrain.SignalBus.Handle(new UiActionSignal(ExitUiActions));
        }
    }
}