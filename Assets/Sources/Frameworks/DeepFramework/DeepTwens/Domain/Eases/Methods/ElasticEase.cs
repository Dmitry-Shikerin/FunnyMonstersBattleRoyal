using System;
using Sources.Frameworks.DeepFramework.DeepUtils.Constants;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class ElasticEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period)
        {
            if (time == 0.0)
                return 0.0f;
            
            if (Mathf.Approximately(time /= duration, 1f))
                return 1f;
            
            if (period == 0.0)
                period = duration * 0.3f;
            
            float num1;
            
            if (overshootOrAmplitude < 1.0)
            {
                overshootOrAmplitude = 1f;
                num1 = period / 4f;
            }
            else
            {
                num1 = period / 6.2831855f * (float)Math.Asin(1.0 / overshootOrAmplitude);
            }

            return (float)-(overshootOrAmplitude * Math.Pow(2.0, 10.0 * --time) * 
                            Math.Sin((time * (double)duration - num1) * MathConst.DoublePi / period));
        }

        public override float Out(float time, float duration, float overshootOrAmplitude, float period)
        {
            if (time == 0.0)
                return 0.0f;
            
            if (Mathf.Approximately(time /= duration, 1f))
                return 1f;
            
            if (period == 0.0)
                period = duration * 0.3f;
            
            float num2;
            
            if (overshootOrAmplitude < 1.0)
            {
                overshootOrAmplitude = 1f;
                num2 = period / 4f;
            }
            else
            {
                num2 = period / MathConst.DoublePi * (float)Math.Asin(1.0 / overshootOrAmplitude);
            }

            return (float)(overshootOrAmplitude * Math.Pow(2.0, -10.0 * time) * 
                Math.Sin((time * (double)duration - num2) * MathConst.DoublePi / period) + 1.0);
        }

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period)
        {
            if (time == 0.0)
                return 0.0f;
            
            if (Mathf.Approximately(time /= duration * 0.5f, 2f))
                return 1f;
            
            if (period == 0.0)
                period = duration * 0.45000002f;
            
            float num3;
            
            if (overshootOrAmplitude < 1.0)
            {
                overshootOrAmplitude = 1f;
                num3 = period / 4f;
            }
            else
            {
                num3 = period / MathConst.DoublePi * (float)Math.Asin(1.0 / overshootOrAmplitude);
            }

            if (time < 1.0)
            {
                return (float)(-0.5 * (overshootOrAmplitude * Math.Pow(2.0, 10.0 * --time) * 
                                       Math.Sin((time * (double)duration - num3) * MathConst.DoublePi / period)));
            }
            
            return (float)(overshootOrAmplitude * Math.Pow(2.0, -10.0 * --time) * 
                Math.Sin((time * (double)duration - num3) * MathConst.DoublePi / period) * 0.5 + 1.0);
        }
    }
}