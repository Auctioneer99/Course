using System.Text;

namespace Gameplay
{
    public abstract class AConnector
    {
        public int ConnectionId { get; private set; }

        public EPlayer Role { get; set; }

        public NetworkManager Manager { get; set; }

        public bool IsConnected { get; private set; }

        public virtual void Initialize(int id, EPlayer role)
        {
            if (IsConnected == false)
            {
                ConnectionId = id;
                Role = role;
                IsConnected = true;
            }
            else
            {
                throw new System.Exception("connector already connected");
            }
        }

        public abstract void Update();

        public abstract void Send(AAction action);

        public abstract void GetDecks();

        public abstract void HandleMessage(AConnector sender, AAction action);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[AConnector]");
            sb.AppendLine($"IsConnected = {IsConnected}");
            sb.AppendLine($"ConnectionId = {ConnectionId}");
            sb.AppendLine($"Role = {Role}");
            return sb.ToString();
        }
    }
}
