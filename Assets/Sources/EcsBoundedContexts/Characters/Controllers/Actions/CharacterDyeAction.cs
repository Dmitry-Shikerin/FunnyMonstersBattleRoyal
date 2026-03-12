using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterDyeAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnExecute()
        {
            _entity.DelFindEnemies();
            _entity.GetReturnToPoolAction().ReturnToPool.Invoke();
            _entity.DelInitialized();
        }
    }
}