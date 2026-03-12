using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Ability)]
    public struct CharactersSpawnPointsComponent
    {
        public List<ProtoEntity> MeleeSpawnPoints;
        public List<ProtoEntity> RangeSpawnPoints;
    }
}