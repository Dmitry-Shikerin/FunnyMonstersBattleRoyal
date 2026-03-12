namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class QuartEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period) =>
            (time /= duration) * time * time * time;

        public override float Out(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)-((time = (float)(time / (double)duration - 1.0)) * (double)time * time * time - 1.0);

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period)
        {
            if ((time /= duration * 0.5f) < 1.0)
                return 0.5f * time * time * time * time;
            
            return (float)(-0.5 * ((time -= 2f) * (double)time * time * time - 2.0));
        }
    }
}