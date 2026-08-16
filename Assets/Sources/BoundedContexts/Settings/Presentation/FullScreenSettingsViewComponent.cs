using System;
using System.Collections.Generic;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Data;
using Sources.BoundedContexts.Settings.Presentation.Base;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Settings.Presentation
{
    public class FullScreenSettingsViewComponent : SettingsViewComponentBase
    {
        [Required] [SerializeField] private UiButton _previousButton;
        [Required] [SerializeField] private UiButton _nextButton;
        [Required] [SerializeField] private TMP_Dropdown _dropdown;

        private readonly int _length = Enum.GetValues(typeof(FullScreenMode)).Length;
        private FullScreenMode _mode = FullScreenMode.ExclusiveFullScreen;
        private int _currentIndex;
        private SettingsConfig _config;
        private IScreenService _screenService;

        [Inject]
        private void Construct(
            IAssetCollector collector,
            IScreenService screenService)
        {
            _config = collector.Get<SettingsConfig>();
            _screenService = screenService;
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

        protected void OnDisable()
        {
            _previousButton.OnClick -= SetPreviousMode;
            _nextButton.OnClick -= SetNextMode;
            _dropdown.onValueChanged.AddListener(ChangeMode);
        }

        public override void UpdateView()
        {
            FullScreenMode mode = SettingsView.Data.FullScreenMode;
            SetMode(mode);
        }

        public override void CancelSettings()
        {
            FullScreenMode mode = SettingsView.Data.FullScreenMode;
            SetMode(mode);
        }

        public override void ResetToDefaultSettings()
        {
            FullScreenMode mode = _config.FullScreenMode;
            SettingsView.Data.FullScreenMode = _config.FullScreenMode;
            SetMode(mode);
        }

        public override void ApplySettings()
        {
            SettingsView.Data.FullScreenMode = _config.FullScreenMode;
            _screenService.SetFullScreen(_mode);
        }

        private void ChangeMode(int index)
        {
            _currentIndex = index;
            SetMode();
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

        private void SetMode(FullScreenMode mode)
        {
            _mode = mode;
            _currentIndex = (int)_mode;
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
        }

        private void SetMode()
        {
            _mode = (FullScreenMode)_currentIndex;
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
            SettingsView.SetChange(true);
        }
    }
}