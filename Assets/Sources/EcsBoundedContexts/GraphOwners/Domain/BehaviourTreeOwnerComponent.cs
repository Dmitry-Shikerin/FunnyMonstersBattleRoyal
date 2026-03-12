using NodeCanvas.BehaviourTrees;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.GraphOwners.Domain
{
    [Component(group: ComponentGroup.Player)]
    public struct BehaviourTreeOwnerComponent
    {
        public BehaviourTreeOwner Value;
    }
}