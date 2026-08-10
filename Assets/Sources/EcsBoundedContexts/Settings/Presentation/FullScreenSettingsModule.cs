using System;
using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Domain.Data;
using Sources.EcsBoundedContexts.Settings.Presentation.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class FullScreenSettingsModule : EntityModule, ISettingsModule
    {
        [Required] [SerializeField] private UiButton _previousButton;
        [Required] [SerializeField] private UiButton _nextButton;
        [Required] [SerializeField] private TMP_Dropdown _dropdown;

        private readonly int _length = Enum.GetValues(typeof(FullScreenMode)).Length;
        private FullScreenMode _mode = FullScreenMode.ExclusiveFullScreen;
        private int _currentIndex;
        private SettingsConfig _config;

        [Inject]
        private void Construct(IAssetCollector collector)
        {
            _config = collector.Get<SettingsConfig>();
        }

        private void Awake()
        {
            _dropdown.ClearOptions();
            List<string> optionsList = new List<string>()
            {
                "Exclusive Full Screen",
                "FullScreen Window",
                "Maximized Window",
                "Windowed",
            };
            _dropdown.AddOptions(optionsList);
            _dropdown.value = 0;
            _dropdown.RefreshShownValue();
        }

        private void OnEnable()
        {
            _previousButton.OnClick += SetPreviousMode;
            _nextButton.OnClick += SetNextMode;
            _dropdown.onValueChanged.AddListener(ChangeMode);
        }

        protected override void OnAfterDisable()
        {
            _previousButton.OnClick -= SetPreviousMode;
            _nextButton.OnClick -= SetNextMode;
            _dropdown.onValueChanged.AddListener(ChangeMode);
        }

        public void UpdateView()
        {
            FullScreenMode mode = Entity.GetFullScreenMode().Value;
            SetMode(mode);
        }

        public void CancelSettings()
        {
            FullScreenMode mode = Entity.GetSavedSettings().Value.FullScreenMode; 
            SetMode(mode);
        }

        public void ResetToDefaultSettings()
        {
            FullScreenMode mode = _config.FullScreenMode;
            SetMode(mode);
        }

        public void ApplySettings()
        {
            Screen.fullScreenMode = _mode;
        }

        private void ChangeMode(int index)
        {
            _currentIndex = index;
            SetMode();
        }

        private void SetMode(FullScreenMode mode)
        {
            _mode = mode;
            _currentIndex = (int)_mode;
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
        }

        private void SetNextMode()
        {
            _currentIndex++;

            if (_currentIndex >= _length)
                _currentIndex = 0;

            SetMode();
        }

        private void SetPreviousMode()
        {
            _currentIndex--;

            if (_currentIndex <= 0)
                _currentIndex = _length - 1;

            SetMode();
        }

        private void SetMode()
        {
            _mode = (FullScreenMode)_currentIndex;
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
            Entity.ReplaceFullScreenMode(_mode);

            if (Entity.HasChangedSettings())
                return;
            
            Entity.AddChangedSettings();
        }
    }
}