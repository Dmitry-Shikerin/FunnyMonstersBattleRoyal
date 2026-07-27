using System.Collections.Generic;
using Fusion;
using Unity.Mathematics;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Infrastructure
{
    public class JoinManager : SimulationBehaviour, IPlayerJoined, IPlayerLeft
    {
        private readonly Dictionary<PlayerRef, NetworkObject> _players = new();
        
        public void PlayerJoined(PlayerRef player)
        {
            // if (Runner.IsServer == false)
            //     return;
            
            // NetworkObject playerObject = Runner.Spawn(_playerPrefab, Vector3.zero, quaternion.identity, player);
            //_players.Add(player, playerObject);
            Debug.Log($"Player Join");
        }

        public void PlayerLeft(PlayerRef player)
        {
            // if (Runner.IsServer == false)
            //     return;
            //
            // if (_players.Remove(player, out NetworkObject playerObject) == false)
            //     return;
            //
            // Runner.Despawn(playerObject);
            Debug.Log($"Player Left");
        }
    }
}