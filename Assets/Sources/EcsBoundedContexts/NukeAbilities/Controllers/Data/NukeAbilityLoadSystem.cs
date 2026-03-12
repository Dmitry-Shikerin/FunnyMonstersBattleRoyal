using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.ApplyAbility.Domain.Data;
using Sources.EcsBoundedContexts.ApplyAbility.Infrastructure;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.NukeAbilities.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.NukeAbilities.Controllers.Data
{
    [EcsSystem(11)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class NukeAbilityLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly IDataService _dataService;
        private readonly NukeAbilityEntityFactory _nukeAbilityEntityFactory;
        private readonly RootGameObject _rootGameObject;
        private readonly AbilityApplierEntityFactory _abilityApplierEntityFactory;
        private readonly IEntityRepository _entityRepository;

        public NukeAbilityLoadSystem(
            IUiViewService uiViewService,
            IDataService dataService,
            NukeAbilityEntityFactory nukeAbilityEntityFactory,
            RootGameObject rootGameObject,
            AbilityApplierEntityFactory abilityApplierEntityFactory,
            IEntityRepository entityRepository)
        {
            _uiViewService = uiViewService;
            _dataService = dataService;
            _nukeAbilityEntityFactory = nukeAbilityEntityFactory;
            _rootGameObject = rootGameObject;
            _abilityApplierEntityFactory = abilityApplierEntityFactory;
            _entityRepository = entityRepository;
        }

        public void Init(IProtoSystems systems)
        {
            ProtoEntity nukeAbilityEntity = _nukeAbilityEntityFactory.Create(_rootGameObject.NukeAbility);
            GameplayUiView gameplayUiView = _uiViewService.Get<GameplayUiView>();
            _abilityApplierEntityFactory.Create(gameplayUiView.NukeAbilityApplier, nukeAbilityEntity);

            if (_dataService.HasKey(IdsConst.NukeAbility) == false)
                return;

            AbilitySaveData data = _dataService.LoadData<AbilitySaveData>(IdsConst.NukeAbility);

            if (data.IsFirstUsedCompleted == false)
                return;

            nukeAbilityEntity.AddFirstUsedCompleted();
        }
    }
}