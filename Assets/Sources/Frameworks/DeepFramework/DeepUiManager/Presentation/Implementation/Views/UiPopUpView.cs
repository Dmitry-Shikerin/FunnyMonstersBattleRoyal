using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views
{
    [DisallowMultipleComponent]
    public class UiPopUpView : UiContainerBase
    {
        //Const
        private const string UiViewLabel = "<size=18><b><color=#0000FF><i>Ui Pop Up</i></color></b></size>";
        
        [DisplayAsString(false)] 
        [HideLabel] 
        [PropertyOrder(-1)]
        [SerializeField] private string _label = UiViewLabel;
        [EnumToggleButtons]
        [PropertyOrder(-1)]
        [SerializeField] private EnableState _showId = EnableState.On;
        [ShowIf(nameof(_showId), EnableState.On)] 
        [PropertyOrder(-1)]
        [Required] [SerializeField] private UiPopUpId _id;

        private void Awake()
        {
            if (_showId != EnableState.Off && _id != UiPopUpId.Default)
                DeepUiBrain.PopUpViewManager.Register(_id, this);
            
            DeepUiBrain.SignalBus.Subscribe<ShowUiPopUpSignal>(OnSendShowUiPopUpSignal);
            DeepUiBrain.SignalBus.Subscribe<HideUiPopUpSignal>(OnSendHideUiPopUpSignal);
        }

        private void OnSendHideUiPopUpSignal(HideUiPopUpSignal obj)
        {
            if (obj.ViewId != _id)
                return;
            
            Hide();
        }

        private void OnDestroy()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            if (_showId != EnableState.Off && _id != UiPopUpId.Default)
                DeepUiBrain.PopUpViewManager.Unregister(_id);
            
            DeepUiBrain.SignalBus.Unsubscribe<ShowUiPopUpSignal>(OnSendShowUiPopUpSignal);
            DeepUiBrain.SignalBus.Unsubscribe<HideUiPopUpSignal>(OnSendHideUiPopUpSignal);
        }

        private void OnSendShowUiPopUpSignal(ShowUiPopUpSignal obj)
        {
            if (obj.ViewId != _id)
                return;
            
            Show();
        }
    }
}