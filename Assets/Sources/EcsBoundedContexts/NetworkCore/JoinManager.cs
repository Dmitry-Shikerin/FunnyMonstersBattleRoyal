using System;
using System.Collections.Generic;
using Fusion;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NetworkCore
{
    public class JoinManager : NetworkBehaviour
    {
        [Required] [SerializeField] private NetworkPrefabRef _playerPrefab;
        [Networked] 
        [Capacity(10)]
        private NetworkDictionary<PlayerRef, NetworkObject> Players => default;
        private readonly Queue<PlayerRef> _joinedQueue = new();
        private bool _isJoinedQueueFreedom = false;
        private EcsGameStartUp _ecsGameStartUp;

        private NetworkCallbacksReceiver _callbackReceiver;
        private static CharacterFactory _factory;
        private IAssetCollector _assetCollector;
        private NetworkRunner _networkRunner;

        public override void Spawned()
        {
            _callbackReceiver = NetworkRunnerProvider.NetworkCallbacksReceiver;
            _networkRunner = NetworkRunnerProvider.Runner ?? throw new NullReferenceException("Runner null");
            _callbackReceiver.PlayerJoined += PlayerJoined;
            _callbackReceiver.PlayerLeft += PlayerLeft;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _callbackReceiver.PlayerJoined -= PlayerJoined;
            _callbackReceiver.PlayerLeft -= PlayerLeft;
        }

        public void FreedomQueue()
        {
            if (_networkRunner.IsClient)
                return;

            for (int i = _joinedQueue.Count - 1; i >= 0; i--)
            {
                PlayerRef player = _joinedQueue.Dequeue();
                NetworkObject playerObject = _factory.Create(_playerPrefab, player, _networkRunner);
                Players.Add(player, playerObject);
            }

            _isJoinedQueueFreedom = true;
        }

        public void CreatePlayerEntities()
        {
            if (Runner.IsClient == false)
                return;
            
            foreach (KeyValuePair<PlayerRef, NetworkObject> player in Players)
                _factory.Create(player.Value, player.Key);
        }

        private void PlayerJoined(PlayerRef player)
        {
            if (_networkRunner.IsClient)
                return;

            if (_isJoinedQueueFreedom == false)
            {
                _joinedQueue.Enqueue(player);
                return;
            }
            
            NetworkObject playerObject = _factory.Create(_playerPrefab, player, _networkRunner);
            Players.Add(player, playerObject);

            //BeforePlayerJoined_Rpc(playerObject);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All, InvokeLocal = false)]
        public void BeforePlayerJoined_Rpc(NetworkObject playerObject)
        {
            //_factory.Create(playerObject);
        }

        private void PlayerLeft(PlayerRef player)
        {
            if (_networkRunner.IsClient)
                return;

            if (Players.Remove(player, out NetworkObject playerObject) == false)
                return;

            _networkRunner.Despawn(playerObject);
        }

        [Inject]
        private void Construct(CharacterFactory factory, IAssetCollector assetCollector)
        {
            _factory = factory;
            _assetCollector = assetCollector;
        }
    }
}