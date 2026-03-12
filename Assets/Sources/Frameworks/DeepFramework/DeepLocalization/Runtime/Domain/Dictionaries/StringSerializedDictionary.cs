using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Dictionaries;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Dictionaries
{
    [Serializable] [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
    public class StringSerializedDictionary : SerializedDictionary<string, string>
    {
    }
}