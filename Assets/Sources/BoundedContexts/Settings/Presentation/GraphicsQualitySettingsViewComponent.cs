using System;
using System.Collections.Generic;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Data;
using Sources.BoundedContexts.Settings.Domain.Enums;
using Sources.BoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.BoundedContexts.Settings.Presentation.Base;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Settings.Presentation
{
    public class GraphicsQualitySettingsViewComponent : SettingsViewComponentBase
    {
        [Required] [SerializeField] private UiButton _previousButton;
        [Required] [SerializeField] private UiButton _nextButton;
        [Required] [SerializeField] private TMP_Dropdown _dropdown;

        private readonly int _length = Enum.GetValues(typeof(GraphicsQualities)).Length;
        private int _currentIndex;
        private GraphicsQualities _currentQuality;
        private SettingsConfig _config;
        private IQualityService _qualityService;

        [Inject]
        private void Construct(
            IAssetCollector assetCollector,
            IQualityService qualityService)
        {
            _config = assetCollector.Get<SettingsConfig>();
            _qualityService = qualityService;
        }
        
        private void Awake()
        {
            List<string> options = new List<string>();
            
            foreach (GraphicsQualities quality in Enum.GetValues(typeof(GraphicsQualities)))
                options.Add(quality.ToString());

            _dropdown.ClearOptions();
            _dropdown.AddOptions(options);
            _dropdown.value = 0;
        }
        
        private void OnEnable()
        {
            _previousButton.OnClick += SetPreviousQuality;
            _nextButton.OnClick += SetNextQuality;
            _dropdown.onValueChanged.AddListener(ChangeQuality);
        }

        protected void OnDisable()
        {
            _previousButton.OnClick -= SetPreviousQuality;
            _nextButton.OnClick -= SetNextQuality;
            _dropdown.onValueChanged.AddListener(ChangeQuality);
        }

        public override void UpdateView()
        {
            GraphicsQualities quality = SettingsView.Data.GraphicsQuality;
            SetQuality(quality);
        }

        public override void CancelSettings()
        {
            GraphicsQualities quality = SettingsView.Data.GraphicsQuality;
            SetQuality(quality);
        }

        public override void ResetToDefaultSettings()
        {
            GraphicsQualities quality = _config.GraphicsQuality;
            SettingsView.Data.GraphicsQuality = quality;
            SetQuality(quality);
        }

        public override void ApplySettings()
        {
            GraphicsQualities quality = _currentQuality;
            SettingsView.Data.GraphicsQuality = quality;
            _qualityService.SetQualityLevel(quality);
        }

        private void ChangeQuality(int index)
        {
            _currentIndex = index;
            SetQuality(_currentIndex);
        }

        private void SetNextQuality()
        {
            _currentIndex++;
            
            if (_currentIndex >= _length)
                _currentIndex = 0;
            
            SetQuality(_currentIndex);
        }

        private void SetPreviousQuality()
        {
            _currentIndex--;
            
            if (_currentIndex <= 0)
                _currentIndex = _length - 1;
            
            SetQuality(_currentIndex);
        }

        private void SetQuality(GraphicsQualities quality)
        {
            _currentQuality = quality;
            _currentIndex = (int)quality;
            _dropdown.value = _currentIndex;
            _dropdown.RefreshShownValue();
        }
        
        private void SetQuality(int index)
        {
            _dropdown.value = index;
            _dropdown.RefreshShownValue();
            _currentQuality = (GraphicsQualities)index;
            SettingsView.SetChange(true);
        }
    }
}