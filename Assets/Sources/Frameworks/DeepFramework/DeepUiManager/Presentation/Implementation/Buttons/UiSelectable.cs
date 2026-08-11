using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    public abstract class UiSelectable : MonoBehaviour
    {
        private readonly List<Action> _onClickActions = new();
        
        public event Action OnClick;
        public event Action<bool> Highlited;

        public void AddOnClickListener(Action action) =>
            _onClickActions.Add(action);
        
        public void RemoveOnClickListener(Action action) =>
            _onClickActions.Add(action);

        protected virtual void Click()
        {
            OnClick?.Invoke();

            for (int i = _onClickActions.Count - 1; i >= 0; i--)
                _onClickActions[i]?.Invoke();
        }

        protected virtual void Highlite(bool highlited) =>
            Highlited?.Invoke(highlited);
    }
}