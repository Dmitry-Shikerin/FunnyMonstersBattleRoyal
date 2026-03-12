using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.ApplyAbility.Domain.Data;
using Sources.EcsBoundedContexts.ApplyAbility.Infrastructure;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Infrastructure;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Controllers.Data
{
    [EcsSystem(14)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class FlamethrowerAbilityLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly IDataService _dataService;
        private readonly FlamethrowerAbilityEntityFactory _flamethrowerAbilityEntityFactory;
        private readonly AbilityApplierEntityFactory _abilityApplierEntityFactory;
        private readonly RootGameObject _rootGameObject;

        public FlamethrowerAbilityLoadSystem(
            IUiViewService uiViewService,
            IDataService dataService,
            FlamethrowerAbilityEntityFactory flamethrowerAbilityEntityFactory,
            RootGameObject rootGameObject,
            AbilityApplierEntityFactory abilityApplierEntityFactory)
        {
            _uiViewService = uiViewService;
            _dataService = dataService;
            _flamethrowerAbilityEntityFactory = flamethrowerAbilityEntityFactory;
            _rootGameObject = rootGameObject;
            _abilityApplierEntityFactory = abilityApplierEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            ProtoEntity flamethrowerAbilityEntity = _flamethrowerAbilityEntityFactory.Create(_rootGameObject.FlamethrowerAbility);
            GameplayUiView gameplayUiView = _uiViewService.Get<GameplayUiView>();
            _abilityApplierEntityFactory.Create(gameplayUiView.FlamethrowerAbilityApplier, flamethrowerAbilityEntity);

            if (_dataService.HasKey(IdsConst.FlamethrowerAbility) == false)
                return;

            AbilitySaveData data = _dataService.LoadData<AbilitySaveData>(IdsConst.FlamethrowerAbility);

            if (data.IsFirstUsedCompleted == false)
                return;

            flamethrowerAbilityEntity.AddFirstUsedCompleted();
        }
    }
}