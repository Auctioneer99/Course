using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp.Server;

namespace Gameplay
{
    public class NetworkManager
    {
        public LocalConnector Host { get; private set; }

        private Dictionary<int, AConnector> _connectionPool = new Dictionary<int, AConnector>();

        private Queue<NetworkMessage> _messages = new Queue<NetworkMessage>();

        public int ConnectionCounter { get; private set; } = 1;

        private Logger _logger;

        public NetworkManager(LocalConnector user, int mainsocketid, EPlayer group, Logger logger)
        {
            _logger = logger;
            user.Initialize(mainsocketid, group);
            Host = user;
            Add(user);
        }

        public int IncomingConnection(AConnector connection)
        {
            int id = InitializeConnection(connection, EPlayer.Spectators);
            _logger.Log("Sending first packet");
            Snapshot snapshot = Snapshot.Create(Host.Controller, EPlayer.Spectators);
            AAction initialization = new ConnectInitializationAction()
                .Initialize(id, EPlayer.Spectators, snapshot);
            SendToTarget(Host, initialization, id);
            //Send(Host, initialization, id);
            return id;
        }

        private int InitializeConnection(AConnector connection, EPlayer group)
        {
            int id = ConnectionCounter;
            connection.Initialize(id, group);
            ConnectionCounter++;
            Add(connection);
            return id;
        }

        public void Add(AConnector connection)
        {
            connection.Manager = this;
            _connectionPool.Add(connection.ConnectionId, connection);
            _logger.Log("Socket setted up " + connection.ConnectionId);
        }

        public void SendToHost(AConnector sender, AAction action)
        {
            NetworkMessage message = new NetworkMessage(sender, action, Host.ConnectionId);
            _messages.Enqueue(message);
        }

        private void OnPacketReceived(int sender, AAction action)
        {
           // SendToTarget(sender, packet, MainSocket.Id);
        }

        public EPlayer GetPlayerTypeFromConnectionId(int connection)
        {
            return _connectionPool[connection].Role;
        }
        public void SendToGroup(AConnector sender, AAction action, EPlayer group)
        {
            int[] receivers = _connectionPool
                .Where(connection => group.Contains(connection.Value.Role))
                .Select(connection => connection.Key)
                .ToArray();
            Debug.Log("[Local Connector] sending packet " + action.ToString() + " : " + string.Join(", ", receivers));
            _messages.Enqueue(new NetworkMessage(sender, action, receivers));
        }

        public void SendToTarget(AConnector sender, AAction action, int target)
        {
            _messages.Enqueue(new NetworkMessage(sender, action, target));
        }

        public void SendToTargets(AConnector sender, AAction action, int[] targets)
        {
            _messages.Enqueue(new NetworkMessage(sender, action, targets));
        }

        public void SendExeptTarget(AConnector sender, AAction action, int target)
        {
            _messages.Enqueue(new NetworkMessage(sender, action, _connectionPool.Keys
                .Except(new List<int>() { target })
                .ToArray()));
        }

        public void SendExeptTargets(AConnector sender, AAction action, int[] targets)
        {
            _messages.Enqueue(new NetworkMessage(sender, action, _connectionPool.Keys
                .Except(targets)
                .ToArray()));
        }

        public void Update()
        {
            foreach(var connector in _connectionPool.Values)
            {
                connector.Update();
            }

            if (_messages.Count > 0)
            {
                UpdateMessages();
            }
        }

        private void UpdateMessages()
        {
            NetworkMessage message = _messages.Dequeue();
            Send(message.Sender, message.Action, message.Receivers);
        }

        private void Send(AConnector sender, AAction action, params int[] targets)
        {
            foreach (int target in targets)
            {
                _connectionPool[target].HandleMessage(sender, action);
            }
        }
    }
}
