namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class QuadEase : Ease
    {
        public override float In(float time, float duration, float overshootOrAmplitude, float period) =>
            (time /= duration) * time;
        
        public override float Out(float time, float duration, float overshootOrAmplitude, float period) =>
            (float)(-(double)(time /= duration) * (time - 2.0));

        public override float InOut(float time, float duration, float overshootOrAmplitude, float period)
        {
            if ((time /= duration * 0.5f) < 1.0)
                return 0.5f * time * time;
            
            return (float)(-0.5 * (--time * (time - 2.0) - 1.0));
        }
    }
}