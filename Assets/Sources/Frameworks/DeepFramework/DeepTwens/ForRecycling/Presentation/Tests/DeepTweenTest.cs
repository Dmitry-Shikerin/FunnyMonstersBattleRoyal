using DG.Tweening;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Extensions;
using UnityEngine;
using Ease = Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases.Ease;
using Tween = Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Tween;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Presentation.Tests
{
    public class DeepTweenTest : MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _target;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _myEase;
        [SerializeField] private DG.Tweening.Ease _doEase;
        private Tween _deepSequence;

        [Button]
        private void SetStart()
        {
            transform.position = _start.position;
        }

        [Button]
        private void PlayMyTween()
        {
            _deepSequence = transform
                .Move(_target.position, _duration)
                .SetEase(_myEase)
                .Play();
        }
        
        [Button]
        private void PlayDoTween()
        {
            transform
                .DOMove(_target.position, _duration)
                .SetEase(_doEase);
        }

        [Button]
        private void Stop()
        {
            _deepSequence.Stop();
        }
    }
}