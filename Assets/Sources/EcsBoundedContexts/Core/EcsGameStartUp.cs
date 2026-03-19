using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.EcsBoundedContexts.Damage.Domain;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.Volumes.Domain.Components;

namespace Sources.EcsBoundedContexts.Core
{
    public class EcsGameStartUp : IEcsGameStartUp
    {
        private readonly DiContainer _container;
        private readonly ProtoSystems _systems;
        private readonly ProtoWorld _world;
        private readonly GameAspect _aspect;
        private readonly ISystemsCollector _systemsCollector;
        private ProtoSystems _unitySystems;
        private bool _isInitialize;

        public EcsGameStartUp(
            DiContainer container, 
            ProtoWorld protoWorld,
            ProtoSystems systems,
            GameAspect aspect,
            ISystemsCollector systemsCollector)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _world = protoWorld ?? throw new ArgumentNullException(nameof(protoWorld));
            _systems = systems ?? throw new ArgumentNullException(nameof(systems));
            _aspect = aspect ?? throw new ArgumentNullException(nameof(aspect));
            _systemsCollector = systemsCollector ?? throw new ArgumentNullException(nameof(systemsCollector));
        }

        public async void Initialize()
        {
            InitUnitySystems();
            //await UniTask.Yield();
            AddModules();
            _systemsCollector.AddSystems();
            AddOneFrame();
            _systems.Init();
            Init();
        }

        public void Update(float deltaTime)
        {
            if (_isInitialize == false)
                return;
            
            _unitySystems?.Run();
            _systems?.Run();
        }

        public void Destroy()
        {
            _systems?.Destroy();
            _unitySystems?.Destroy();
        }

        private void AddModules()
        {
            _systems.AddModule(new AutoInjectModule());
        }

        private void AddOneFrame()
        {
            _systems.DelHere<CompleteMoveAlongPathEvent>();
            _systems.DelHere<CompleteMoveAlongPathPointEvent>();
            _systems.DelHere<SaveDataEvent>();
            _systems.DelHere<ClearDataEvent>();
            _systems.DelHere<EnableGameObjectEvent>();
            _systems.DelHere<DisableGameObjectEvent>();
            _systems.DelHere<IncreaseEvent>();
            _systems.DelHere<DecreaseEvent>();
            _systems.DelHere<MuteVolumeEvent>();
            _systems.DelHere<UnmuteVolumeEvent>();
            _systems.DelHere<InitializeEvent>();
            _systems.DelHere<DamageEvent>();
            _systems.DelHere<CoinsChangedEvent>();
            _systems.DelHere<IncreaseCoinsEvent>();
            _systems.DelHere<DecreaseCoinsEvent>();
            _systems.DelHere<ApplyDailyRewardEvent>();
            _systems.DelHere<ChangeVolumeEvent>();
        }
        
        private async void Init()
        {
            //await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _isInitialize = true;
        }

        private void InitUnitySystems()
        {
            _unitySystems = new ProtoSystems(_world);
            _unitySystems
                .AddModule(new AutoInjectModule())
                .AddModule(new UnityModule())
                .Init();
            // _rootGameObject
            //     .GetComponentsInChildren<ProtoUnityAuthoring>()
            //     .ForEach(authoring => authoring.ProcessAuthoring());
        }
    }
}
