using System;
using System.Collections.Generic;
using Fusion;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Networks.Core;
using Sources.BoundedContexts.Networks.Infrastructure.Services;
using Unity.Mathematics;
using UnityEngine;

namespace Sources.BoundedContexts.Networks
{
    public class JoinManager : MonoBehaviour
    {
        [Required] [SerializeField] private NetworkPrefabRef _playerPrefab;
        private readonly Dictionary<PlayerRef, NetworkObject> _players = new();
        
        private NetworkRunner _runner;
        private NetworkCallbacksReceiver _callbackReceiver;

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

        private void PlayerJoined(PlayerRef player)
        {
            Debug.Log($"Join Player");
            
             if (_runner.IsServer == false)
                 return;
            
             Debug.Log($"Join Player");
             NetworkObject playerObject = _runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, player);
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
    }
}