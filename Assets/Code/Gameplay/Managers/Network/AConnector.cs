namespace Gameplay
{
    public abstract class AConnector
    {
        public int ConnectionId { get; private set; }

        public EPlayer Role { get; protected set; }

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

        public void Send(AAction action, int connectionId)
        {
            Manager.SendToTarget(this, action, connectionId);
        }

        public abstract void Update();

        public abstract void Send(AAction action);

        public abstract void GetDecks();

        public abstract void HandleMessage(AConnector sender, AAction action);

        public override string ToString()
        {
            return $@"IsConnected = {IsConnected}
                ConnectionId = {ConnectionId}
                Role = {Role}";
        }
    }
}
