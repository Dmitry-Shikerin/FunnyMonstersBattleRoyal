using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases
{
    public static class EaseManager
    {
        private const float DefaultDuration = 1f;
        private const float DefaultOvershootOrAmplitude = 1.2f;
        private const float DefaultPeriod = 1f;

        private static readonly Dictionary<Ease, EaseFunk> s_easeFunks = new();
        private static readonly LinearEase s_LinearEase = new();
        private static readonly SineEase s_SineEase = new();
        private static readonly QuadEase s_QuadEase = new();
        private static readonly CubicEase s_CubicEase = new();
        private static readonly QuartEase s_QuartEase = new();
        private static readonly QuintEase s_QuintEase = new();
        private static readonly ExpoEase s_ExpoEase = new();
        private static readonly CircEase s_CircEase = new();
        private static readonly ElasticEase s_ElasticEase = new();
        private static readonly BackEase s_BackEase = new();
        private static readonly BounceEase s_BounceEase = new();
        private static readonly FlashEase s_FlashEase = new();

        static EaseManager()
        {
            s_easeFunks[Ease.Linear] = s_LinearEase.In;
            s_easeFunks[Ease.InSine] = s_SineEase.In;
            s_easeFunks[Ease.OutSine] = s_SineEase.Out;
            s_easeFunks[Ease.InOutSine] = s_SineEase.InOut;
            s_easeFunks[Ease.InQuad] = s_QuadEase.In;
            s_easeFunks[Ease.OutQuad] = s_QuadEase.Out;
            s_easeFunks[Ease.InOutQuad] = s_QuadEase.InOut;
            s_easeFunks[Ease.InCubic] = s_CubicEase.In;
            s_easeFunks[Ease.OutCubic] = s_CubicEase.Out;
            s_easeFunks[Ease.InOutCubic] = s_CubicEase.InOut;
            s_easeFunks[Ease.InQuart] = s_QuartEase.In;
            s_easeFunks[Ease.OutQuart] = s_QuadEase.Out;
            s_easeFunks[Ease.InOutQuart] = s_QuartEase.InOut;
            s_easeFunks[Ease.InQuint] = s_QuintEase.In;
            s_easeFunks[Ease.OutQuint] = s_QuintEase.Out;
            s_easeFunks[Ease.InOutQuint] = s_QuintEase.InOut;
            s_easeFunks[Ease.InExpo] = s_ExpoEase.In;
            s_easeFunks[Ease.OutExpo] = s_ExpoEase.Out;
            s_easeFunks[Ease.InOutExpo] = s_ExpoEase.InOut;
            s_easeFunks[Ease.InCirc] = s_CircEase.In;
            s_easeFunks[Ease.OutCirc] = s_CircEase.Out;
            s_easeFunks[Ease.InOutCirc] = s_CircEase.InOut;
            s_easeFunks[Ease.InElastic] = s_ElasticEase.In;
            s_easeFunks[Ease.OutElastic] = s_ElasticEase.Out;
            s_easeFunks[Ease.InOutElastic] = s_ElasticEase.InOut;
            s_easeFunks[Ease.InBack] = s_BackEase.In;
            s_easeFunks[Ease.OutBack] = s_BackEase.Out;
            s_easeFunks[Ease.InOutBack] = s_BackEase.InOut;
            s_easeFunks[Ease.InBounce] = s_BounceEase.In;
            s_easeFunks[Ease.OutBounce] = s_BounceEase.Out;
            s_easeFunks[Ease.InOutBounce] = s_BounceEase.InOut;
            s_easeFunks[Ease.Flash] = s_FlashEase.Ease;
            s_easeFunks[Ease.InFlash] = s_FlashEase.In;
            s_easeFunks[Ease.OutFlash] = s_FlashEase.Out;
            s_easeFunks[Ease.InOutFlash] = s_FlashEase.InOut;
            s_easeFunks[Ease.InternalZero] = (_, _, _, _) => 1f;
        }

        /// <summary>
        /// Returns a value between 0 and 1 (inclusive) based on the elapsed time and ease selected
        /// </summary>
        /// <param name="ease">Ease type</param>
        /// <param name="time">Time between 0 and 1</param>
        /// /// <param name="duration">Time between 0 and 1</param>
        /// <returns></returns>
        public static float Evaluate(Ease ease, float time, float duration = 1f) =>
            Evaluate(ease, null, time, duration, DefaultOvershootOrAmplitude, DefaultPeriod);

        /// <summary>
        /// Returns a value between 0 and 1 (inclusive) based on the elapsed time and ease selected
        /// </summary>
        public static float Evaluate(
            Ease easeType,
            EaseFunk customEase,
            float time,
            float duration,
            float overshootOrAmplitude,
            float period)
        {
            if (easeType == Ease.InternalCustom)
                return customEase.Invoke(time, duration, overshootOrAmplitude, period);

            return GetEaseFunk(easeType).Invoke(time, duration, overshootOrAmplitude, period);
        }

        public static EaseFunk GetEaseFunk(Ease ease)
        {
            if (s_easeFunks.TryGetValue(ease, out EaseFunk funk) == false)
                return (time, duration, _, _) => (float)(-(double)(time /= duration) * (time - 2.0));

            return funk;
        }

        internal static bool IsFlashEase(Ease ease)
        {
            return ease switch
            {
                Ease.Flash or Ease.InFlash or Ease.OutFlash or Ease.InOutFlash => true,
                _ => false
            };
        }
    }
}