using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Editor.Services
{
    public static class EditorUpdateService
    {
        private static readonly List<Action<float>> Actions = new();

#if UNITY_EDITOR
        public static void Initialize() =>
            EditorApplication.update += Update;

        public static void Destroy() =>
            EditorApplication.update -= Update;
#endif
        public static void UnRegister(Action<float> action) =>
            Actions.Remove(action);

        public static void Register(Action<float> action) =>
            Actions.Add(action);

        public static void UnregisterAll() =>
            Actions.Clear();

        private static void Update()
        {
            for (int i = Actions.Count - 1; i >= 0; i--)
                Actions[i].Invoke(Time.deltaTime);
        }
    }
}