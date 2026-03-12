using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Managers
{
    public static class DeepCoreManager
    {
        public static bool IsApplicationQuitting { get; private set; }
        
#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RunOnStart() =>
            SetApplicationQuitting(false);
#endif
        public static void SetApplicationQuitting(bool isApplicationQuitting) =>
            IsApplicationQuitting = isApplicationQuitting;
    }
}