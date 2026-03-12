using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Damage.Domain
{
    [Component(group: ComponentGroup.Player)]
    [Serializable]
    [ProtoUnityAuthoring]
    public struct HealthBarComponent
    {
        public Image Value;
    }
}