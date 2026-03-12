using System;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;

namespace Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces
{
    public interface IUiViewServiceBase<in TViewId, in TUiView> 
        where TViewId : Enum
        where TUiView : UiContainerBase
    {
        T Get<T>()
            where T : TUiView;
        void Show(TViewId id);
    }
}