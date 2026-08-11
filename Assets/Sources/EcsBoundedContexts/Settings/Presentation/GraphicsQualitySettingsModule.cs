using System;
using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Domain.Data;
using Sources.EcsBoundedContexts.Settings.Domain.Enums;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.EcsBoundedContexts.Settings.Presentation.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class GraphicsQualitySettingsModule : EntityModule, ISettingsModule
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

        protected override void OnAfterDisable()
        {
            _previousButton.OnClick -= SetPreviousQuality;
            _nextButton.OnClick -= SetNextQuality;
            _dropdown.onValueChanged.AddListener(ChangeQuality);
        }

        public void UpdateView()
        {
            GraphicsQualities quality = Entity.GetGraphicsQuality().Value;
            SetQuality(quality);
        }

        public void CancelSettings()
        {
            GraphicsQualities quality = Entity.GetSavedSettings().Value.GraphicsQuality;
            SetQuality(quality);
        }

        public void ResetToDefaultSettings()
        {
            GraphicsQualities quality = _config.GraphicsQuality;
            SetQuality(quality);
        }

        public void ApplySettings()
        {
            GraphicsQualities quality = Entity.GetGraphicsQuality().Value;
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
            Entity.ReplaceGraphicsQuality(_currentQuality);
            
            if (Entity.HasChangedSettings())
                return;
            
            Entity.AddChangedSettings();
        }
    }
}