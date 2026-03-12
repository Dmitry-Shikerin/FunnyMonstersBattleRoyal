using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views
{
    [DisallowMultipleComponent]
    public class UiView : UiContainerBase
    {
        //Const
        private const string UiViewLabel = "<size=18><b><color=#0000FF><i>Ui View</i></color></b></size>";
        private const int Space = 10;

        [DisplayAsString(false)] 
        [HideLabel] 
        [PropertyOrder(-1)]
        [SerializeField] private string _label = UiViewLabel;
        [EnumToggleButtons]
        [PropertyOrder(-1)]
        [SerializeField] protected EnableState ShowId = EnableState.On;
        [ShowIf(nameof(ShowId), EnableState.On)] 
        [PropertyOrder(-1)]
        [Required] [SerializeField] private UiViewId _id;

        private UiViewManager _viewManager;

        private void Awake()
        {
            _viewManager = DeepUiBrain.ViewManager;

            if (ShowId == EnableState.Off)
                return;

            if (_id == UiViewId.Default)
                return;
            
            _viewManager.Register(_id, this);
        }

        private void OnDestroy()
        {
            if (ShowId == EnableState.Off)
                return;

            if (_id == UiViewId.Default)
                return;

            _viewManager.Unregister(_id);
        }
    }
}