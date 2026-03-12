using System;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepCores.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using Sources.Frameworks.DeepFramework.DeepUtils.Singletones;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepCores.Core
{
    [DefaultExecutionOrder(-10)]
    public class DeepCore : MonoBehaviourSingleton<DeepCore>
    {
        private readonly List<Action<float>> _updateActions = new();
        private readonly List<Action<float>> _fixedUpdateActions = new();
        private readonly List<Action<float>> _lateUpdateActions = new();
        private readonly List<IDeepCoreChild> _childs = new();
        
        private void OnApplicationQuit() =>
            DeepCoreManager.SetApplicationQuitting(true);

        private void Awake()
        {
            DeepCoreManager.SetApplicationQuitting(false);
            DeepCore[] deepCores = FindObjectsByType<DeepCore>(FindObjectsSortMode.None);

            foreach (DeepCore deepCore in deepCores)
            {
                if (deepCore == this)
                    continue;
                
                Destroy(deepCore);
            }
            
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            for (int i = _childs.Count - 1; i >= 0; i--)
            {
                IDeepCoreChild tempChild = _childs[i];
                _childs.Remove(tempChild);
                tempChild.Destroy();
            }
            
            _childs.Clear();
        }

        private void Update()
        {
            for (int i = _updateActions.Count - 1; i >= 0; i--)
                _updateActions[i].Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            for (int i = _fixedUpdateActions.Count - 1; i >= 0; i--)
                _fixedUpdateActions[i].Invoke(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            for (int i = _lateUpdateActions.Count - 1; i >= 0; i--)
                _lateUpdateActions[i].Invoke(Time.deltaTime);
        }

        public void AddChild(IDeepCoreChild child)
        {
            _childs.Add(child);
            child.GameObject.transform.SetParent(transform);
        }

        public void RegisterUpdate(Action<float> action) =>
            _updateActions.Add(action);

        public void UnregisterUpdate(Action<float> action) =>
            _updateActions.Remove(action);

        public void RegisterFixedUpdate(Action<float> action) =>
            _fixedUpdateActions.Add(action);

        public void UnregisterFixedUpdate(Action<float> action) =>
            _fixedUpdateActions.Remove(action);

        public void RegisterLateUpdate(Action<float> action) =>
            _lateUpdateActions.Add(action);

        public void UnregisterLateUpdate(Action<float> action) =>
            _lateUpdateActions.Remove(action);
    }
}