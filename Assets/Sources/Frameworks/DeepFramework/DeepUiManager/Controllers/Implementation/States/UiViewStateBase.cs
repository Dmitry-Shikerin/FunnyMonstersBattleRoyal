using System.Collections.Generic;
using NodeCanvas.StateMachines;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.States
{
    public abstract class UiViewStateBase : FSMState
    {
        protected abstract bool IsHideAllPopUpsEntered { get; }
        protected abstract bool IsHideAllPopUpsExited { get; }
        
        //EnteredViews
        private readonly List<UiViewId> _enterShowViews = new();
        private readonly List<UiViewId> _enterHideViews = new();
        protected abstract bool IsHideAllViewsEntered { get; }
        //ExitedViews
        private readonly List<UiViewId> _exitShowViews = new();
        private readonly List<UiViewId> _exitHideViews = new();
        private readonly List<UiActionId> _enterUiActions = new();
        private readonly List<UiActionId> _exitUiActions = new();
        protected abstract bool IsHideAllViewsExited { get; }

        protected override void OnInit()
        {
            AddEnterShowedViews(_enterShowViews);
            AddEnterHidedViews(_enterHideViews);
            AddExitShowViews(_exitShowViews);
            AddExitHidedViews(_exitHideViews);
            AddEnterUiActions(_enterUiActions);
            AddExitUiActions(_exitUiActions);
        }

        protected override void OnEnter()
        {
            ManageEnterViews();
            ManageEnterPopUp();
        }

        protected override void OnExit()
        {
            ManageExitView();
            ManageExitPopUp();
        }

        protected abstract void AddEnterShowedViews(List<UiViewId> viewIds);
        protected virtual void AddEnterHidedViews(List<UiViewId> viewIds) { }
        protected virtual void AddExitShowViews(List<UiViewId> viewIds) { }
        protected virtual void AddExitHidedViews(List<UiViewId> viewIds) { }
        protected virtual void AddEnterUiActions(List<UiActionId> uiActionIds) { }
        protected virtual void AddExitUiActions(List<UiActionId> uiActionIds) { }

        private void ManageEnterViews()
        {
            UiViewManager manager = DeepUiBrain.ViewManager;

            if (IsHideAllViewsEntered)
                manager.HideAll();

            manager.Show(_enterShowViews);
            manager.Hide(_enterHideViews);
            DeepUiBrain.SignalBus.Handle(new UiActionSignal(_enterUiActions));
        }

        private void ManageExitView()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;

            UiViewManager manager = DeepUiBrain.ViewManager;

            if (IsHideAllViewsExited)
                manager.HideAll();

            manager.Show(_exitShowViews);
            manager.Hide(_exitHideViews);
            DeepUiBrain.SignalBus.Handle(new UiActionSignal(_exitUiActions));
        }

        private void ManageEnterPopUp()
        {
            UiPopUpViewManager manager = DeepUiBrain.PopUpViewManager;
            
            if (IsHideAllPopUpsEntered)
                manager.HideAll();
        }

        private void ManageExitPopUp()
        {
            UiPopUpViewManager manager = DeepUiBrain.PopUpViewManager;
            
            if (IsHideAllPopUpsExited)
                manager.HideAll();
        }
    }
}