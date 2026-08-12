using System;
using Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Data
{
    [Serializable]
    public struct CanvasGroupFadeData
    {
        public CanvasGroup CanvasGroup;
        public float Target;
        public float Duration;
        public Ease Ease;
    }
}