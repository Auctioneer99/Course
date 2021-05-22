namespace Gameplay
{
    public interface IDeserializable : ISerializable
    {
        void FromPacket(Packet packet);
    }
}
