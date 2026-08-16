using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FramerateSettingsViewComponent : SettingsViewComponentBase
    {
        [Required] [SerializeField] private UiButton _previousButton;
        [Required] [SerializeField] private UiButton _nextButton;
        [Required] [SerializeField] private TMP_Dropdown _dropdown;
        [Required] [SerializeField] private VSyncSettingsViewComponent _vSyncViewComponent;
        [Required] [SerializeField] private CanvasGroup _canvasGroup;

        private int _framerate;
        private int _currentIndex;
        private SettingsConfig _config;
        private Dictionary<int, string> _fpsOptions;
        private IScreenService _screenService;

        [Inject]
        private void Construct(
            IAssetCollector collector,
            IScreenService screenService)
        {
            _config = collector.Get<SettingsConfig>();
            _screenService = screenService;
            OnAfterConstruct();
        }

        private void OnAfterConstruct()
        {
            _dropdown.ClearOptions();
            _fpsOptions = GetFpsOptions();
            _dropdown.AddOptions(_fpsOptions.Values.ToList());
            _dropdown.value = 0;
            _dropdown.RefreshShownValue();
        }

        private void OnEnable()
        {
            _previousButton.OnClick += SetPreviousRate;
            _nextButton.OnClick += SetNextFrameRate;
            _dropdown.onValueChanged.AddListener(ChangeFrameRate);
            _vSyncViewComponent.OnVSyncApplyChanges += AfterVSyncApplySettings;
            _vSyncViewComponent.OnVSyncChanged += OnVSyncChanged;
        }

        protected void OnDisable()
        {
            _previousButton.OnClick -= SetPreviousRate;
            _nextButton.OnClick -= SetNextFrameRate;
            _dropdown.onValueChanged.AddListener(ChangeFrameRate);
            _vSyncViewComponent.OnVSyncApplyChanges -= AfterVSyncApplySettings;
            _vSyncViewComponent.OnVSyncChanged -= OnVSyncChanged;
        }

        public override void UpdateView()
        {
            int frameRate = SettingsView.Data.Framerate;
            bool isVSync = SettingsView.Data.IsVSync;
            
            if (isVSync)
            {
                SetFramerate(-1);
                DeactivateFrameRateView();
                return;
            }
            
            SetFramerate(frameRate);
            ActivateFramerateView();
        }

        public override void CancelSettings()
        {
            int frameRate = SettingsView.Data.Framerate;
            SetFramerate(frameRate);
        }

        public override void ResetToDefaultSettings()
        {
            int frameRate = _config.Framerate;
            SettingsView.Data.Framerate = frameRate;
            SetFramerate(frameRate);
        }

        public override void ApplySettings()
        {
            _screenService.SetFramerate(_framerate);
            SettingsView.Data.Framerate = _framerate;
        }

        private void AfterVSyncApplySettings(bool isOn)
        {
            if (isOn == false)
                return;

            _screenService.SetFramerate(-1);
        }

        private void OnVSyncChanged(bool isOn)
        {
            if (isOn)
            {
                SetFramerate(-1);
                DeactivateFrameRateView();
                return;
            }
            
            SetFramerate(_framerate);
            ActivateFramerateView();
        }

        private void ActivateFramerateView()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }

        private void DeactivateFrameRateView()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0.5f;
        }

        private void ChangeFrameRate(int index)
        {
            _currentIndex = index;
            SetFramerateByIndex(_currentIndex);
        }

        private void SetNextFrameRate()
        {
            _currentIndex++;

            if (_currentIndex >= _fpsOptions.Count)
                _currentIndex = 0;

            SetFramerateByIndex(_currentIndex);
        }

        private void SetPreviousRate()
        {
            _currentIndex--;

            if (_currentIndex <= 0)
                _currentIndex = _fpsOptions.Count - 1;

            SetFramerateByIndex(_currentIndex);
        }

        private void SetFramerate(int frameRate)
        {
            _framerate = frameRate;
            _currentIndex = GetIndexByFrameRate(frameRate);
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
        }

        private void SetFramerateByIndex(int index)
        {
            _framerate = GetFrameRateByIndex(index);
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
            SettingsView.SetChange(true);
        }

        private int GetFrameRateByIndex(int index)
        {
            int currentIndex = 0;

            foreach (KeyValuePair<int, string> option in _fpsOptions)
            {
                if (index == currentIndex)
                    return option.Key;

                currentIndex++;
            }

            throw new IndexOutOfRangeException($"Not enough index {index}");
        }

        private int GetIndexByFrameRate(int frameRate)
        {
            int currentIndex = 0;

            foreach (KeyValuePair<int, string> option in _fpsOptions)
            {
                if (frameRate == option.Key)
                    return currentIndex;

                currentIndex++;
            }

            throw new IndexOutOfRangeException($"Not enough index {currentIndex}");
        }

        private Dictionary<int, string> GetFpsOptions()
        {
            List<int> allFpsOptions = new List<int>()
            {
                30,
                60,
                120,
                144,
                240,
                -1,
            };

            int maxFramerate = _screenService.MaxFramerate;
            List<int> result = new List<int>();

            foreach (int option in allFpsOptions)
            {
                if (option > maxFramerate)
                    continue;

                result.Add(option);
            }

            Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

            foreach (int option in result)
            {
                if (option == -1)
                {
                    resultDictionary.Add(-1, "Без ограничений");
                    continue;
                }

                resultDictionary.Add(option, $"{option} FPS");
            }

            return resultDictionary;
        }
    }
}