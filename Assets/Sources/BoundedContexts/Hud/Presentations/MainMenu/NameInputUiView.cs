using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Players.Presentation.Ui;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Players.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class NameInputUiView : UiView
    {
        [Required] [SerializeField] private TMP_InputField _nameInput;
        [Required] [SerializeField] private TMP_Text _warningText;
        [Required] [SerializeField] private Button _backButton;
        
        private PlayerSaveData _playerSaveData;
        
        private IDataService _dataService;
        private PlayerNameUiView _playerNameUiView;
        private IUiViewService _uiViewService;

        [Inject]
        private void Construct(
            IDataService dataService,
            IUiViewService uiViewService)
        {
            _dataService = dataService;
            _uiViewService = uiViewService;
            _playerNameUiView = uiViewService.Get<MainMenuUiView>().PlayerNameUiView;
            LoadPlayerName();
        }

        private void Start()
        {
            _nameInput.onSelect.AddListener(ClearWarningText);
            _nameInput.onEndEdit.AddListener(OnNameInputEnd);
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick()
        {
            _uiViewService.Show(UiViewId.MainMenu);
        }

        private void ClearWarningText(string _)
        {
            _warningText.text = string.Empty;
        }

        protected override void OnBeforeDestroy()
        {
            _nameInput.onEndEdit.RemoveListener(OnNameInputEnd);
            _nameInput.onEndEdit.RemoveListener(OnNameInputEnd);
            _backButton.onClick.RemoveListener(OnBackButtonClick);
        }

        private void OnNameInputEnd(string enteredName) =>
            SetPlayerName(enteredName);

        private void SetPlayerName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                _warningText.text = "Имя не может быть пустым. Используем имя по умолчанию.";
                newName = _playerSaveData.PlayerName;
                _playerNameUiView.SetPlayerName(newName);
            }
            
            _playerSaveData.PlayerName = newName;
            _playerNameUiView.SetPlayerName(newName);
            _dataService.SaveData(_playerSaveData, IdsConst.Player);
        }

        private void LoadPlayerName()
        {
            if (_dataService.HasKey(IdsConst.Player) == false)
            {
                string playerName = GeneratePlayerName();
                SetPlayerName(playerName);
                return;
            }
            
            PlayerSaveData playerSaveData = _dataService.LoadData<PlayerSaveData>(IdsConst.Player);
            SetPlayerName(playerSaveData.PlayerName);
        }

        private string GeneratePlayerName() =>
            $"PlayerName.{Random.Range(0, 9999)}";
    }
}