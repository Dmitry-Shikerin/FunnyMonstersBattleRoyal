using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners;
using Sources.Frameworks.DeepFramework.DeepTwens.Presentation.MonoSequence.Containers;
using Sources.Frameworks.DeepFramework.DeepTwens.Presentation.MonoSequence.Containers.Impl;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Presentation
{
    public class DeepTweenAnimation : MonoBehaviour
    {
        [OnValueChanged(nameof(Set))]
        [SerializeField] public TweenType Type;
        [SerializeField] public bool IsLoop;
        
        [HideIf(nameof(MonoTweenIsNull))]
        [SerializeReference] public MonoTween MonoTween;
        [HideInInspector]
        [SerializeReference] private Tween _tweener;

        public Tween Play()
        {
            _tweener = MonoTween.Play();
            return _tweener;
        }

        [Button("Play")]
        public void PlayVoid()
        {
            _tweener = MonoTween.Play().Play();
        }

        [Button]
        private void Stop() =>
            _tweener?.Stop();

        private bool MonoTweenIsNull() =>
            MonoTween == null;

        private void Set(TweenType type)
        {
            MonoTween = type switch
            {
                TweenType.SpriteSwaper => new SpriteSwaperTween(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}