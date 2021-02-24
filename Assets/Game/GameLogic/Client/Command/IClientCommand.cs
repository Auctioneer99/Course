public interface IClientCommand : IPacketable
{
    ClientPackets Command { get; }

    void Execute(GameDirector director);
}
