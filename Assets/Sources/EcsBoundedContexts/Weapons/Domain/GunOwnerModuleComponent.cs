using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Weapons.Presentation;

namespace Sources.EcsBoundedContexts.Weapons.Domain
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct GunOwnerModuleComponent
    {
        public GunOwnerModule Value;
    }
}