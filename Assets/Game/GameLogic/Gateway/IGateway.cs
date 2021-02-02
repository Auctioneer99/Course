using System;

public interface IGateway
{
    int ClientId { get; }

    event Action<int, ICommand> Received;

    void Initialize(int id);

    void Send(ICommand command);
}
