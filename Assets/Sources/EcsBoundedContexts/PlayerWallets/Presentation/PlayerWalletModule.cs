using System.Collections.Generic;
using Dott;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.PlayerWallets.Presentation
{
    public class PlayerWalletModule : EntityModule
    {
        [field: Required] [field: SerializeField] public List<TMP_Text> MoneyTexts { get; private set; }
        [field: Required] [field: SerializeField] public DOTweenTimeline ScullAnimation { get; private set; }
    }
}