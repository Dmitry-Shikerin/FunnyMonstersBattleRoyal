using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Extensions;
using Sources.Frameworks.DeepFramework.DeepTwens.Tweners;
using UnityEngine;
using Tween = Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Tween;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Tests
{
    public class DeepTweenTest : MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _target;
        [SerializeField] private float _duration;
        [SerializeField] private Eases.Ease _myEase;
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