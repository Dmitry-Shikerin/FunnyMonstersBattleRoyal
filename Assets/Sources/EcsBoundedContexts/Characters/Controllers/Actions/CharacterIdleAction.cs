using System.ComponentModel;
using Leopotam.EcsProto;
using NodeCanvas.Framework;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [ParadoxNotion.Design.Category(NcCategoriesConst.Characters)]
    public class CharacterIdleAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnExecute()
        {
            _entity.DelInitialized();
            _entity.AddFindEnemies();
            
            AnimationName animationName = _entity.GetCharacterType().Value switch
            {
                CharacterType.Melee => AnimationName.SwordIdle,
                CharacterType.Range => AnimationName.IdleAssaultRifle,
                _ => throw new InvalidEnumArgumentException()
            };
            
            _entity.PlayAnimation(animationName);
        }

        protected override void OnStop(bool interrupted)
        {
            _entity.DelFindEnemies();
        }
    }
}