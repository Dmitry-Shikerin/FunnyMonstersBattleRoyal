using System;
using System.Collections.Generic;
using Dott;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Achievements.Presentation
{
    public class AchievementModule : EntityModule
    {
        [SerializeField] private Button _button;
        [field: SerializeField] public Image IconImage { get; set; }
        [field: SerializeField] public TMP_Text TitleText { get; set; }
        [field: SerializeField] public TMP_Text DescriptionText { get; set; }
        [field: SerializeField] public Image UncompletedImage { get; set; }
        [field: SerializeField] public DOTweenTimeline Animation { get; set; }
        [field: SerializeField] public List<GameObject> SelectedObjects { get; private set; }

        private void OnEnable()
        {
            _button?.onClick.AddListener(OnClick);
        }

        protected override void OnAfterDisable()
        {
            _button?.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (Entity.HasSelectAchievementEvent())
                return;
            
            Entity.AddSelectAchievementEvent();
        }
    }
}