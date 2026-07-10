using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class IsGroundDistanceCondition : ConditionTask
    {
        private IEntityRepository _repository;
        private ProtoEntity _input;
        private ProtoEntity _entity;
        private IAssetCollector _assetCollector;
        private CharacterConfig _config;

        protected override string OnInit()
        {
            _input = _repository.GetByName(IdsConst.Input);
            _config = _assetCollector.Get<CharacterConfig>();
            return null;
        }

        [Inject]
        private void Construct(IEntityRepository repository, IAssetCollector assetCollector)
        {
            _repository = repository;
            _assetCollector = assetCollector;
        }

        [Construct]
        private void Construct(ProtoEntity entity)
        {
            _entity = entity;
        }

        protected override bool OnCheck()
        {
            return _entity.GetGroundDistance().Value <= _config.EndAirDistance;
        }
    }
}