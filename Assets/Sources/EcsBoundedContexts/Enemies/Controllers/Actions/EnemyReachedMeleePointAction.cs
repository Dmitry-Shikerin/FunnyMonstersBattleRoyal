using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Actions
{
    [Category(NcCategoriesConst.Enemies)]
    public class EnemyReachedMeleePointAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnExecute()
        {
            _entity.AddReachedMeleePoint();
            EndAction();
        }
    }
}