using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class MainMenuUiPopUp : UiPopUpView
    {
        [Required] [SerializeField] private TMP_Text _text;
        [Required] [SerializeField] private UiButton _button;
        
        private IUiPopUpService _popUpService;

        private void OnEnable() =>
            _button.OnClick += HidePopUp;

        private void OnDisable() =>
            _button.OnClick -= HidePopUp;

        private void HidePopUp() =>
            _popUpService.Hide(UiPopUpId.MainMenuPopUp);

        public void SetMassage(string massage) =>
            _text.text = massage;

        [Inject]
        private void Construct(IUiPopUpService popUpService) =>
            _popUpService = popUpService;
    }
}