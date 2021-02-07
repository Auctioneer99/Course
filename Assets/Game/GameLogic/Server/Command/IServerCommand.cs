public interface IServerCommand : ICommand
{
    void Execute(int invoker, Server server);
}
