using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class PlayCardClientCommand : IClientCommand
{
    private int _invoker;
    private int _cardNumber;
    private Vector3 _tile;
    private Card _card;

    public PlayCardClientCommand(int invoker, int cardNumber, Vector3 tile, Card card)
    {
        _invoker = invoker;
        _cardNumber = cardNumber;
        _tile = tile;
        _card = card;
    }

    public void Execute(GameDirector director)
    {
        Player player = director.Players[_invoker];
        player.Mana -= _card.Cost;
        Unit unit = UnitFactory.Warrior();
        director.Playground.AddUnit(unit, director.Playground.TileAt(_tile));
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)ClientPackets.PlayCard);
        packet.Write(_cardNumber);
        packet.Write(_tile);
        packet.Write(_card.Cost);
        packet.Write((int)_card.Unit);
        return packet;
    }
}