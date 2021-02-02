public interface ICommand
{
    void Execute();

    Packet ToPacket();
}
