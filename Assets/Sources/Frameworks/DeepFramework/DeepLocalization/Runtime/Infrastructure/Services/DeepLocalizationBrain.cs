using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepCores.Presentation;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using Sources.Frameworks.DeepFramework.DeepUtils.Singletones;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Infrastructure.Services
{
    [DefaultExecutionOrder(-9)]
    public class DeepLocalizationBrain : MonoBehaviourSingleton<DeepLocalizationBrain>, IDeepCoreChild
    {
        [HideInEditorMode]
        [SerializeField] private List<UiLocalizationText> _texts = new();

        [HideInEditorMode]
        [SerializeField] private List<UiLocalizationSprite> _sprites = new();

        private IReadOnlyDictionary<string, Dictionary<string, string>> _textsDictionary;
        private IReadOnlyDictionary<string, Dictionary<string, Sprite>> _spritesDictionary;
        private IReadOnlyDictionary<string, string> _currentLanguageTextDictionary;
        private IReadOnlyDictionary<string, Sprite> _currentLanguageSpriteDictionary;
        private LocalizationDataBase _dataBase;
        private DeepCore _core;

        public GameObject GameObject => gameObject;

        private void Awake()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            InitializeCore();
            Initialize();
        }

        private void InitializeCore()
        {
            DeepLocalizationBrain[] brains = FindObjectsByType<DeepLocalizationBrain>(FindObjectsSortMode.None);

            foreach (DeepLocalizationBrain brain in brains)
            {
                if (brain == this)
                    continue;
                
                Destroy(brain.gameObject);
            }
            
            _core = DeepCore.Instance;
            _core.AddChild(this);
        }

        private void Initialize()
        {
            _dataBase = LocalizationDataBase.Instance;
            _textsDictionary = new Dictionary<string, Dictionary<string, string>>()
            {
                [LocalizationConst.Russian] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.Russian),
                [LocalizationConst.English] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.English),
                [LocalizationConst.Turkish] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.Turkish),
            };
            _spritesDictionary = new Dictionary<string, Dictionary<string, Sprite>>()
            {
                [LocalizationConst.Russian] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.RussianSprite),
                [LocalizationConst.English] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.EnglishSprite),
                [LocalizationConst.Turkish] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.TurkishSprite),
            };
        }

        public static void Add(UiLocalizationSprite sprite) => 
            Instance._sprites.Add(sprite);

        public static void Remove(UiLocalizationSprite sprite) => 
            Instance?._sprites.Remove(sprite);

        public static void Add(UiLocalizationText text) => 
            Instance._texts.Add(text);

        public static void Remove(UiLocalizationText text) => 
            Instance?._texts.Remove(text);

        public static string GetText(string key)
        {
            if(Instance._currentLanguageTextDictionary.ContainsKey(key) == false)
                throw new KeyNotFoundException(nameof(key));
            
            return Instance._currentLanguageTextDictionary[key];
        }

        public static Sprite GetSprite(string key)
        {
            if(Instance._currentLanguageSpriteDictionary.ContainsKey(key) == false)
                throw new KeyNotFoundException(nameof(key));
            
            return Instance._currentLanguageSpriteDictionary[key];
        }

        public static void Translate(string key)
        {
            Instance._currentLanguageTextDictionary = Instance._textsDictionary[key];
            Instance._currentLanguageSpriteDictionary = Instance._spritesDictionary[key];

            foreach (UiLocalizationText textView in Instance._texts)
            {
                if (string.IsNullOrWhiteSpace(textView.Id))
                    Debug.Log($"LocalizationService: {textView.gameObject.name} has empty id");

                if (Instance._currentLanguageTextDictionary.ContainsKey(textView.Id) == false)
                {
                    Debug.Log($"LocalizationService: {textView.Id} not found in LocalizationData");
                    continue;
                }

                textView.SetText(Instance._currentLanguageTextDictionary[textView.Id]);
            }

            foreach (UiLocalizationSprite localizationSprite in Instance._sprites)
            {
                if (Instance._currentLanguageSpriteDictionary.ContainsKey(localizationSprite.Id) == false)
                {
                    Debug.Log($"LocalizationService: {localizationSprite.Id} not found in LocalizationData");
                    continue;
                }
                
                localizationSprite.SetSprite(Instance._currentLanguageSpriteDictionary[localizationSprite.Id]);
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}