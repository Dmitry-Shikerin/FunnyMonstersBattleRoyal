using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Presentation.MonoSequence.Containers
{
    [Serializable]
    [HideReferenceObjectPicker]
    public abstract class MonoTween
    {
        public abstract Tween Play();
    }
}