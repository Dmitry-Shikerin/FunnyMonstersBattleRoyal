namespace Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases.Methods
{
    public class LinearEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period) =>
            time / duration;

        public override float Out(float time, float duration, float overshootOrAmplitude, float period) =>
            time / duration;

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period) =>
            time / duration;
    }
}