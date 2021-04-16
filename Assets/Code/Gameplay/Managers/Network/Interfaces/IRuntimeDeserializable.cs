namespace Gameplay
{
    public interface IRuntimeDeserializable : ISerializable
    {
        void FromPacket(GameController controller, Packet packet);
    }
}
