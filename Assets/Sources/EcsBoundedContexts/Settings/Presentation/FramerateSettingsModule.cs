using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FramerateSettingsModule : EntityModule, ISettingsModule
    {
        [Required] [SerializeField] private UiButton _previousButton;
        [Required] [SerializeField] private UiButton _nextButton;
        [Required] [SerializeField] private TMP_Dropdown _dropdown;
        [Required] [SerializeField] private VSyncSettingsModule _vSyncModule;
        [Required] [SerializeField] private CanvasGroup _canvasGroup;

        private int _framerate;
        private int _currentIndex;
        private SettingsConfig _config;
        private Dictionary<int, string> _fpsOptions;

        [Inject]
        private void Construct(IAssetCollector collector)
        {
            _config = collector.Get<SettingsConfig>();
        }

        private void Awake()
        {
            //TODO учесть что framerate зависит еще от разрешения так что зоздавать этот диктионари нужно еще с учетом разрешения
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
            _vSyncModule.OnVSyncApplyChanges += AfterVSyncApplySettings;
            _vSyncModule.OnVSyncChanged += OnVSyncChanged;
        }

        protected override void OnAfterDisable()
        {
            _previousButton.OnClick -= SetPreviousRate;
            _nextButton.OnClick -= SetNextFrameRate;
            _dropdown.onValueChanged.AddListener(ChangeFrameRate);
            _vSyncModule.OnVSyncApplyChanges -= AfterVSyncApplySettings;
            _vSyncModule.OnVSyncChanged -= OnVSyncChanged;
        }

        public void UpdateView()
        {
            int frameRate = Entity.GetFramerate().Value;
            SetFramerate(frameRate);
            bool isVSync = Entity.HasVSync();
            
            if (isVSync)
            {
                SetFramerate(-1);
                DeactivateFrameRateView();
                Entity.ReplaceFramerate(-1);
                return;
            }
            
            SetFramerate(frameRate);
            ActivateFramerateView();
        }

        public void CancelSettings()
        {
            int frameRate = Entity.GetSavedSettings().Value.Framerate;
            SetFramerate(frameRate);
        }

        public void ResetToDefaultSettings()
        {
            int frameRate = _config.Framerate;
            SetFramerate(frameRate);
        }

        public void ApplySettings()
        {
            Application.targetFrameRate = _framerate;
        }

        private void AfterVSyncApplySettings(bool isOn)
        {
            if (isOn == false)
                return;

            Application.targetFrameRate = -1;
        }

        private void OnVSyncChanged(bool isOn)
        {
            if (isOn)
            {
                SetFramerate(-1);
                DeactivateFrameRateView();
                Entity.ReplaceFramerate(-1);
                return;
            }

            int frameRate = Entity.GetFramerate().Value;
            SetFramerate(frameRate);
            ActivateFramerateView();
            Entity.ReplaceFramerate(frameRate);
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
            _currentIndex = GetIndexByFrameRate(frameRate);
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
        }

        private void SetFramerateByIndex(int index)
        {
            _framerate = GetFrameRateByIndex(index);
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
            Entity.ReplaceFramerate(_framerate);
            AddChangeSettings();
        }

        private void AddChangeSettings()
        {
            if (Entity.HasChangedSettings())
                return;

            Entity.AddChangedSettings();
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

            int maxFramerate = Screen.currentResolution.refreshRate;
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