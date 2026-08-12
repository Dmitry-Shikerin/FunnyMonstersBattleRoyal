using System;
using Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Data
{
    [Serializable]
    public struct RectTransformMoveData
    {
        public RectTransform Transform;
        public Vector3 TargetPosition;
        public RectTransform TargetTransformPosition;
        public float Duration;
        public Ease Ease;
    }
}