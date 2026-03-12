using System;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Extensions;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Presentation.MonoSequence.Containers.Impl
{
    [Serializable]
    public class TransformMoveTween : MonoTween
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _targetPosition;
        [SerializeField] private float _duration;
        
        public override Tween Play()
        {
            return _transform.Move(_targetPosition, _duration);
        }
    }
}