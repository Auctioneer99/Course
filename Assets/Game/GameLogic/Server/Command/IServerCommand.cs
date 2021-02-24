public interface IServerCommand : IPacketable
{
    ServerPackets Command { get; }
    void Execute(int invoker, Server server);
}
