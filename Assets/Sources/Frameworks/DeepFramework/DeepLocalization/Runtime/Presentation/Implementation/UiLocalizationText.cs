using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Infrastructure.Services;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using TMPro;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Presentation.Implementation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UiLocalizationText : MonoBehaviour, ISelfValidator
    {
        private const string GetIdGroup = "GetId";
        private const string TranslationsGroup = "Translations";
        private const string ButtonGroup = "GetId/Translations/Get";
        private const int Space = 10;
        private const int TextAreaMin = 1;
        private const int TextAreaMax = 20;
        
        [DisplayAsString(false)] [HideLabel] 
        [SerializeField] private string _label = LocalizationConst.UiLocalizationTextLabel;

        [TabGroup(GetIdGroup, TranslationsGroup)] 
        [Space(Space)]
        [ValueDropdown("GetDropdownValues")] 
        [OnValueChanged("GetPhrase")]
        [SerializeField] private string _localizationId;
        
        [TabGroup(GetIdGroup, TranslationsGroup)] 
        [EnumToggleButtons] 
        [Space(Space)]
        [SerializeField] private EnableState _disableTexts = EnableState.Off;
        
        [TabGroup(GetIdGroup, TranslationsGroup)]
        [TextArea(TextAreaMin, TextAreaMax)] 
        [Space(Space)]
        [DisableIf("_disableTexts", EnableState.Off)]
        [SerializeField] private string _russianText;
        
        [TabGroup(GetIdGroup, TranslationsGroup)] 
        [TextArea(TextAreaMin, TextAreaMax)]  
        [Space(Space)]
        [DisableIf("_disableTexts", EnableState.Off)]
        [SerializeField] private string _englishText;
        
        [TabGroup(GetIdGroup, TranslationsGroup)]
        [TextArea(TextAreaMin, TextAreaMax)] 
        [Space(Space)]         
        [DisableIf("_disableTexts", EnableState.Off)]
        [SerializeField] private string _turkishText;
        
        [Space(Space)]
        [SerializeField] private TextMeshProUGUI _tmpText;

        public bool IsHide { get; private set; }
        public string Id => _localizationId;

        public void Validate(SelfValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(_localizationId))
                result.AddError($"Localization Id is empty {gameObject.name}");
        }

        private void Awake()
        {
            if (_tmpText == null)
                throw new NullReferenceException(nameof(gameObject.name));
            
            DeepLocalizationBrain.Add(this);
        }

        private void OnDestroy() =>
            DeepLocalizationBrain.Remove(this);

        public void SetText(string text) =>
            _tmpText.text = text;

        public void SetTextColor(Color color) =>
            _tmpText.color = color;

        public void SetIsHide(bool isHide) =>
            IsHide = isHide;

        public void EnableText() =>
            _tmpText.enabled = true;

        public void DisableText() =>
            _tmpText.enabled = false;

        [OnInspectorGUI]
        public void SetTmpText() =>
            _tmpText = GetComponent<TextMeshProUGUI>();

        [UsedImplicitly]
        private List<string> GetDropdownValues() =>
            LocalizationDataBase.Instance.Phrases.Select(phrase => phrase.LocalizationId).ToList();

        [UsedImplicitly]
        private void GetPhrase()
        {
            var phrase = LocalizationDataBase.Instance.Phrases
                .FirstOrDefault(phrase => phrase.LocalizationId == _localizationId);

            if (phrase == null)
                return;
            
            _russianText = phrase.Russian;
            _englishText = phrase.English;
            _turkishText = phrase.Turkish;
        }

        [TabGroup(GetIdGroup, TranslationsGroup)]
        [ResponsiveButtonGroup(ButtonGroup)] 
        [UsedImplicitly]
        private void GetRussian() =>
            _tmpText.text = _russianText;

        [TabGroup(GetIdGroup, TranslationsGroup)]
        [ResponsiveButtonGroup(ButtonGroup)] 
        [UsedImplicitly]
        private void GetEnglish() =>
            _tmpText.text = _englishText;

        [TabGroup(GetIdGroup, TranslationsGroup)]
        [ResponsiveButtonGroup(ButtonGroup)] 
        [UsedImplicitly]
        private void GetTurkish() =>
            _tmpText.text = _turkishText;
    }
}