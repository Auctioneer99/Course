public interface IClientCommand : ICommand
{
    void Execute(Client client);
}
