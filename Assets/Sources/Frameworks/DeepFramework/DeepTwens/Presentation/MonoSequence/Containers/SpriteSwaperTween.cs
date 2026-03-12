using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Presentation.MonoSequence.Containers.Impl
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class SpriteSwaperTween : MonoTween
    {
        public Image Image;
        public List<Sprite> Sprites;
        public float Duration;
        public float Delay = 1f;

        public override Tween Play()
        {
            return Image.SpriteSwap(Sprites, Duration, Delay);
        }
    }
}