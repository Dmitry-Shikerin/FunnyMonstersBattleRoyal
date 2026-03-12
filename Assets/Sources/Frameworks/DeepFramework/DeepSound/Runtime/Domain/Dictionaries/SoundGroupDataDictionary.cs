using System;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Dictionaries;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Dictionaries
{
    [Serializable]
    public class SoundGroupDataDictionary : SerializedDictionary<SoundName, SoundGroupData>
    {
    }
}