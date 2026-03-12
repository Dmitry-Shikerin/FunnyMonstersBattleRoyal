namespace Sources.Frameworks.DeepFramework.DeepTwens.Eases.Methods
{
    public class BounceEase : Ease
    {
        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: accelerating from zero velocity.
        /// </summary>
        /// <param name="time">Current time (in frames or seconds).</param>
        /// <param name="duration">
        /// Expected easing duration (in frames or seconds).
        /// </param>
        /// <param name="unusedOvershootOrAmplitude">Unused: here to keep same delegate for all ease types.</param>
        /// <param name="unusedPeriod">Unused: here to keep same delegate for all ease types.</param>
        /// <returns>The eased value.</returns>
        public override float In(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod) =>
            1f - Out(duration - time, duration, -1f, -1f);

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: decelerating from zero velocity.
        /// </summary>
        /// <param name="time">Current time (in frames or seconds).</param>
        /// <param name="duration">
        /// Expected easing duration (in frames or seconds).
        /// </param>
        /// <param name="unusedOvershootOrAmplitude">Unused: here to keep same delegate for all ease types.</param>
        /// <param name="unusedPeriod">Unused: here to keep same delegate for all ease types.</param>
        /// <returns>The eased value.</returns>
        public override float Out(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
        {
            if ((time /= duration) < 0.3636363744735718)
                return 121f / 16f * time * time;
            
            if (time < 0.7272727489471436)
                return (float)(121.0 / 16.0 * (time -= 0.54545456f) * time + 0.75);

            if (time < 0.9090909361839294)
                return (float)(121.0 / 16.0 * (time -= 0.8181818f) * time + 15.0 / 16.0);
            
            return (float)(121.0 / 16.0 * (time -= 0.95454544f) * time + 63.0 / 64.0);
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="time">Current time (in frames or seconds).</param>
        /// <param name="duration">
        /// Expected easing duration (in frames or seconds).
        /// </param>
        /// <param name="unusedOvershootOrAmplitude">Unused: here to keep same delegate for all ease types.</param>
        /// <param name="unusedPeriod">Unused: here to keep same delegate for all ease types.</param>
        /// <returns>The eased value.</returns>
        public override float InOut(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
        {
            if (time < duration * 0.5)
                return In(time * 2f, duration, -1f, -1f) * 0.5f;
            
            return (float)(Out(time * 2f - duration, duration, -1f, -1f) * 0.5 + 0.5);
        }
    }
}