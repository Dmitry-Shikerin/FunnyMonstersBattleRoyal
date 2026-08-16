using System;
using System.Collections.Generic;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Data;
using Sources.BoundedContexts.Settings.Presentation.Base;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Settings.Presentation
{
    public class ResolutionSettingsViewComponent : SettingsViewComponentBase
    {
        [Required] [SerializeField] private UiButton _previousButton;
        [Required] [SerializeField] private UiButton _nextButton;
        [Required] [SerializeField] private TMP_Dropdown _dropdown;

        private readonly List<Resolution> _resolutions = new ();
        private int _currentIndex;
        private Resolution _currentResolution;
        private IScreenService _screenService;

        [Inject]
        private void Construct(IScreenService screenService)
        {
            _screenService = screenService;
        }
        
        private void Awake()
        {
            Resolution[] availableResolutions = Screen.resolutions;
            List<string> options = new List<string>();
            
            foreach (Resolution res in availableResolutions)
            {
                _resolutions.Add(res);
                options.Add($"{res.width}x{res.height}");
            }
            
            _dropdown.ClearOptions();
            _dropdown.AddOptions(options);
            Resolution currentResolution = Screen.currentResolution;
            _dropdown.value = GetIndexByResolution(currentResolution);
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
            ResolutionSaveData resolutionData = SettingsView.Data.Resolution;
            Resolution resolution = GetResolution(resolutionData.Width, resolutionData.Height);
            SetResolution(resolution);
        }

        public override void CancelSettings()
        {
            ResolutionSaveData resolutionData = SettingsView.Data.Resolution;
            Resolution resolution = GetResolution(resolutionData.Width, resolutionData.Height);
            SetResolution(resolution);
        }

        public override void ResetToDefaultSettings()
        {
            Resolution defaultResolution = Screen.currentResolution;
            SettingsView.Data.Resolution = new ResolutionSaveData()
            {
                Height = defaultResolution.height,
                Width = defaultResolution.width,
                RefreshRate = defaultResolution.refreshRate,
            };
            SetResolution(defaultResolution);
        }

        public override void ApplySettings()
        {
            RefreshRate refreshRate = new RefreshRate
            {
                numerator = (uint)_currentResolution.refreshRate,
                denominator = 1,
            };
            _screenService.SetResolution(_currentResolution.width, _currentResolution.height, refreshRate);
            SettingsView.Data.Resolution = new ResolutionSaveData()
            {
                Height = _currentResolution.height,
                Width = _currentResolution.width,
                RefreshRate = _currentResolution.refreshRate,
            };
        }

        private void ChangeMode(int index)
        {
            _currentIndex = index;
            SetResolution();
        }

        private void SetNextMode()
        {
            _currentIndex++;
            
            if (_currentIndex >= _resolutions.Count)
                _currentIndex = 0;
            
            SetResolution();
        }

        private void SetPreviousMode()
        {
            _currentIndex--;
            
            if (_currentIndex <= 0)
                _currentIndex = _resolutions.Count - 1;
            
            SetResolution();
        }

        private void SetResolution(Resolution resolution)
        {
            _currentResolution = resolution;
            _currentIndex = GetIndexByResolution(resolution);
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
            SettingsView.SetChange(true);
        }    
        
        private void SetResolution()
        {
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
            Resolution resolution = GetResolutionByIndex(_currentIndex);
            _currentResolution = resolution;
            SettingsView.SetChange(true);
        }
        
        private Resolution GetResolution(int width, int height)
        {
            foreach (Resolution resolution in _resolutions)
            {
                if (resolution.width != width)
                    continue;

                if (resolution.height != height)
                    continue;

                return resolution;
            }

            throw new NullReferenceException($"Not available resolution {width}x{height}");
        }
        
        private Resolution GetResolutionByIndex(int targetIndex)
        {
            for (int i = 0; i < _resolutions.Count; i++)
            {
                if (i != targetIndex)
                {
                    continue;
                }

                return _resolutions[i];
            }

            throw new NullReferenceException($"Not available resolution by index {targetIndex}");
        }

        private int GetIndexByResolution(Resolution currentResolution)
        {
            int index = 0;
            
            foreach (Resolution resolution in _resolutions)
            {
                if (resolution.width != currentResolution.width)
                {
                    index++;
                    continue;
                }

                if (resolution.height != currentResolution.height)
                {
                    index++;
                    continue;
                }

                return index;
            }

            throw new NullReferenceException($"Not available resolution {currentResolution.width}x{currentResolution.height} @{currentResolution.refreshRate} FPS");
        }
    }
}