using System;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Singletones
{
    public class MonoBehaviourSingleton<T>  : MonoBehaviour
        where T : MonoBehaviour
    {
        protected static T s_instance;

        public static T Instance
        {
            get
            {
                if (DeepCoreManager.IsApplicationQuitting)
                {
                    //todo добавить сеттинги для кора в котором можно будет выбрать кидать ли эксепшн или нулл
                    return null;
                    //throw new InvalidOperationException($"An attempt to create an object when closing an application in a class {typeof(T).Name}");
                }
                
                if (s_instance == null)
                    s_instance = FindFirstObjectByType<T>();

                if (s_instance == null)
                    s_instance = new GameObject(typeof(T).Name).AddComponent<T>();

                return s_instance;
            }
        }
    }
}