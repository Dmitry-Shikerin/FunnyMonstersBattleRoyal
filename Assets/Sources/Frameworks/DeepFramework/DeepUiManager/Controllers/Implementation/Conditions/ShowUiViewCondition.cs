using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.Conditions
{
    [Category("Custom/UI")]
    public class ShowUiViewCondition : ConditionTask
    {
        public UiViewId ViewId;

        protected override void OnEnable()
        {
            DeepUiBrain.SignalBus.Subscribe<ShowUiViewSignal>(OnShow);
        }

        protected override void OnDisable()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            DeepUiBrain.SignalBus.Unsubscribe<ShowUiViewSignal>(OnShow);
        }

        private void OnShow(ShowUiViewSignal signal)
        {
            if (signal.ViewId != ViewId)
                return;
            
            YieldReturn(true);
        }

        protected override bool OnCheck() =>
            false;
    }
}