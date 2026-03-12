using System;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class CircEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)-(Math.Sqrt(1.0 - (time /= duration) * (double)time) - 1.0);

        public override float Out(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)Math.Sqrt(1.0 - (time = (float)(time / (double)duration - 1.0)) * (double)time);

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period)
        {
            if ((time /= duration * 0.5f) < 1.0)
                return (float)(-0.5 * (Math.Sqrt(1.0 - time * (double)time) - 1.0));
            
            return (float)(0.5 * (Math.Sqrt(1.0 - (time -= 2f) * (double)time) + 1.0));
        }
    }
}