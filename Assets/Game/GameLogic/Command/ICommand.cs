public interface ICommand
{
    void Execute(int hostId);

    Packet ToPacket();
}
