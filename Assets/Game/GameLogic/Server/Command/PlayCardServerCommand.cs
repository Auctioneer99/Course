using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class PlayCardServerCommand : IServerCommand
{
    public ServerPackets Command => ServerPackets.PlayCard;

    private int _cardNumber;
    private Vector3 _tile;

    public PlayCardServerCommand(int cardNumber, Vector3 tile)
    {
        _cardNumber = cardNumber;
        _tile = tile;
    }

    public void Execute(int invoker, Server server)
    {
        if (server.GameDirector.GameState == GameState.PlayingCards)
        {
            IClientCommand command = new PlayCardClientCommand(invoker, _cardNumber, _tile, new CardFactory().Token());

            Action<GameState> sendCardInfo = null;
            sendCardInfo = new Action<GameState>((state) =>
            {
                server.GameDirector.GameStateChanged -= sendCardInfo;

                command.Execute(server.GameDirector);

                server.Clients.First(c => c.Id == invoker).IncludedCommands.Remove(command);

                server.Send(command, invoker);
            });

            server.GameDirector.GameStateChanged += sendCardInfo;

            server.Clients.First(c => c.Id == invoker).IncludedCommands.Add(command);

            server.Send(invoker, command);
        }
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)Command);
        packet.Write(_cardNumber);
        packet.Write(_tile);
        return packet;
    }
}
