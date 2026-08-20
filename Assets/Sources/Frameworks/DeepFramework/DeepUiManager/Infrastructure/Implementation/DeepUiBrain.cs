using System;
using Cysharp.Threading.Tasks;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using Reflex.Core;
using Reflex.Injectors;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepCores.Presentation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Curtains.Implementation;
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
        private CurtainView _curtainView;
        private UiScaler _uiScaler;

        public static StreamSignalBus SignalBus => Instance._signalBus;
        public static UiActionHandler ActionHandler => Instance._actionHandler;
        public static UiViewManager ViewManager => Instance._viewManager;
        public static UiPopUpViewManager PopUpViewManager => Instance._popUpViewManager;
        public static ButtonsManager ButtonsManager => Instance._buttonsManager;
        public static Hud Hud => Instance._hud;
        public static CurtainView CurtainView => Instance._curtainView;
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
            _curtainView = Instantiate(Resources.Load<CurtainView>(CurtainView.AssetPath), transform, true);
            InitCore();
        }

        public async UniTask  Initialize(string assetPath, Camera mainCamera, Container container)
        {
            UiManagerConfig config = await Resources.LoadAsync<UiManagerConfig>(assetPath) as UiManagerConfig;

            if (config == null)
                throw new NullReferenceException("UiManagerConfig is null");
            
            _hud = Instantiate(config.Hud);
            Transform uiParentTransform = _hud.Canvas.transform;

            UniversalAdditionalCameraData cameraData = mainCamera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(_hud.UiCamera);
            _actionHandler.Initialize(container);

            _uiScaler.Initialize(_hud.UiCamera);

            //Views
            foreach (UiView viewPrefab in config.Views)
            {
                UiView view = Instantiate(viewPrefab, uiParentTransform, false);
                
                AttributeInjector.Inject(view, container);
                
                foreach (MonoBehaviour mono in view.InjectedMonoBehaviours)
                    AttributeInjector.Inject(mono, container);
            }

            _viewManager.Initialize();

            //PopUp
            foreach (UiPopUpView popUpViewPrefab in config.PopUps)
            {
                UiPopUpView view = Instantiate(popUpViewPrefab, uiParentTransform, false);
                
                AttributeInjector.Inject(view, container);
                
                foreach (MonoBehaviour mono in view.InjectedMonoBehaviours)
                    AttributeInjector.Inject(mono, container);
            }

            _popUpViewManager.Initialize();

            //FSM
            FSMOwner fsmOwner = Instantiate(config.FsmOwner, _hud.transform, false);
            FSM behaviour = fsmOwner.behaviour;
            behaviour.Initialize(behaviour.agent, behaviour.blackboard, true, false);
            InjectOwner(fsmOwner, container);
            fsmOwner.StartBehaviour();
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

        private static void  InjectOwner<T>(GraphOwner<T> owner, Container container)
            where T : Graph
        {
            foreach (FSMState state in owner.behaviour.GetAllNodesOfType<FSMState>())
                AttributeInjector.Inject(state, container);
            
            foreach (Task task in owner.behaviour.GetAllTasksOfType<Task>())
                AttributeInjector.Inject(task, container);
            
            foreach (BehaviourTree graph in owner.behaviour.GetAllNestedGraphs<BehaviourTree>(true))
            {
                foreach (Task task in graph.GetAllTasksOfType<Task>())
                    AttributeInjector.Inject(task, container);
            }
        }
    }
}