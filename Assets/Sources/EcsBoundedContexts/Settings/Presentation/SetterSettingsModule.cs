using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Presentation.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class SetterSettingsModule : EntityModule
    {
        [Required] [SerializeField] private UiButton _resetToDefaultButton;
        [Required] [SerializeField] private UiButton _mainMenuButton;
        [field: Required] [field: SerializeField] public UiButton ApplyButton { get; private set; }

        private void OnEnable()
        {
            ApplyButton.OnClick += ApplySettings;
            _resetToDefaultButton.OnClick += ResetToDefaultSettings;
            _mainMenuButton.OnClick += CancelSettings;
        }

        protected override void OnAfterDisable()
        {
            ApplyButton.OnClick -= ApplySettings;
            _resetToDefaultButton.OnClick -= ResetToDefaultSettings;
            _mainMenuButton.OnClick -= CancelSettings;
        }

        protected override void OnAfterInitialize()
        {
            ApplyButton.gameObject.SetActive(false);
        }

        private void CancelSettings()
        {
            IReadOnlyList<EntityModule> modules = Link.GetModules();

            foreach (EntityModule module in modules)
            {
                if (module is not ISettingsModule concrete)
                    continue;
                
                concrete.CancelSettings();
            }

            if (Entity.HasChangedSettings())
                Entity.DelChangedSettings();
            
            ApplyButton.gameObject.SetActive(false);
            Entity.AddSaveDataEvent();
            Debug.Log($"Cancel Settings");
        }

        private void ResetToDefaultSettings()
        {
            IReadOnlyList<EntityModule> modules = Link.GetModules();

            foreach (EntityModule module in modules)
            {
                if (module is not ISettingsModule concrete)
                    continue;
                
                concrete.ResetToDefaultSettings();
            }

            if (Entity.HasChangedSettings())
                Entity.DelChangedSettings();
            
            ApplyButton.gameObject.SetActive(false);
            Entity.AddSaveDataEvent();
            Debug.Log($"Reset to default");
        }

        private void ApplySettings()
        {
            ApplyButton.gameObject.SetActive(false);
            Entity.DelChangedSettings();
            Entity.AddSaveDataEvent();
            Debug.Log($"Apply settings");
        }
    }
}