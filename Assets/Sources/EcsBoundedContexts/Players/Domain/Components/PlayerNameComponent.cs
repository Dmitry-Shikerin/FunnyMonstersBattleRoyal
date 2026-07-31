using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine.Serialization;

namespace Sources.EcsBoundedContexts.Players.Domain.Components
{
    [Serializable]
    [Component(group: ComponentGroup.Characters)]
    public struct PlayerNameComponent
    {
        [FormerlySerializedAs("Name")] public string Value;
    }
}