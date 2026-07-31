using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class NetworkCallbacksReceiver : MonoBehaviour, INetworkRunnerCallbacks
    {
        public event Action<NetworkRunner, NetworkInput> OnPopulateInput;
        public event Action<PlayerRef> PlayerJoined; 
        public event Action<PlayerRef> PlayerLeft; 
        
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) =>
            PlayerJoined?.Invoke(player);

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) =>
            PlayerLeft?.Invoke(player);

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ReadOnlySpan<byte> data) { }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

        public void OnInput(NetworkRunner runner, NetworkInput input) =>
            OnPopulateInput?.Invoke(runner, input);

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        public void OnConnectedToServer(NetworkRunner runner) { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        public void OnSceneLoadDone(NetworkRunner runner) { }

        public void OnSceneLoadStart(NetworkRunner runner) { }
    }
}