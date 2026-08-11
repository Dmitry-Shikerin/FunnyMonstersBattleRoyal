using System;
using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Domain.Components.Parts;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.EcsBoundedContexts.Settings.Presentation.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class ResolutionSettingsModule : EntityModule, ISettingsModule
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

        protected override void OnAfterDisable()
        {
            _previousButton.OnClick -= SetPreviousMode;
            _nextButton.OnClick -= SetNextMode;
            _dropdown.onValueChanged.AddListener(ChangeMode);
        }

        public void UpdateView()
        {
            ResolutionComponent resolutionComponent = Entity.GetResolution();
            Resolution resolution = GetResolution(resolutionComponent);
            SetResolution(resolution);
        }

        public void CancelSettings()
        {
            ResolutionComponent resolutionComponent = Entity.GetResolution();
            Resolution resolution = GetResolution(resolutionComponent);
            SetResolution(resolution);
        }

        public void ResetToDefaultSettings()
        {
            SetResolution(Screen.currentResolution);
        }

        public void ApplySettings()
        {
            ResolutionComponent resolutionComponent = Entity.GetResolution();
            Resolution resolution = GetResolution(resolutionComponent);
            RefreshRate refreshRate = new RefreshRate
            {
                numerator = (uint)resolution.refreshRate,
                denominator = 1,
            };
            _screenService.SetResolution(resolutionComponent.Width, resolutionComponent.Height, refreshRate);
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
            Entity.ReplaceResolution(resolution.width, resolution.height, resolution.refreshRate);
            
            if (Entity.HasChangedSettings())
                return;
            
            Entity.AddChangedSettings();
        }    
        
        private void SetResolution()
        {
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
            Resolution resolution = GetResolutionByIndex(_currentIndex);
            Entity.ReplaceResolution(resolution.width, resolution.height, resolution.refreshRate);
            
            if (Entity.HasChangedSettings())
                return;
            
            Entity.AddChangedSettings();
        }
        
        private Resolution GetResolution(ResolutionComponent component)
        {
            foreach (Resolution resolution in _resolutions)
            {
                if (resolution.width != component.Width)
                    continue;

                if (resolution.height != component.Height)
                    continue;

                return resolution;
            }

            throw new NullReferenceException($"Not available resolution {component.Width}x{component.Height} @{component.RefreshRate} FPS");
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