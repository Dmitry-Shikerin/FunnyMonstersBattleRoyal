using Fusion.Menu;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Players.Presentation
{
    public class PlayerNameUiModule : EntityModule
    {
        [Required] [SerializeField] private UiButton _uiButton;
        [Required] [SerializeField] private TMP_InputField _playerNameInputField;
        [Required] [SerializeField] private TMP_Text _playerNameText;
        [Required] [SerializeField] private FusionMenuPartyCodeGenerator _partyCodeGenerator;

        private void OnEnable() =>
            _uiButton.OnClick += ChangeName;

        protected override void OnAfterDisable() =>
            _uiButton.OnClick -= ChangeName;

        public void InitPlayerName(string text)
        {
            _playerNameText.text = text;
            _playerNameInputField.text = text;
        }

        public void GeneratePlayerName()
        {
            string randomName = $"PlayerName.{Random.Range(0, 9999)}";
            InitPlayerName(randomName);
            Entity.ReplacePlayerName(randomName);
            Entity.AddSaveDataEvent();
        }

        private void ChangeName()
        {
            string newName = _playerNameInputField.text;
            
            //Todo Добавить проверки на вводимые значения
            _playerNameText.text = newName;
            Entity.ReplacePlayerName(newName);
            Entity.AddSaveDataEvent();
        }
    }
}