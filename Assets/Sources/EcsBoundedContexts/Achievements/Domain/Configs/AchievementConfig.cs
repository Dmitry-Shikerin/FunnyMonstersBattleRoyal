using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Achievements.Domain.Data;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using UnityEditor;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Achievements.Domain.Configs
{
    public class AchievementConfig : ScriptableObject
    {
        private const string SplitGroup = "Split";
        private const string LeftBoxGroup = SplitGroup + "/Left";
        private const string RightBoxGroup = SplitGroup + "/Right";
        private const string RightHorizontalGroupId = RightBoxGroup + "/Id";
        private const string RightHorizontalGroupTitleId = RightBoxGroup + "/TitleId";
        private const string RightHorizontalGroupDescriptionId = RightBoxGroup + "/DescriptionId";
        private const string ButtonsGroup = "Buttons";
        private const string ResponsiveButtonsGroup = ButtonsGroup + "/" + ButtonsGroup;
        
        [HorizontalGroup(SplitGroup,0.17f, LabelWidth = 30)]
        [BoxGroup(LeftBoxGroup, ShowLabel = false)] 
        [HideLabel]
        [PreviewField(58, ObjectFieldAlignment.Center)]
        public Sprite Sprite;
        [BoxGroup(RightBoxGroup, ShowLabel = false)]
        [HorizontalGroup(RightHorizontalGroupId, 0.93f)]
        [LabelWidth(80)]
        [ValueDropdown("GetId")]
        public string Id;
        [BoxGroup(RightBoxGroup)]
        [HorizontalGroup(RightHorizontalGroupTitleId, 0.93f)]
        [LabelWidth(80)]
        [ValueDropdown("GetLocalisationId")]
        public string TitleId;
        [BoxGroup(RightBoxGroup)]
        [HorizontalGroup(RightHorizontalGroupDescriptionId, 0.93f)]
        [LabelWidth(80)]
        [ValueDropdown("GetLocalisationId")]
        public string DescriptionId;
        [field: SerializeField] public AchievementConfigCollector Parent { get; set; }
        
        private IReadOnlyList<string> GetId() =>
            IdsConst.GetIds<AchievementSaveData>();
        
#if UNITY_EDITOR
        [HideLabel]
        [HorizontalGroup(RightHorizontalGroupId)]
        [Button(SdfIconType.Search)]
        private void PingId()
        {
            Selection.activeObject = LocalizationDataBase.Instance.GetPhrase(Id);
        }

        [HideLabel]
        [HorizontalGroup(RightHorizontalGroupTitleId)]
        [Button(SdfIconType.Search)]
        private void PingTitleId()
        {
            Selection.activeObject = LocalizationDataBase.Instance.GetPhrase(TitleId);
        }

        [HideLabel]
        [HorizontalGroup(RightHorizontalGroupDescriptionId)]
        [Button(SdfIconType.Search)]
        private void PingDescriptionId()
        {
            Selection.activeObject = LocalizationDataBase.Instance.GetPhrase(DescriptionId);
        }
        
        [BoxGroup(ButtonsGroup)]
        [ResponsiveButtonGroup(ResponsiveButtonsGroup)]
        [Button(ButtonSizes.Medium)]
        private void Rename()
        {
            if (string.IsNullOrEmpty(Id))
                return;

            if (string.IsNullOrWhiteSpace(Id))
                return;
            
            Parent.RemoveConfig(this);
            Parent.CreateConfig(Id, TitleId, DescriptionId, Sprite);
        }

        [BoxGroup(ButtonsGroup)]
        [ResponsiveButtonGroup(ResponsiveButtonsGroup)]
        [Button(ButtonSizes.Medium)]
        private void Remove() =>
            Parent.RemoveConfig(this);

        private List<string> GetLocalisationId() =>
            LocalizationDataBase.Instance.Phrases
                .Select(phrase => phrase.LocalizationId)
                .ToList();
#endif
    }
}