using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.ExplosionBodies.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Actions
{
    [Category(NcCategoriesConst.Enemies)]
    public class EnemyBomberAction : ActionTask
    {
        private ICameraService _cameraService;
        private ProtoEntity _entity;
        private ExplosionBodyEntityFactory _explosionBodyEntityFactory;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(
            ICameraService cameraService, 
            ExplosionBodyEntityFactory explosionBodyEntityFactory)
        {
            _cameraService = cameraService;
            _explosionBodyEntityFactory = explosionBodyEntityFactory;
        }

        protected override void OnExecute()
        {
            //_cameraService.SetOnTimeCamera(CameraId.Explosion, 1.5f);
            Vector3 spawnPosition = _entity.GetTransform().Value.position + Vector3.up;
            _explosionBodyEntityFactory.Create(spawnPosition);
            _entity.GetReturnToPoolAction().ReturnToPool.Invoke();
            _entity.DelCharacterMeleePoint();
            _entity.DelInitialized();
            _entity.DelTargetPoint();
            _entity.AddExplode();
            _entity.AddDamageEvent(1000);
        }
    }
}