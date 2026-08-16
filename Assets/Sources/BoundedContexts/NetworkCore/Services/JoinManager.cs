using System;
using System.Collections.Generic;
using Fusion;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Spawners.Infrastructure.Services;
using UnityEngine;

namespace Sources.BoundedContexts.NetworkCore.Services
{
    public class JoinManager : NetworkBehaviour
    {
        [Required] [SerializeField] private NetworkPrefabRef _playerPrefab;
        [Networked]
        [Capacity(10)]
        [OnChangedRender(nameof(ChangePlayers))]
        private NetworkDictionary<PlayerRef, NetworkObject> Players => default;
        private readonly Queue<PlayerRef> _joinedQueue = new();
        private readonly List<NetworkObject> _playersObjects = new();

        private NetworkCallbacksReceiver _callbackReceiver;
        private static CharacterFactory _factory;
        private bool _isInitialized;
        private SpawnPointEntitiesProvider _spawnPointEntitiesProvider;

        public IReadOnlyList<NetworkObject> PlayersObjects => GetPlayers();
        
        public event Action OnPlayersChanged;

        [Inject]
        private void Construct(CharacterFactory factory)
        {
            _factory = factory;
        }

        public override void Spawned()
        {
            _callbackReceiver = NetworkRunnerProvider.NetworkCallbacksReceiver;
            _callbackReceiver.PlayerJoined += PlayerJoined;
            _callbackReceiver.PlayerLeft += PlayerLeft;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _callbackReceiver.PlayerJoined -= PlayerJoined;
            _callbackReceiver.PlayerLeft -= PlayerLeft;
        }

        public override void FixedUpdateNetwork() =>
            ServerFreedomQueue();

        public void Update() =>
            ClientFreedomQueue();

        public void Initialize()
        {
            FillClientQueue();
            _isInitialized = true;
        }

        //На клиенте не работает FixedUpdate если он не его InputAuthority
        private void ClientFreedomQueue()
        {
            if (_isInitialized == false)
                return;
            
            if (Runner.IsClient == false)
                return;
            
            for (int i = _joinedQueue.Count - 1; i >= 0; i--)
            {
                PlayerRef player = _joinedQueue.Dequeue();

                if (Players.TryGet(player, out NetworkObject playerObject) == false)
                    return;
                
                _factory.ClientCreate(playerObject, player, Runner);
                Players.Add(player, playerObject);
            }
        }     
        
        private void ServerFreedomQueue()
        {
            if (Runner.IsClient)
                return;
            
            if (_isInitialized == false)
                return;

            for (int i = _joinedQueue.Count - 1; i >= 0; i--)
            {
                PlayerRef player = _joinedQueue.Dequeue();
                NetworkObject playerObject = _factory.ServerCreate(_playerPrefab, player, Runner);
                Players.Add(player, playerObject);
            }
        }
        
        private void PlayerJoined(PlayerRef player)
        {
            if (Runner.IsClient)
            {
                if (_isInitialized)
                    _joinedQueue.Enqueue(player);
                
                return;
            }
            
            _joinedQueue.Enqueue(player);
        }

        private void PlayerLeft(PlayerRef player)
        {
            if (Runner.IsClient)
                return;

            if (Players.Remove(player, out NetworkObject playerObject) == false)
                return;

            Runner.Despawn(playerObject);
        }

        private void FillClientQueue()
        {
            if (Runner.IsClient == false)
                return;

            foreach (KeyValuePair<PlayerRef, NetworkObject> player in Players)
                _joinedQueue.Enqueue(player.Key);
        }

        private List<NetworkObject> GetPlayers()
        {
            _playersObjects.Clear();
            
            foreach (KeyValuePair<PlayerRef, NetworkObject> player in Players)
                _playersObjects.Add(player.Value);

            return _playersObjects;
        }
        
        private void ChangePlayers()
        {
            GetPlayers();
            Debug.Log($"Change players, Count {Players.Count}");
            OnPlayersChanged?.Invoke();
        }
    }
}