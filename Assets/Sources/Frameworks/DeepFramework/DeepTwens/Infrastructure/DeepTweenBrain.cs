using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepCores.Presentation;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Sequences;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners;
using Sources.Frameworks.DeepFramework.DeepTwens.Tweners;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using Sources.Frameworks.DeepFramework.DeepUtils.Singletones;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure
{
    [DefaultExecutionOrder(-6)]
    public class DeepTweenBrain : MonoBehaviourSingleton<DeepTweenBrain>, IDeepCoreChild
    {
        private readonly SequencePool _sequencePool = new ();
        private readonly TweenPool _tweenPool = new ();
        private DeepCore _core;

        public GameObject GameObject => gameObject;
        public static float DeltaTime
        {
            get
            {
#if UNITY_EDITOR
                return EditorApplication.isPlaying ? Time.deltaTime : Time.fixedDeltaTime;
#else
                return Time.deltaTime;
#endif
            }
        }

        private void Awake()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            _core = DeepCore.Instance;
            _core.AddChild(this);
        }

        public void Destroy()
        {
            _tweenPool?.Dispose();
            _sequencePool?.Dispose();
            Destroy(gameObject);
        }

        public static DeepSequence GetSequence()
        {
            if (Application.isPlaying == false)
                return new DeepSequence();
            
            return Instance._sequencePool.Get();
        }

        public static Tween GetTweener()
        {
            if (Application.isPlaying == false)
                return new Tween();
            
            return Instance._tweenPool.Get();
        }
    }
}