using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Achievements.Presentation
{
    public class AchievementInfoModule : EntityModule
    {
        [field: SerializeField] public Image IconImage { get; private set; }
        [field: SerializeField] public TMP_Text TitleText { get; private set; }
        [field: SerializeField] public TMP_Text DescriptionText { get; private set; }
    }
}