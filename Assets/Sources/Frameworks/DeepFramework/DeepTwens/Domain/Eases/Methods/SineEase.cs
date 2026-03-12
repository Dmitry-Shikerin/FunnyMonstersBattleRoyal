using System;
using Sources.Frameworks.DeepFramework.DeepUtils.Constants;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class SineEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)(-Math.Cos(time / (double)duration * MathConst.HalfPi) + 1.0);

        public override float Out(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)Math.Sin(time / (double)duration * MathConst.HalfPi);

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)(-0.5 * (Math.Cos(MathConst.Pi * time / duration) - 1.0));
    }
}