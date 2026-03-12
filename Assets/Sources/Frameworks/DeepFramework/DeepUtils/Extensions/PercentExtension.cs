using Sources.Frameworks.DeepFramework.DeepUtils.Constants;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Extensions
{
    public static class PercentExtension
    {
        public static float FloatToPercent(this float value, float min = 0, float max = 100)
        {
            float percent = max / 100f;
            int currentPercents = 0;
            float startValue = min;

            while (startValue < value)
            {
                startValue += percent;
                currentPercents++;
            }

            return currentPercents;
        }
        
        public static int IntToPercent(this int value, int max) =>
            (int)FloatToPercent((int)value, max: (int)max);

        public static float FloatPercentToUnitPercent(this float value) =>
            value * MathConst.UnitMultiplier;
        
        public static float IntPercentToUnitPercent(this int value) =>
            value * MathConst.UnitMultiplier;
    }
}