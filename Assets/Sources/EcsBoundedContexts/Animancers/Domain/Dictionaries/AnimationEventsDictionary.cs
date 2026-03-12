using System;
using Animancer;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Dictionaries;

namespace Sources.EcsBoundedContexts.Animancers.Domain.Dictionaries
{
    [Serializable]
    public class AnimationEventsDictionary : SerializedDictionary<AnimationEventName, StringReference>
    {
    }
}