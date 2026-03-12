using Dott;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Bunker.Presentation
{
    public class BunkerUiModule : EntityModule
    {
        [field: Required] [field: SerializeField] public TMP_Text HealthText { get; private set; }
        [field: Required] [field: SerializeField] public DOTweenTimeline HealthAnimation { get; private set; }
        [field: Required] [field: SerializeField] public DOTweenTimeline DamageVignetteAnimation { get; private set; }
        [field: Required] [field: SerializeField] public DOTweenTimeline HealVignetteAnimation { get; private set; }
    }
}