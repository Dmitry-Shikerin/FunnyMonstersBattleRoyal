using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.ApplyAbility.Domain.Data;
using Sources.EcsBoundedContexts.ApplyAbility.Infrastructure;
using Sources.EcsBoundedContexts.CharacterSpawner.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Controllers.Data
{
    [EcsSystem(0)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class CharacterSpawnerLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly IDataService _dataService;
        private readonly CharacterSpawnerEntityFactory _characterSpawnerEntityFactory;
        private readonly RootGameObject _rootGameObject;
        private readonly AbilityApplierEntityFactory _abilityApplierEntityFactory;

        public CharacterSpawnerLoadSystem(
            IUiViewService uiViewService,
            IDataService dataService,
            CharacterSpawnerEntityFactory characterSpawnerEntityFactory,
            RootGameObject rootGameObject,
            AbilityApplierEntityFactory abilityApplierEntityFactory)
        {
            _uiViewService = uiViewService;
            _dataService = dataService;
            _characterSpawnerEntityFactory = characterSpawnerEntityFactory;
            _rootGameObject = rootGameObject;
            _abilityApplierEntityFactory = abilityApplierEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            ProtoEntity characterSpawnerEntity = _characterSpawnerEntityFactory.Create(_rootGameObject.CharacterSpawner);
            GameplayUiView gameplayUiView = _uiViewService.Get<GameplayUiView>();
            _abilityApplierEntityFactory.Create(gameplayUiView.CharacterSpawnerAbilityApplier, characterSpawnerEntity);

            if (_dataService.HasKey(IdsConst.CharacterSpawnerAbility) == false)
                return;

            AbilitySaveData data = _dataService.LoadData<AbilitySaveData>(IdsConst.CharacterSpawnerAbility);

            if (data.IsFirstUsedCompleted == false)
                return;

            characterSpawnerEntity.AddFirstUsedCompleted();
        }
    }
}