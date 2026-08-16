using System;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Data;
using Sources.BoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.BoundedContexts.Settings.Presentation.Base;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Settings.Presentation
{
    public class VSyncSettingsViewComponent : SettingsViewComponentBase
    {
        [Required] [SerializeField] private UiToggle _toggle;

        private SettingsConfig _config;
        private IQualityService _qualityService;
        private bool _isVSync;

        public event Action<bool> OnVSyncChanged;
        public event Action<bool> OnVSyncInitialized;
        public event Action<bool> OnVSyncApplyChanges;

        [Inject]
        private void Construct(
            IAssetCollector collector,
            IQualityService qualityService)
        {
            _config = collector.Get<SettingsConfig>();
            _qualityService = qualityService;
        }

        private void OnEnable()
        {
            _toggle.SubscribeStateChange(ChangeVsync);
        }

        protected void OnDisable()
        {
            _toggle.UnsubscribeStateChange(ChangeVsync);
        }

        public override void UpdateView()
        {
            bool hasVSync = SettingsView.Data.IsVSync;
            EnableState state = hasVSync
                ? EnableState.On
                : EnableState.Off;
            _toggle.SetState(state);
            OnVSyncInitialized?.Invoke(hasVSync);
            _qualityService.EnableVSync(hasVSync);
        }

        public override void CancelSettings()
        {
            bool hasVSync = SettingsView.Data.IsVSync;
            EnableState state = hasVSync ? EnableState.On : EnableState.Off;
            ChangeVsync(state);
        }

        public override void ResetToDefaultSettings()
        {
            EnableState state = _config.IsVSync ? EnableState.On : EnableState.Off;
            ChangeVsync(state);
        }

        public override void ApplySettings()
        {
            _qualityService.EnableVSync(_isVSync);
            SettingsView.Data.IsVSync = _isVSync;
            OnVSyncApplyChanges?.Invoke(_isVSync);
        }

        private void ChangeVsync(EnableState state)
        {
            SettingsView.SetChange(true);

            if (state == EnableState.On)
            {
                OnVSyncChanged?.Invoke(true);
                _isVSync = true;
                return;
            }
            
            OnVSyncChanged?.Invoke(false);
            _isVSync = false;
        }
    }
}