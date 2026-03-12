using Dott;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.EnemySpawners.Presentation
{
    public class EnemySpawnerUiModule : EntityModule
    {
        [field: Required] [field: SerializeField] public TMP_Text CurrentWaveText { get; private set; }
        [field: Required] [field: SerializeField] public Slider SpawnerProgressSlider { get; private set; }
        [field: Required] [field: SerializeField] public DOTweenTimeline PopUpAnimation { get; private set; }
    }
}