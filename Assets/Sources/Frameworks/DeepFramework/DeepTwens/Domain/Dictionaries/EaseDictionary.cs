using System;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using Sources.Frameworks.DeepFramework.DeepUtils.Dictionaries;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Domain.Dictionaries
{
    [Serializable]
    public class EaseDictionary : SerializedDictionary<Ease, AnimationCurve>
    {
    }
}