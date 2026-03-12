using System;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class ExpoEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period)
        {
            if (time != 0.0)
                return (float)Math.Pow(2.0, 10.0 * (time / (double)duration - 1.0));
            
            return 0.0f;
        }

        public override float Out(float time, float duration, float overshootOrAmplitude, float period)
        {
            if (Mathf.Approximately(time, duration))
                return 1f;
            
            return (float)(-Math.Pow(2.0, -10.0 * time / duration) + 1.0);
        }

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period)
        {
            if (time == 0.0)
                return 0.0f;
            
            if (Mathf.Approximately(time, duration))
                return 1f;

            if ((time /= duration * 0.5f) < 1.0)
                return 0.5f * (float)Math.Pow(2.0, 10.0 * (time - 1.0));
            
            return (float)(0.5 * (-Math.Pow(2.0, -10.0 * --time) + 2.0));
        }
    }
}