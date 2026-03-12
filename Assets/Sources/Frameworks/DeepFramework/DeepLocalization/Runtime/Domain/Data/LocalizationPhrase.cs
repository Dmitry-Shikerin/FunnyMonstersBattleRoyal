using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Extensions;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepUtils;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data
{
    public class LocalizationPhrase : ScriptableObject
    {
        private const string ParentLabel = "Parent";
        private const string RussianLabel = "Russian";
        private const string RussianHorizontalGroup = "Russian/Russian";
        private const string EnglishLabel = "English";
        private const string EnglishHorizontalGroup = "English/English";
        private const string TurkishLabel = "Turkish";
        private const string TurkishHorizontalGroup = "Turkish/Turkish";
        private const int TextAreaMin = 14;
        private const int TextAreaMax = 14;
        private const int Space = 10;
        private const int PreviewFieldHeight = 200;
        private const int LeftWidth = 400;
        private const int RightWidth = 200;
        
        [DisplayAsString(false)] [HideLabel] 
        [SerializeField] private string _headere = LocalizationConst.UiLocalizationPhraseLabel;

        //Data
        [EnumToggleButtons] 
        [LabelText(ParentLabel)]
        [SerializeField] private EnableState _editParent;
        [field: DisableIf("_editParent", EnableState.Off)]
        [field: SerializeField] public LocalizationDataBase Parent { get; set; }
        [field: DisableIf("_editParent", EnableState.Off)]
        [field: SerializeField] public LocalizationScope Scope { get; set; }

        [field: ValueDropdown("GetDropdownValues")] 
        [field: Space(Space)]
        [field: SerializeField] public string LocalizationId { get; set; }
        [field: SerializeField] public string TextId { get; set; }

        //Russian
        [FoldoutGroup(RussianLabel)] 
        [EnumToggleButtons] 
        [LabelText(RussianLabel)]
        [SerializeField] private EnableState _editRussian;
        
        [field: FoldoutGroup(RussianLabel)] 
        [field: TextArea(TextAreaMin, TextAreaMax)] 
        [field: HideLabel]
        [field: EnableIf("_editRussian", EnableState.On)]
        [field: HorizontalGroup(RussianHorizontalGroup, width: LeftWidth)]
        [field: BoxGroup(RussianHorizontalGroup + "/Text")]
        [field: SerializeField] public string Russian { get; set; }
        
        [field: FoldoutGroup(RussianLabel)]
        [field: EnableIf("_editRussian", EnableState.On)]
        [field: HideLabel]
        [field: HorizontalGroup(RussianHorizontalGroup, width: RightWidth)]
        [field: BoxGroup(RussianHorizontalGroup + "/Sprite")]
        [field: PreviewField(PreviewFieldHeight)] 
        [field: SerializeField] public Sprite RussianSprite { get; set; }

        //English
        [FoldoutGroup(EnglishLabel)] 
        [EnumToggleButtons] 
        [LabelText(EnglishLabel)]
        [SerializeField] private EnableState _editEnglish;
        
        [field: EnableIf("_editEnglish", EnableState.On)] 
        [field: FoldoutGroup(EnglishLabel)] 
        [field: TextArea(TextAreaMin, TextAreaMax)] 
        [field: HideLabel]
        [field: HorizontalGroup(EnglishHorizontalGroup, width: LeftWidth)]
        [field: BoxGroup(EnglishHorizontalGroup + "/Text")]
        [field: SerializeField] public string English { get; set; }
        
        [field: EnableIf("_editEnglish", EnableState.On)]
        [field: FoldoutGroup(EnglishLabel)]
        [field: PreviewField(PreviewFieldHeight)] 
        [field: HideLabel]
        [field: HorizontalGroup(EnglishHorizontalGroup, width: RightWidth)]
        [field: BoxGroup(EnglishHorizontalGroup + "/Spritee")]
        [field: SerializeField] public Sprite EnglishSprite { get; set; }

        //Turkish
        [FoldoutGroup(TurkishLabel)] 
        [EnumToggleButtons] 
        [LabelText(TurkishLabel)]
        [SerializeField] private EnableState _editTurkish;
        
        [field: EnableIf("_editTurkish", EnableState.On)] 
        [field: FoldoutGroup(TurkishLabel)]
        [field: TextArea(TextAreaMin, TextAreaMax)] 
        [field: HideLabel]
        [field: HorizontalGroup(TurkishHorizontalGroup, width: LeftWidth)]
        [field: BoxGroup(TurkishHorizontalGroup + "/Text")]
        [field: SerializeField] public string Turkish { get; set; }
        
        [field: EnableIf("_editTurkish", EnableState.On)]
        [field: FoldoutGroup(TurkishLabel)]
        [field: PreviewField(PreviewFieldHeight)] 
        [field: HideLabel]
        [field: HorizontalGroup(TurkishHorizontalGroup, width: RightWidth)]
        [field: BoxGroup(TurkishHorizontalGroup + "/Spritee")]
        [field: SerializeField] public Sprite TurkishSprite { get; set; }
        

#if UNITY_EDITOR
        [Button(ButtonSizes.Large)] 
        [ResponsiveButtonGroup]
        private void ChangeName()
        {
            if (string.IsNullOrWhiteSpace(LocalizationId))
                return;

            LocalizationExtension.RenameAsset(this, LocalizationId);
        }

        [Button(ButtonSizes.Large)] 
        [ResponsiveButtonGroup]
        private void AddTextId()
        {
            var localizationIds = LocalizationExtension.GetTranslateId();

            if (localizationIds.Contains(TextId))
                return;

            LocalizationId = TextId;
            AssetDatabase.SaveAssets();
        }

        [Button(ButtonSizes.Large)]
        [ResponsiveButtonGroup]
        private void Remove()
        {
            Parent.RemovePhrase(this);
        }
#endif

        [UsedImplicitly]
        private List<string> GetDropdownValues() =>
            LocalizationDataBase.Instance.Phrases.Select(phrase => phrase.LocalizationId).ToList();
    }
}