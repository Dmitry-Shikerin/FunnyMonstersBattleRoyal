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
    public class ButtonClickCondition : ConditionTask
    {
        public ButtonId ButtonId;

        protected override void OnEnable()
        {
            DeepUiBrain.SignalBus.Subscribe<OnClickSignal>(OnClick);
        }

        protected override void OnDisable()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            DeepUiBrain.SignalBus.Unsubscribe<OnClickSignal>(OnClick);
        }

        private void OnClick(OnClickSignal signal)
        {
            if (signal.ButtonId != ButtonId)
                return;
            
            YieldReturn(true);
        }

        protected override bool OnCheck() =>
            false;
    }
}