namespace Gameplay
{
    public interface ISerializable
    {
        void ToPacket(Packet packet);
    }
}
