using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data
{
    public class LocalizationDataBase : ScriptableObject
    {
        private const string GetIdTabGroup = "GetId";
        private const string DatabaseTab = "DataBase";
        private const string CreatePhraseTab = "CreatePhrase";
        private const string TextIdLabel = "TextId";
        private const string RussianLabel = "Russian";
        private const string EnglishLabel = "English";
        private const string TurkishLabel = "Turkish";
        private const int Space = 10;
        
        private static LocalizationDataBase s_instance;

        public static LocalizationDataBase Instance
        {
            get
            {
                if (s_instance != null)
                    return s_instance;

                s_instance = Resources.Load<LocalizationDataBase>(LocalizationConst.LocalizationDataBaseAssetPath);

                if (s_instance != null)
                    return s_instance;

                s_instance = CreateInstance<LocalizationDataBase>();

#if UNITY_EDITOR
                AssetDatabase.CreateAsset(s_instance,
                    "Assets/Resources/Services/Localizations/ " + LocalizationConst.LocalizationDatabaseAsset);
#endif

                return s_instance;
            }
        }
        
        [DisplayAsString(false)] [HideLabel] [SerializeField]
        private string _headere = LocalizationConst.UiLocalizationDataBaseLabel;

        //Data
        [TabGroup(GetIdTabGroup, DatabaseTab)] 
        [Space(Space)] 
        [SerializeField] private List<LocalizationPhrase> _phrases;
        
        [TabGroup(GetIdTabGroup, DatabaseTab)] 
        [Space(Space)] 
        [SerializeField] private List<LocalizationScope> _scopes;
        
        [field: Space(Space)]
        [field: SerializeField] public LocalizationLanguage Language { get; private set; }
        
        [FormerlySerializedAs("_enableTextId")]
        [TabGroup(GetIdTabGroup, CreatePhraseTab)] 
        [EnumToggleButtons] 
        [Space(Space)] 
        [LabelText(TextIdLabel)]
        [SerializeField] private EnableState enableStateTextId;
        
        [TabGroup(GetIdTabGroup, CreatePhraseTab)]
        [HideLabel] 
        [ValidateInput("ValidateTextId", "TextId contains in DataBase")]
        [EnableIf("enableStateTextId", EnableState.On)]
        [SerializeField] private string _textId;
        
        //Russian
        [FormerlySerializedAs("_enableRussian")]
        [Space(Space)]
        [TabGroup(GetIdTabGroup, CreatePhraseTab)] 
        [EnumToggleButtons] 
        [Space(Space)] 
        [LabelText(RussianLabel)]
        [SerializeField] private EnableState enableStateRussian;
        
        [TabGroup(GetIdTabGroup, CreatePhraseTab)]
        [TextArea(1, 20)] 
        [HideLabel] 
        [EnableIf("enableStateRussian", EnableState.On)]
        [SerializeField] private string _russian;
        
        //English
        [FormerlySerializedAs("_enableEnglish")]
        [TabGroup(GetIdTabGroup, CreatePhraseTab)] 
        [EnumToggleButtons] 
        [Space(Space)] 
        [LabelText(EnglishLabel)]
        [SerializeField] private EnableState enableStateEnglish;
        
        [TabGroup(GetIdTabGroup, CreatePhraseTab)]
        [TextArea(1, 20)] 
        [HideLabel]
        [EnableIf("enableStateEnglish", EnableState.On)]
        [SerializeField] private string _english;
        
        //Turkish
        [FormerlySerializedAs("_enableTurkish")]
        [TabGroup(GetIdTabGroup, CreatePhraseTab)] 
        [EnumToggleButtons] 
        [Space(Space)] 
        [LabelText(TurkishLabel)]
        [SerializeField] private EnableState enableStateTurkish;
        
        [TabGroup(GetIdTabGroup, CreatePhraseTab)]
        [TextArea(1, 20)] 
        [HideLabel]
        [EnableIf("enableStateTurkish", EnableState.On)]
        [SerializeField] private string _turkish;

        public IReadOnlyList<LocalizationPhrase> Phrases => _phrases;

        public IReadOnlyDictionary<string, LocalizationPhrase> Get() =>
            _phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase);

#if UNITY_EDITOR
        public void RemovePhrase(LocalizationPhrase phrase)
        {
            AssetDatabase.RemoveObjectFromAsset(phrase);
            _phrases.Remove(phrase);
            AssetDatabase.SaveAssets();
        }

        public void RemoveScope(LocalizationScope localizationScope)
        {
            AssetDatabase.RemoveObjectFromAsset(localizationScope);
            _scopes.Remove(localizationScope);
            AssetDatabase.SaveAssets();
        }

        public LocalizationPhrase GetPhrase(string textId) =>
            _phrases.First(phrase => phrase.LocalizationId == textId);
        
        [TabGroup(GetIdTabGroup, CreatePhraseTab)]
        [Button(ButtonSizes.Large)]
        private void CreatePhrase()
        {
            if (_phrases.Any(phrase => phrase.LocalizationId == _textId))
                return;
            
            LocalizationPhrase phrase = CreateInstance<LocalizationPhrase>();
            
            AssetDatabase.AddObjectToAsset(phrase, this);
            AssetDatabase.Refresh();
            
            _phrases.Add(phrase);
            phrase.Parent = this;
            phrase.TextId = _textId;
            phrase.name = _textId + "_Phrase";

            phrase.Russian = _russian;
            phrase.English = _english;
            phrase.Turkish = _turkish;
            
            AssetDatabase.SaveAssets();
        }

        private List<string> GetScopes()
        {
            if (_scopes == null || _scopes.Count == 0)
                return new List<string>() { "Default", };
            
            return _scopes.Select(scope => scope.Id).ToList();
        }
#endif

        [UsedImplicitly]
        private bool ValidateTextId(string textId) =>
            _phrases.Any(phrase => phrase.LocalizationId == textId) == false;
    }
}