namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class BackEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)((time /= duration) * (double)time * ((overshootOrAmplitude + 1.0) * time - overshootOrAmplitude));

        public override float Out(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)((time = (float)(time / (double)duration - 1.0)) * (double)time * ((overshootOrAmplitude + 1.0) * time + overshootOrAmplitude) + 1.0);

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period)
        {
            if ((time /= duration * 0.5f) < 1.0)
                return (float)(0.5 * (time * (double)time * (((overshootOrAmplitude *= 1.525f) + 1.0) * time - overshootOrAmplitude)));
            
            return (float)(0.5 * ((time -= 2f) * (double)time * (((overshootOrAmplitude *= 1.525f) + 1.0) * time + overshootOrAmplitude) + 2.0));
        }
    }
}