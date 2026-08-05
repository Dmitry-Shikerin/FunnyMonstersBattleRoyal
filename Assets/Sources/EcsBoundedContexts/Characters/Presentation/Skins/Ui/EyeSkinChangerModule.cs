using System;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins.Ui
{
    public class EyeSkinChangerModule : EntityModule
    {
        [Required] [SerializeField] private UiButton _leftButton;
        [Required] [SerializeField] private UiButton _rightButton;
        [Required] [SerializeField] private TMP_Text _nameText;

        private EyeSkinName _currentSkinName = EyeSkinName.Eye01;
        private int _currentIndex = 1;

        protected override void OnAfterInitialize()
        {
            _nameText.text = _currentSkinName.ToString();
        }

        private void OnEnable()
        {
            _leftButton.OnClick += SetPreviousSkin;
            _rightButton.OnClick += SetNextSkin;
        }

        protected override void OnAfterDisable()
        {
            _leftButton.OnClick += SetPreviousSkin;
            _rightButton.OnClick += SetNextSkin;
        }

        private void SetNextSkin()
        {
            // Переход к следующему скину
            _currentIndex++;
            
            if (_currentIndex >= Enum.GetValues(typeof(EyeSkinName)).Length)
                _currentIndex = 1; // Зацикливаем
        
            _currentSkinName = (EyeSkinName)_currentIndex;
            _nameText.text = _currentSkinName.ToString();
            Entity.GetCharacterSkinChangerModule().Value.SetEyeSkin(_currentSkinName);
        }

        private void SetPreviousSkin()
        {
            // Переход к предыдущему скину
            _currentIndex--;
            
            if (_currentIndex <= 0)
                _currentIndex = Enum.GetValues(typeof(HeadSkinName)).Length - 1; // Зацикливаем
        
            _currentSkinName = (EyeSkinName)_currentIndex;
            _nameText.text = _currentSkinName.ToString();
            Entity.GetCharacterSkinChangerModule().Value.SetEyeSkin(_currentSkinName);
        }
    }
}