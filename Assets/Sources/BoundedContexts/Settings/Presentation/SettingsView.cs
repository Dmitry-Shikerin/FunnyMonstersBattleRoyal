using System;
using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Data;
using Sources.BoundedContexts.Settings.Presentation.Base;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Settings.Presentation
{
    public class SettingsView : MonoBehaviour
    {
        [Required] [SerializeField] private List<SettingsViewComponentBase> _components;
        [Required] [SerializeField] private UiButton _resetToDefaultButton;
        [Required] [SerializeField] private UiButton _mainMenuButton;

        [field: Required]
        [field: SerializeField]
        public UiButton ApplyButton { get; private set; }

        public SettingsSaveData Data { get; private set; }
        public bool IsChanged { get; private set; }

        private IDataService _dataService;
        private SettingsConfig _config;
        private bool _isUpdated;

        [Inject]
        private void Construct(
            IAssetCollector assetCollector,
            Container container,
            IDataService dataService)
        {
            _dataService = dataService;
            _config = assetCollector.Get<SettingsConfig>();

            foreach (SettingsViewComponentBase component in _components)
                AttributeInjector.Inject(component, container);
            
            Initialize();
        }

        private void OnEnable()
        {
            ApplyButton.OnClick += ApplySettings;
            _resetToDefaultButton.OnClick += ResetToDefaultSettings;
            _mainMenuButton.OnClick += CancelSettings;
        }

        protected void OnDisable()
        {
            ApplyButton.OnClick -= ApplySettings;
            _resetToDefaultButton.OnClick -= ResetToDefaultSettings;
            _mainMenuButton.OnClick -= CancelSettings;
        }

        public void SetChange(bool isChanged)
        {
            if (_isUpdated == false)
                return;
            
            IsChanged = isChanged;
            ApplyButton.gameObject.SetActive(isChanged);
        }

        public T Get<T>() 
            where T : SettingsViewComponentBase
        {
            foreach (SettingsViewComponentBase component in _components)
            {
                if (component is not T concrete)
                    continue;

                return concrete;
            }

            throw new NullReferenceException($"{typeof(T).Name} is no available in {nameof(SettingsView)}");
        }

        private void Initialize()
        {
            ApplyButton.gameObject.SetActive(false);
            LoadSaves();
            InitializeComponents();
        }

        private void CancelSettings()
        {
            foreach (SettingsViewComponentBase component in _components)
                component.CancelSettings();

            SetChange(false);
            Save();
            Debug.Log($"Cancel Settings");
        }

        private void ResetToDefaultSettings()
        {
            foreach (SettingsViewComponentBase component in _components)
                component.ResetToDefaultSettings();

            SetChange(false);
            Save();
            Debug.Log($"Reset to default");
        }

        private void ApplySettings()
        {
            foreach (SettingsViewComponentBase component in _components)
                component.ApplySettings();

            SetChange(false);
            Save();
            Debug.Log($"Apply settings");
        }

        private void LoadSaves()
        {
            if (_dataService.HasKey(IdsConst.Settings) == false)
            {
                Resolution resolution = Screen.currentResolution;

                Data = new SettingsSaveData()
                {
                    Id = IdsConst.Settings,
                    FullScreenMode = _config.FullScreenMode,
                    Framerate = _config.Framerate,
                    Resolution = new ResolutionSaveData()
                    {
                        Height = resolution.height,
                        Width = resolution.width,
                        RefreshRate = resolution.refreshRate,
                    },
                    IsVSync = _config.IsVSync,
                    GraphicsQuality = _config.GraphicsQuality,
                    MusicVolume = _config.MusicVolume,
                    IsMusicMuted = _config.IsMutedMusic,
                    SoundVolume = _config.SoundVolume,
                    IsSoundMuted = _config.IsMutedSound,
                };

                return;
            }

            Data = _dataService.LoadData<SettingsSaveData>(IdsConst.Settings);
        }

        private void Save()
        {
            _dataService.SaveData(Data, IdsConst.Settings);
        }

        private void InitializeComponents()
        {
            foreach (SettingsViewComponentBase component in _components)
            {
                component.Initialize(this);
                component.UpdateView();
            }

            _isUpdated = true;
        }

        [Button]
        private void FillComponents()
        {
            _components  = 
                GetComponentsInChildren<SettingsViewComponentBase>()
                .ToList()
                .Concat(GetComponents<SettingsViewComponentBase>())
                .ToList();
        }
    }
}