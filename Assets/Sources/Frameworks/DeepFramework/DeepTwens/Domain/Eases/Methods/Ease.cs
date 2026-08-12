namespace Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases.Methods
{
    public abstract class Ease
    {
        public abstract float In(float time, float duration, float overshootOrAmplitude, float period);

        public abstract float Out(float time, float duration, float overshootOrAmplitude, float period);

        public abstract float InOut(float time, float duration, float overshootOrAmplitude, float period);
    }
}