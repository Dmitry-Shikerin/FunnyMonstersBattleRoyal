using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Infrastructure.Services;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Presentation.Implementation
{
    [RequireComponent(typeof(Image))]
    public class UiLocalizationSprite : MonoBehaviour, ISelfValidator
    {
        private const string GetIdGroup = "GetId";
        private const string TranslationsTab = "Translations";
        private const string ButtonGroup = "GetId/Translations/Get";
        private const int Space = 10;
        private const int PreviewFieldSize = 250;
        
        [DisplayAsString(false)] 
        [HideLabel] 
        [SerializeField] private string _label = LocalizationConst.UiLocalizationSpriteLabel;

        [TabGroup(GetIdGroup, TranslationsTab)] 
        [Space(Space)]
        [ValueDropdown("GetDropdownValues")] 
        [OnValueChanged("GetPhrase")]
        [SerializeField] private string _localizationId;
        
        [TabGroup(GetIdGroup, TranslationsTab)]
        [EnumToggleButtons] 
        [Space(Space)]
        [SerializeField] private EnableState _disableTexts = EnableState.Off;
        
        [TabGroup(GetIdGroup, TranslationsTab)] 
        [Space(Space)] 
        [DisableIf("_disableTexts", EnableState.Off)]
        [PreviewField(PreviewFieldSize)]
        [SerializeField] private Sprite _russianSprite;
        
        [TabGroup(GetIdGroup, TranslationsTab)] 
        [Space(Space)]        
        [DisableIf("_disableTexts", EnableState.Off)]
        [PreviewField(PreviewFieldSize)]
        [SerializeField] private Sprite _englishSprite;
        
        [TabGroup(GetIdGroup, TranslationsTab)] 
        [Space(Space)]         
        [DisableIf("_disableTexts", EnableState.Off)]
        [PreviewField(PreviewFieldSize)]
        [SerializeField] private Sprite _turkishSprite;
        
        [Space(Space)]
        [SerializeField] private Image _image;

        public bool IsHide { get; private set; }
        public string Id => _localizationId;

        public void Validate(SelfValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(_localizationId))
                result.AddError($"Localization Id is empty {gameObject.name}");
        }

        private void Awake()
        {
            if (_image == null)
                throw new NullReferenceException(nameof(gameObject.name));
            
            DeepLocalizationBrain.Add(this);
        }

        private void OnDestroy()
        {
            DeepLocalizationBrain.Remove(this);
        }

        public void EnableImage() =>
            _image.enabled = true;

        public void DisableImage() =>
            _image.enabled = false;

        public void SetSprite(Sprite sprite) =>
            _image.sprite = sprite;

        [OnInspectorGUI]
        private void SetImage() =>
            _image = GetComponent<Image>();

        [UsedImplicitly]
        private List<string> GetDropdownValues() =>
            LocalizationDataBase.Instance.Phrases.Select(phrase => phrase.LocalizationId).ToList();

        [UsedImplicitly]
        private void GetPhrase()
        {
            LocalizationPhrase phrase = LocalizationDataBase.Instance.Phrases
                .FirstOrDefault(phrase => phrase.LocalizationId == _localizationId);

            if (phrase == null)
                return;
            
            _russianSprite = phrase.RussianSprite;
            _englishSprite = phrase.EnglishSprite;
            _turkishSprite = phrase.TurkishSprite;
        }

        [TabGroup(GetIdGroup, TranslationsTab)]
        [ResponsiveButtonGroup(ButtonGroup)] 
        [UsedImplicitly]
        private void GetRussian() =>
            _image.sprite = _russianSprite;

        [TabGroup(GetIdGroup, TranslationsTab)]
        [ResponsiveButtonGroup(ButtonGroup)] 
        [UsedImplicitly]
        private void GetEnglish() =>
            _image.sprite = _englishSprite;

        [TabGroup(GetIdGroup, TranslationsTab)]
        [ResponsiveButtonGroup(ButtonGroup)] 
        [UsedImplicitly]
        private void GetTurkish() =>
            _image.sprite = _turkishSprite;
    }
}