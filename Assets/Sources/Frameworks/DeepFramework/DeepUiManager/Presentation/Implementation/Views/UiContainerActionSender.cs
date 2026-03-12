using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views
{
    public class UiContainerActionSender : MonoBehaviour
    {
        //Const
        private const string UiContainerLabel = "<size=18><b><color=#0000FF><i>UiContainerActionSender</i></color></b></size>";
        
        [DisplayAsString(false)] [HideLabel] 
        [SerializeField] private string _label = UiContainerLabel;
        
        [Space(10)]
        [SerializeField] private List<UiActionId> _enableCommandId;
        [Space(10)]
        [SerializeField] private List<UiActionId> _disableCommandId;
        [Space(10)]
        [Required] [SerializeField] private UiContainer _container;

        private void Awake()
        {
            _container.Showed += OnShow;
            _container.Hided += OnHide;
        }

        private void OnDestroy()
        {
            _container.Showed -= OnShow;
            _container.Hided -= OnHide;
        }

        private void OnHide() =>
            OnHandle(_disableCommandId);

        private void OnShow() =>
            OnHandle(_enableCommandId);

        private void OnHandle(IEnumerable<UiActionId> actionIds) =>
            DeepUiBrain.ActionHandler.Handle(actionIds);
    }
}