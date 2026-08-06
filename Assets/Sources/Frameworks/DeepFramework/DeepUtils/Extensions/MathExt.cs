using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Extensions
{
    public static class MathExt
    {
        public static float Normalize(this float value, float min, float max)
        {
            float result = (value - min) / (max - min);
            return Mathf.Clamp01(result);
        }
    }
}