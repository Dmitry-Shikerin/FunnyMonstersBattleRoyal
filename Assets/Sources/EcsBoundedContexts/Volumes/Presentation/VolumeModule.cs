using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Volumes.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Steppers;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.DeepFramework.DeepUtils;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Volumes.Presentation
{
    public class VolumeModule : EntityModule
    {
        [Required] [SerializeField] private Button _increaceButton;
        [Required] [SerializeField] private Button _decreaseButton;
        [field: Required] [field: SerializeField] public UiToggle MuteToggle {get; private set; }
        [field: Required] [field: SerializeField] public UiStepper UiStepper { get; set; }
        [field: SerializeField] public VolumeType VolumeType { get; private set; }

        private void OnEnable()
        {
            // if (IsInitialized == false)
            //     return;
            //
            // _increaceButton.onClick.AddListener(OnIncreaseVolume);
            // _decreaseButton.onClick.AddListener(OnDecreaseVolume);
            // MuteToggle.StateChanged += OnMuteToggleStateChanged;
        }

        protected override void OnAfterDisable()
        {
            // _increaceButton.onClick.RemoveListener(OnIncreaseVolume);
            // _decreaseButton.onClick.RemoveListener(OnDecreaseVolume);
            // MuteToggle.StateChanged -= OnMuteToggleStateChanged;
        }

        private void OnMuteToggleStateChanged(EnableState state)
        {
            if (state == EnableState.On)
            {
                Entity.AddUnmuteVolumeEvent();
                return;
            }
            
            Entity.AddMuteVolumeEvent();
        }

        private void OnIncreaseVolume()
        {
            if (Entity.HasIncreaseEvent())
                return;
            
            Entity.AddIncreaseEvent();
        }

        private void OnDecreaseVolume()
        {
            if (Entity.HasDecreaseEvent())
                return;
            
            Entity.AddDecreaseEvent();
        }
    }
}