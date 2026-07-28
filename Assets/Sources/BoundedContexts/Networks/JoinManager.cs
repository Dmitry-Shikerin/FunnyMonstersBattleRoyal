using System.Collections.Generic;
using Fusion;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Networks.Core;
using Sources.BoundedContexts.Networks.Infrastructure.Services;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Core;
using UnityEngine;

namespace Sources.BoundedContexts.Networks
{
    public class JoinManager : MonoBehaviour
    {
        [Required] [SerializeField] private NetworkPrefabRef _playerPrefab;        
        [Required] [SerializeField] private NetworkPrefabRef _ecsGameStartUpPrefab;
        private readonly Dictionary<PlayerRef, NetworkObject> _players = new();
        private readonly Queue<PlayerRef> _joinedQueue = new ();
        private bool _isJoinedQueueFreedom = false;
        private EcsGameStartUp _ecsGameStartUp;
        
        private NetworkRunner _runner;
        private NetworkCallbacksReceiver _callbackReceiver;
        private CharacterFactory _factory;

        private void Awake()
        {
            _runner = NetworkRunnerProvider.Runner;
            _callbackReceiver = NetworkRunnerProvider.NetworkCallbacksReceiver;
            _callbackReceiver.PlayerJoined += PlayerJoined;
        }

        private void OnDestroy()
        {
            _callbackReceiver.PlayerJoined -= PlayerJoined;
        }

        public void FreedomQueue()
        {
            for (int i = _joinedQueue.Count - 1; i >= 0; i--)
            {
                PlayerRef player = _joinedQueue.Dequeue();
                NetworkObject playerObject = _factory.Create(_playerPrefab, player, _runner);
                _players.Add(player, playerObject);
            }

            _isJoinedQueueFreedom = true;
        }

        private void PlayerJoined(PlayerRef player)
        {
             if (_runner.IsServer == false)
                 return;

             if (_isJoinedQueueFreedom == false)
             {
                 _joinedQueue.Enqueue(player);
                 return;
             }
             
             NetworkObject playerObject = _factory.Create(_playerPrefab, player, _runner);
            _players.Add(player, playerObject);
        }

        private void PlayerLeft(PlayerRef player)
        {
            if (_runner.IsServer == false)
                return;
            
            if (_players.Remove(player, out NetworkObject playerObject) == false)
                return;
            
            _runner.Despawn(playerObject);
        }

        [Inject]
        private void Construct(CharacterFactory factory)
        {
            _factory = factory;
        }
    }
}