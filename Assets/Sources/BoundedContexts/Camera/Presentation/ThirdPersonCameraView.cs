using Sirenix.OdinInspector;
using Sources.BoundedContexts.Hud.Presentations.Common;
using Sources.BoundedContexts.Settings.Presentation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Unity.Cinemachine;
using UnityEngine;

namespace Sources.BoundedContexts.Camera.Presentation
{
    public class ThirdPersonCameraView : MonoBehaviour
    {
        private const string HorizontalAxis = "Look Orbit X";
        private const string VerticalAxis = "Look Orbit Y";
        
        [Required] [SerializeField] private CinemachineCamera _camera;
        [Required] [SerializeField] private CinemachineInputAxisController _axisController;
        
        private MouseSensitivitySettingsViewComponent _sensitivitySettingsView;

        public void Construct(IUiViewService uiViewService)
        {
            gameObject.SetActive(false);
            _sensitivitySettingsView = uiViewService.Get<SettingsUiView>().SettingsView
                .Get<MouseSensitivitySettingsViewComponent>();
            gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            if (_sensitivitySettingsView == null)
                return;

            OnSensitivityChange(_sensitivitySettingsView.Sensitivity);
            _sensitivitySettingsView.OnSensitivityChange += OnSensitivityChange;
        }

        private void OnDisable()
        {
            if (_sensitivitySettingsView == null)
                return;

            _sensitivitySettingsView.OnSensitivityChange -= OnSensitivityChange;
        }

        private void OnSensitivityChange(float value)
        {
            foreach (InputAxisControllerBase<CinemachineInputAxisController.Reader>.Controller controller in _axisController.Controllers)
            {
                if (controller.Name == HorizontalAxis)
                    controller.Input.Gain = value;
                
                if (controller.Name == VerticalAxis)
                    controller.Input.Gain = -value;
            }
        }

        public void SetFollow(Transform followTransform) =>
            _camera.Follow = followTransform;
    }
}