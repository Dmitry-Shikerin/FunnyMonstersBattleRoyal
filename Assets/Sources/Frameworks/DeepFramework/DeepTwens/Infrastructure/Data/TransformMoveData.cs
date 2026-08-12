using System;
using Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Data
{
    [Serializable]
    public struct TransformMoveData
    {
        public Transform Transform;
        public Vector3 TargetPosition;
        public Transform TargetTransformPosition;
        public float Duration;
        public Ease Ease;
    }
}