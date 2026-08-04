using System;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Reflex.Attributes;
using Reflex.Core;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.EcsBoundedContexts.Damage.Domain;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.Input.Domain;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.Spawners.Infrastructure.Services;
using Sources.EcsBoundedContexts.Volumes.Domain.Components;

namespace Sources.EcsBoundedContexts.Core
{
    public class LeoEcsGameStartUp : IEcsGameStartUp
    {
        private Container _container;
        private ProtoSystems _systems;
        private ProtoWorld _world;
        private GameAspect _aspect;
        private ISystemsCollector _systemsCollector;
        private ProtoSystems _unitySystems;
        private bool _isInitialize;
        private SpawnPointEntitiesProvider _spawnPointEntitiesProvider;

        [Inject]
        private void Construct(
            SpawnPointEntitiesProvider spawnPointEntitiesProvider,
            Container container, 
            ProtoWorld protoWorld,
            ProtoSystems systems,
            GameAspect aspect,
            ISystemsCollector systemsCollector)
        {
            _spawnPointEntitiesProvider = spawnPointEntitiesProvider;
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _world = protoWorld ?? throw new ArgumentNullException(nameof(protoWorld));
            _systems = systems ?? throw new ArgumentNullException(nameof(systems));
            _aspect = aspect ?? throw new ArgumentNullException(nameof(aspect));
            _systemsCollector = systemsCollector ?? throw new ArgumentNullException(nameof(systemsCollector));
        }

        public async UniTask Initialize()
        {
            InitUnitySystems();
            await UniTask.Yield();
            AddModules();
            _systems.AddService(_spawnPointEntitiesProvider);
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
            _systems.AddModule(new AutoInjectModule(true));
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
            _systems.DelHere<JumpEventComponent>();
        }

        private void Init()
        {
            _isInitialize = true;
        }

        private void InitUnitySystems()
        {
            _unitySystems = new ProtoSystems(_world);
            _unitySystems
                .AddModule(new AutoInjectModule())
                .AddModule(new UnityModule())
                .Init();
        }
    }
}
