using System;
using Sources.Frameworks.MyGameCreator.Triggers.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Triggers.Implementation.Common.Generic
{
    public abstract class TriggerBase<T> : MonoBehaviour, ITrigger<T>
    {
        public event Action<T> Entered;
        public event Action<T> Exited;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out T component))
                Entered?.Invoke(component);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out T component))
                Exited?.Invoke(component);
        }
    }
}