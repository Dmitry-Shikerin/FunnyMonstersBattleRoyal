using System.Collections.Generic;
using Fusion;
using Unity.Mathematics;
using UnityEngine;

namespace Sources.BoundedContexts.Networks
{
    public class PlayerJoinManager : SimulationBehaviour, IPlayerJoined, IPlayerLeft
    {
        [SerializeField] private NetworkPrefabRef _playerPrefab;

        private readonly Dictionary<PlayerRef, NetworkObject> _players = new();
        
        public void PlayerJoined(PlayerRef player)
        {
            if (Runner.IsServer == false)
                return;
            
            NetworkObject playerObject = Runner.Spawn(_playerPrefab, Vector3.zero, quaternion.identity, player);
            _players.Add(player, playerObject);
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (Runner.IsServer == false)
                return;

            if (_players.Remove(player, out NetworkObject playerObject) == false)
                return;
            
            Runner.Despawn(playerObject);
        }
    }
}