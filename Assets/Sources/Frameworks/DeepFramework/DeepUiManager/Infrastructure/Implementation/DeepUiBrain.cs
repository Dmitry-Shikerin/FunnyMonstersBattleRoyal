using MyDependencies.Sources.Containers;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepCores.Presentation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Singletones;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation
{
    //TODO вынести ордеры в константы
    [DefaultExecutionOrder(-5)]
    public class DeepUiBrain : MonoBehaviourSingleton<DeepUiBrain>, IDeepCoreChild
    {
        private StreamSignalBus _signalBus;
        private UiActionHandler _actionHandler;
        private UiViewManager _viewManager;
        private UiPopUpViewManager _popUpViewManager;
        private ButtonsManager _buttonsManager;
        private DeepCore _core;
        private Hud _hud;
        private UiScaler _uiScaler;

        public static StreamSignalBus SignalBus => Instance._signalBus;
        public static UiActionHandler ActionHandler => Instance._actionHandler;
        public static UiViewManager ViewManager => Instance._viewManager;
        public static UiPopUpViewManager PopUpViewManager => Instance._popUpViewManager;
        public static ButtonsManager ButtonsManager => Instance._buttonsManager;
        public static Hud Hud => Instance._hud;
        public static UiScaler UiScaler => Instance._uiScaler;
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            _signalBus = new StreamSignalBus();
            _actionHandler = new UiActionHandler(_signalBus);
            _viewManager = new UiViewManager();
            _popUpViewManager = new UiPopUpViewManager();
            _buttonsManager = new ButtonsManager(_signalBus);
            _uiScaler = new UiScaler();
            InitCore();
        }

        public void Initialize(UiManagerConfig config, Camera mainCamera, DiContainer container)
        {
            _hud = Instantiate(config.Hud);
            Transform parentTransform = _hud.Canvas.transform;

            UniversalAdditionalCameraData cameraData = mainCamera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(_hud.UiCamera);
            _actionHandler.Initialize(container);

            _uiScaler.Initialize(_hud.UiCamera);
            
            foreach (UiView view in config.Views)
                Instantiate(view, parentTransform, false);

            _viewManager.Initialize();
            
            foreach (UiPopUpView popUpView in config.PopUps)
                Instantiate(popUpView, parentTransform, false);

            _popUpViewManager.Initialize();

            _hud.FsmOwner.behaviour = config.Fsm;
            _hud.FsmOwner.StartBehaviour();
        }

        public void Destroy()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            _actionHandler.Destroy();
            _viewManager.Destroy();
            _popUpViewManager.Destroy();
            _buttonsManager.Destroy();
            _signalBus.Release();
        }

        private void InitCore()
        {
            DeepUiBrain[] brains = FindObjectsByType<DeepUiBrain>(FindObjectsSortMode.None);

            foreach (DeepUiBrain manager in brains)
            {
                if (manager == this)
                    continue;

                Destroy(manager.gameObject);
            }

            _core = DeepCore.Instance;
            _core.AddChild(this);
        }
    }
}