namespace Sources.Frameworks.YandexSdkFramework.Sdk.Services
{
    public static class WebApplication
    {
        /// <summary>
        /// Use it to check whether you're running the game in the Editor or another platform.
        /// </summary>
        public static bool IsRunningOnWebGL
        {
            get
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                return true;
#else
                return false;
#endif
            }
        }
    }
}