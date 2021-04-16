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

        public NetworkManager(LocalConnector user, int mainsocketid, Logger logger)
        {
            _logger = logger;
            user.Initialize(mainsocketid);
            Host = user;
            Add(user);
        }

        public int IncomingConnection(OnlineConnector connection)
        {
            int id = InitializeConnection(connection);
            _logger.Log("Sending first packet");
            AAction initialization = new ConnectInitializationAction()
                .Initialize(id);
            Send(Host, initialization, id);
            return id;
        }

        public int IncomingConnection(LocalConnector connection)
        {
            return InitializeConnection(connection);
        }

        private int InitializeConnection(AConnector connection)
        {
            int id = ConnectionCounter;
            connection.Initialize(id);
            ConnectionCounter++;
            Add(connection);
            return id;
        }

        public void Add(AConnector connection)
        {
            connection.Manager = this;
            _connectionPool.Add(connection.ConnectionId, connection);
            _logger.Log("Socket setted up " + connection.ConnectionId);

            //if (socket is OnlineSocket oSocket)
            //{
            //    oSocket.OnPacketReceived += OnPacketReceived;
            //}
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
