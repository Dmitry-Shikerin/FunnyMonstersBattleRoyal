using System;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation
{
    public abstract class UiViewManagerBase<TViewId, TUiView> 
        where TViewId : Enum
        where TUiView : UiContainerBase
    {
        private readonly Dictionary<TViewId, TUiView> _views = new ();

        public void Initialize()
        {
            foreach (KeyValuePair<TViewId, TUiView> viewId in _views)
                viewId.Value.Initialize();
        }
        
        public void Destroy()
        {
            _views.Clear();
        }

        public void Show(IEnumerable<TViewId> viewIds)
        {
            foreach (TViewId viewId in viewIds)
            {
                if (_views.ContainsKey(viewId) == false)
                    throw new KeyNotFoundException(viewId.ToString());
                
                _views[viewId].Show();
            }
        }  

        public void Hide(IEnumerable<TViewId> viewIds)
        {
            foreach (TViewId viewId in viewIds)
            {
                if (_views.ContainsKey(viewId) == false)
                    throw new KeyNotFoundException(viewId.ToString());
                
                _views[viewId].Hide();
            }
        }
        
        public void HideAll()
        {
            foreach (KeyValuePair<TViewId, TUiView> viewId in _views)
                viewId.Value.Hide();
        }

        public void Register(TViewId viewId, TUiView view)
        {
            if (_views.TryAdd(viewId, view) == false)
                throw new InvalidOperationException(viewId.ToString());
        }

        public void Unregister(TViewId viewId)
        {
            if (_views.ContainsKey(viewId) == false)
                return;
            
            _views.Remove(viewId);
        }      
        
        public TUiView Get(TViewId viewId) =>
            _views[viewId];       

        public T Get<T>()
            where T : TUiView
        {
            foreach (KeyValuePair<TViewId, TUiView> viewId in _views)
            {
                if (viewId.Value is not T converted)
                    continue;
                    
                return converted;
            }
            
            throw new KeyNotFoundException(typeof(T).ToString());
        }         
    }
}