using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Utils
{
    public static class SettingsUtilsExt
    {
        /// <summary>
        /// Return the current selected resolution index based on Screen.resolutions.
        /// </summary>
        /// <returns>Index into Screen.resolutions</returns>
        public static int GetCurrentResolutionIndex() 
        {
            Resolution[] resolutions = Screen.resolutions;
            
            if (resolutions == null || resolutions.Length == 0)
                return -1;

            int currentWidth = Mathf.RoundToInt(Screen.width);
            int currentHeight = Mathf.RoundToInt(Screen.height);
#if UNITY_2022_2_OR_NEWER
            RefreshRate defaultRefreshRate = resolutions[^1].refreshRateRatio;
#else
      var defaultRefreshRate = resolutions[^1].refreshRate;
#endif

            for (int i = 0; i < resolutions.Length; i++) 
            {
                Resolution resolution = resolutions[i];

                if (resolution.width == currentWidth
                    && resolution.height == currentHeight
#if UNITY_2022_2_OR_NEWER
                    && resolution.refreshRateRatio.denominator == defaultRefreshRate.denominator
                    && resolution.refreshRateRatio.numerator == defaultRefreshRate.numerator)
#else
          && resolution.refreshRate == defaultRefreshRate)
#endif
                {
                    return i;
                }
            }

            return -1;
        }
    }
}