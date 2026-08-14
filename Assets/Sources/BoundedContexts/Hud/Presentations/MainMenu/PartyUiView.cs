using Fusion.Menu;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.NetworkCore.Services;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class PartyUiView : UiView
    {
        [Required] [SerializeField] private UiButton _createButton;
        [Required] [SerializeField] private UiButton _joinButton;
        [Required] [SerializeField] private TMP_InputField _joinSessionCodeField;
        
        private NetworkStartGameService _startGameService;
        private FusionMenuPartyCodeGenerator _codeGenerator;
        private MainMenuUiPopUp _popUp;
        private FusionMenuConnectArgs _args;

        [Inject]
        private void Construct(
            FusionMenuConfig config,
            IUiPopUpService uiPopUpService,
            IUiViewService uiViewService,
            NetworkStartGameService startGameService)
        {
            _codeGenerator = config.CodeGenerator;
            _startGameService = startGameService;
            _popUp = uiPopUpService.Get<MainMenuUiPopUp>();
            _args = uiViewService.Get<MainMenuUiView>().ConnectArgs;
            SetDefaultText();
        }
        
        private void SetDefaultText()
        {
            _joinSessionCodeField.SetTextWithoutNotify("".PadLeft(_codeGenerator.Length, '-'));
            _joinSessionCodeField.characterLimit = _codeGenerator.Length;
        }

        private void OnEnable()
        {
            _joinButton.AddOnClickListener(JoinGame);
            _createButton.AddOnClickListener(CreateGame);
        }

        private void OnDisable()
        {
            _joinButton.RemoveOnClickListener(JoinGame);
            _createButton.RemoveOnClickListener(CreateGame);
        }

        private async void CreateGame() =>
            await _startGameService.StartGameAsync(_args, null, true);

        private async void JoinGame()
        {
            string inputRegionCode = _joinSessionCodeField.text.ToUpper();
            
            if (IsValidInputRegion(inputRegionCode) == false)
                return;

            await _startGameService.StartGameAsync(_args, inputRegionCode, false);
        }

        private bool IsValidInputRegion(string inputRegionCode)
        {
            if (_codeGenerator.IsValid(inputRegionCode) == false)
            {
                _popUp.SetMassage($"The session code '{inputRegionCode}' is not a valid session code. Please enter {_codeGenerator.Length} characters or digits.");
                return false;
            }

            return true;
        }
    }
}