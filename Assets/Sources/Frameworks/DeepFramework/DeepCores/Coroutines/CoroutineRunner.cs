using System;
using System.Collections;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepCores.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.Singletones;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepCores.Coroutines
{
    public class CoroutineRunner : MonoBehaviourSingleton<CoroutineRunner>, IDeepCoreChild
    {
        private DeepCore _core;
        public static bool ApplicationIsQuitting { get; private set; }
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;
            
            _core = DeepCore.Instance;
            _core.AddChild(this);
        }

        public void Destroy()
        {
            StopAll();
            Destroy(this);
        }

        private void OnApplicationQuit() =>
            ApplicationIsQuitting = true;

        public Coroutine StartLocalCoroutine(IEnumerator enumerator) =>
            StartCoroutine(enumerator);

        public void StopLocalCoroutine(Coroutine coroutine) =>
            StopCoroutine(coroutine);

        public void StopLocalCoroutine(IEnumerator enumerator) =>
            StopCoroutine(enumerator);

        public void StopAllLocalCoroutines() =>
            StopAllCoroutines();

        public static Coroutine Start(IEnumerator enumerator)
        {
            return Instance != null && enumerator != null 
                       ? Instance.StartLocalCoroutine(enumerator) 
                       : null;
        }

        public static void Stop(IEnumerator enumerator)
        {
            if (Instance == null || enumerator == null)
                return;
            
            Instance.StopLocalCoroutine(enumerator);
        }

        public static void Stop(Coroutine coroutine)
        {
            if (Instance == null || coroutine == null)
                return;
            
            Instance.StopLocalCoroutine(coroutine);
        }

        public static void StopAll()
        {
            if (Instance == null)
                return;
            
            Instance.StopAllLocalCoroutines();
        }
    }
}