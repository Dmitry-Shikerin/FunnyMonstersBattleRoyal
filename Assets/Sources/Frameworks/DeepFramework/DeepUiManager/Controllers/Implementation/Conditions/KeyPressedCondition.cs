using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.Conditions
{
    [Category("Custom/UI")]
    public class KeyPressedCondition : ConditionTask
    {
        public KeyCode Key;

        protected override void OnEnable()
        {
            DeepUiBrain.SignalBus.Subscribe<KeyPressedSignal>(KeyPressed);
        }

        protected override void OnDisable()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            DeepUiBrain.SignalBus.Unsubscribe<KeyPressedSignal>(KeyPressed);
        }

        private void KeyPressed(KeyPressedSignal signal)
        {
            if (signal.Key != Key)
                return;
            
            YieldReturn(true);
        }

        protected override bool OnCheck() =>
            false;
    }
}