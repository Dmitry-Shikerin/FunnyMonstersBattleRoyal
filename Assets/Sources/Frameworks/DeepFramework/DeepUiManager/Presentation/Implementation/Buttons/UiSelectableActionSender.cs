using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    //TODO обобщить ActionSenders
    public class UiSelectableActionSender : MonoBehaviour
    {
        private const string Label = "<size=18><b><color=#C71585><i>Ui Selectable Action Sender</i></color></b></size>";
        private const int Space = 10;

        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        [SerializeField] private UiSelectable _selectable;
        [Space(Space)]
        [SerializeField] private List<UiActionId> _onClickCommandId;

        private UiActionHandler _actionHandler;

        [OnInspectorInit]
        private void Init()
        {
            _selectable = GetComponent<UiSelectable>();
        }
        
        private void Awake()
        {
            _actionHandler = DeepUiBrain.ActionHandler;
            _selectable.OnClick += OnClick;
        }

        private void OnDestroy()
        {
            _selectable.OnClick -= OnClick;
        }

        private void OnClick()
        {
            if (_onClickCommandId.Count > 0)
                _actionHandler.Handle(_onClickCommandId);
        }
    }
}