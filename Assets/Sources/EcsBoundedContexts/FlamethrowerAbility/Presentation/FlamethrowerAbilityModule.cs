using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Presentation
{
    public class FlamethrowerAbilityModule : EntityModule
    {
        [field: Required] [field: SerializeField] public List<Transform> Path { get; private set; }
    }
}