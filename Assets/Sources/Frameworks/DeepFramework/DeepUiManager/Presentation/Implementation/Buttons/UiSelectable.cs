using System;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    public abstract class UiSelectable : MonoBehaviour
    {
        public event Action OnClick;
        public event Action<bool> Highlited;

        protected virtual void Click() =>
            OnClick?.Invoke();

        protected virtual void Highlite(bool highlited) =>
            Highlited?.Invoke(highlited);
    }
}