using System.Collections.Generic;
using Animancer;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Animancers.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(AnimationConfig), menuName = "Configs/" + nameof(AnimationConfig), order = 51)]
    public class AnimationConfig : SerializedScriptableObject
    {
         public Dictionary<AnimationName, TransitionAsset> Animations = new();
         public Dictionary<AnimationEventName, StringReference> AnimationNames = new();
    }
}