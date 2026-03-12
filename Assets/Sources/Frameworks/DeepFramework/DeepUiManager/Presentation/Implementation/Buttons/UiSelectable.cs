using System;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    public abstract class UiSelectable : MonoBehaviour
    {
        public event Action OnClick;

        protected virtual void Click()
        {
            OnClick?.Invoke();
        }
    }
}