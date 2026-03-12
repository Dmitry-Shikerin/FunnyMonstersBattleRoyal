using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Move.Components
{
    [Component(group: ComponentGroup.Player)]
    public struct CharacterControllerComponent
    {
        public CharacterController Value;
    }
}