public interface IClientCommand : ICommand
{
    void Execute(GameDirector director);
}
