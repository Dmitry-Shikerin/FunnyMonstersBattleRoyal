using System;
using System.Collections.Generic;
using Fusion;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Spawners.Infrastructure.Services;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class JoinManager : NetworkBehaviour
    {
        [Required] [SerializeField] private NetworkPrefabRef _playerPrefab;
        [Networked] 
        [Capacity(10)]
        private NetworkDictionary<PlayerRef, NetworkObject> Players => default;
        private readonly Queue<PlayerRef> _joinedQueue = new();

        private NetworkCallbacksReceiver _callbackReceiver;
        private static CharacterFactory _factory;
        private IAssetCollector _assetCollector;
        private NetworkRunner _networkRunner;
        private bool _isInitialized;
        private SpawnPointEntitiesProvider _spawnPointEntitiesProvider;

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

        public override void FixedUpdateNetwork()
        {
            if (_networkRunner.IsClient)
                return;
            
            FreedomQueue();
        }

        public void Initialize()
        {
            _isInitialized = true;
            CreatePlayerEntities();
        }

        private void PlayerJoined(PlayerRef player)
        {
            if (_networkRunner.IsClient)
            {
                //Todo добавить логику создания энтити на клиенте
                return;
            }
            
            _joinedQueue.Enqueue(player);
        }

        private void PlayerLeft(PlayerRef player)
        {
            if (_networkRunner.IsClient)
                return;

            if (Players.Remove(player, out NetworkObject playerObject) == false)
                return;

            _networkRunner.Despawn(playerObject);
        }

        private void FreedomQueue()
        {
            if (_networkRunner.IsClient)
                return;

            if (_isInitialized == false)
                return;

            for (int i = _joinedQueue.Count - 1; i >= 0; i--)
            {
                PlayerRef player = _joinedQueue.Dequeue();
                NetworkObject playerObject = _factory.Create(_playerPrefab, player, _networkRunner);
                Players.Add(player, playerObject);
            }
        }

        private void CreatePlayerEntities()
        {
            if (Runner.IsClient == false)
                return;
            
            foreach (KeyValuePair<PlayerRef, NetworkObject> player in Players)
                _factory.Create(player.Value, player.Key, _networkRunner);
        }

        [Inject]
        private void Construct(CharacterFactory factory, IAssetCollector assetCollector)
        {
            _factory = factory;
            _assetCollector = assetCollector;
        }
    }
}