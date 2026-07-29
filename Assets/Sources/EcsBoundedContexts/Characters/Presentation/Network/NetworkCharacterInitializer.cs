using Fusion;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Network
{
    public class NetworkCharacterInitializer : NetworkBehaviour
    {
        [SerializeField] private EntityLink _entity;
        [SerializeField] private NetworkObject _networkObject;
        private CharacterFactory _factory;

        public override void Spawned()
        {
            if (Runner.IsClient == false)
                return;
            
            //_factory.Create(_networkObject);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
        }

        [Inject]
        private void Construct(CharacterFactory characterFactory)
        {
            _factory = characterFactory;
        }
    }
}