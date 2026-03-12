using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.DailyRewards.Presentation
{
    public class DailyRewardModule : EntityModule
    {
        [field: Required] [field: SerializeField] public Button Button { get; private set; }
        [field: Required] [field: SerializeField] public TMP_Text TimerText { get; private set; }
        [field: Required] [field: SerializeField] public Image LockImage { get; private set; }
        [field: Required] [field: SerializeField] public Image OutlineImage { get; private set; }
        //[field: Required] [field: SerializeField] public DeepTweenMonoSequence Animator { get; private set; }
        [field: Required] [field: SerializeField] public CanvasGroup TimerCanvasGroup { get; private set; }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
        }

        protected override void OnAfterDisable()
        {
            Button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (Entity.HasApplyDailyRewardEvent())
                return;
            
            Entity.AddApplyDailyRewardEvent();
        }
    }
}