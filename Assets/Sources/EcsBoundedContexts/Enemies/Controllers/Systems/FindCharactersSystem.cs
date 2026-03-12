using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Systems
{
    [EcsSystem(63)]
    [ComponentGroup(ComponentGroup.Enemy)]
    [Aspect(AspectName.Game)]
    public class FindCharactersSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                FindCharactersComponent>());
        [DI] private readonly ProtoItExc _charactersIt = new(
            It.Inc<
                CharacterTag>(),
            It.Exc<
                InPoolComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                Vector3 enemyPosition = entity.GetTransform().Value.position;

                int index = 0;
                int len = _charactersIt.Len();
                
                foreach (ProtoEntity characterEntity in _charactersIt)
                {
                    index++;

                    //TODO подумать еще над логикой
                    Vector3 position = characterEntity.GetTransform().Value.position;
                    float distance = Vector3.Distance(enemyPosition, position);
                    
                    if (entity.HasTargetCharacter())
                    {
                        ProtoEntity targetCharacter = entity.GetTargetCharacter().Value;
                        Vector3 targetCharacterPosition = targetCharacter.GetTransform().Value.position;

                        if (Vector3.Distance(enemyPosition, targetCharacterPosition) < distance)
                            continue;
                            
                        entity.ReplaceTargetCharacter(characterEntity);
                    }
                    
                    //TODO move to config
                    if (distance > 3f)
                        continue;
                    
                    if (index == len && entity.HasTargetCharacter())
                    {
                        entity.DelFindCharacters();
                        return;
                    }

                    if (entity.HasTargetCharacter() == false)
                        entity.AddTargetCharacter(characterEntity);
                }
            }
        }
    }
}