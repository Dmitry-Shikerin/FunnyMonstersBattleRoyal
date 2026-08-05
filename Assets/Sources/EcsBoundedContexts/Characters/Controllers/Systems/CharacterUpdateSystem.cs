using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using NodeCanvas.StateMachines;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.GraphOwners.Domain;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(55)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game, AspectName.Lobby)]
    public class CharacterUpdateSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                FsmOwnerComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                FSMOwner fsmOwner = entity.GetFsmOwner().Value;
                fsmOwner.UpdateBehaviour();
            }
        }
    }
}