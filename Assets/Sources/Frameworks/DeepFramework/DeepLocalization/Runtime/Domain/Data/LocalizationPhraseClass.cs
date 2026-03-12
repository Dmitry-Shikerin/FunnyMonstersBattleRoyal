using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Data
{
    [Serializable]
    public class LocalizationPhraseClass
    {
        [FoldoutGroup("Value")]
        [FoldoutGroup("Value/Russian")] [EnumToggleButtons]
        [SerializeField] private EnableState _editRussian;
        [FoldoutGroup("Value/Russian")][TextArea(1, 20)] 
        [EnableIf("_editRussian", EnableState.On)]
        [SerializeField] private string _russian;
        
        [FoldoutGroup("Value/English")] [EnumToggleButtons]
        [SerializeField] private EnableState _editEnglish;
        [EnableIf("_editEnglish", EnableState.On)]
        [FoldoutGroup("Value/English")] [TextArea(1, 20)]
        [SerializeField] private string _english;
        
        [FoldoutGroup("Value/Turkish")] [EnumToggleButtons]
        [SerializeField] private EnableState _editTurkish;
        [EnableIf("_editTurkish", EnableState.On)]
        [FoldoutGroup("Value/Turkish")][TextArea(1, 20)]
        [SerializeField] private string _turkish;

        public string Russian => _russian;
        public string English => _english;
        public string Turkish => _turkish;
    }
}