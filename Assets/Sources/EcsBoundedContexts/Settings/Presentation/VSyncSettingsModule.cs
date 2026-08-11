using System;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Domain.Data;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using Sources.EcsBoundedContexts.Settings.Presentation.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class VSyncSettingsModule : EntityModule, ISettingsModule
    {
        [Required] [SerializeField] private UiToggle _toggle;

        private SettingsConfig _config;
        private IQualityService _qualityService;

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
            _toggle.StateChanged += ChangeVsync;
        }

        protected override void OnAfterDisable()
        {
            _toggle.StateChanged += ChangeVsync;
        }

        public void UpdateView()
        {
            bool hasVSync = Entity.HasVSync();
            EnableState state = hasVSync
                ? EnableState.On
                : EnableState.Off;
            _toggle.SetState(state);
            OnVSyncInitialized?.Invoke(hasVSync);
            _qualityService.EnableVSync(hasVSync);
        }

        public void CancelSettings()
        {
            EnableState state = Entity.GetSavedSettings().Value.IsVSync
                ? EnableState.On
                : EnableState.Off;
            ChangeVsync(state);
        }

        public void ResetToDefaultSettings()
        {
            EnableState state = _config.IsVSync
                ? EnableState.On
                : EnableState.Off;
            ChangeVsync(state);
        }

        public void ApplySettings()
        {
            bool hasVSync = Entity.HasVSync();
            _qualityService.EnableVSync(hasVSync);
            OnVSyncApplyChanges?.Invoke(hasVSync);
        }

        private void ChangeVsync(EnableState state)
        {
            AddChangeSettings();

            if (state == EnableState.On)
            {
                OnVSyncChanged?.Invoke(true);
                Entity.AddVSync();
                return;
            }
            
            OnVSyncChanged?.Invoke(false);
            Entity.DelVSync();
        }

        private void AddChangeSettings()
        {
            if (Entity.HasChangedSettings())
                return;

            Entity.AddChangedSettings();
        }
    }
}