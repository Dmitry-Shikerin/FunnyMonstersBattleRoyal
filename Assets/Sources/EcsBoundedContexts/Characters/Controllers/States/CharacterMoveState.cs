using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterMoveState : FSMState
    {
        private ProtoEntity _entity;
        private IEntityRepository _entityRepository;
        private ProtoEntity _inputEntity;

        [Construct]
        private void Construct(ProtoEntity entity)
        {
            _entity = entity;
        }

        [Inject]
        private void Construct(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        protected override void OnInit()
        {
            _inputEntity = _entityRepository.GetByName(IdsConst.Input);
        }

        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.Walk);
        }

        protected override void OnUpdate()
        {
            CharacterController characterController = _entity.GetCharacterController().Value;
            Vector3 direction = _inputEntity.GetDirection().Value * 10;
            //форвард
            Transform transform = _entity.GetTransform().Value;
            transform.forward = direction;
            //гравитация
            direction.y += 3;
            characterController.SimpleMove(direction);
        }
    }
}