using System;

namespace Gameplay
{
    public enum EPlayer : byte
    {
        Undefined = 0,
        Player1 = 1 << 0,
        Player2 = 1 << 1,
        Player3 = 1 << 2,
        Spectator = 1 << 6,

        Players = Player1 | Player2 | Player3,

        All = Players | Spectator
    }

    public static class EPlayerExtensions
    {
        public static Packet Write(this Packet packet, EPlayer player)
        {
            packet.buffer.AddRange(BitConverter.GetBytes((byte)player));
            return packet;
        }

        public static EPlayer ReadEPlayer(this Packet packet)
        {
            return (EPlayer)packet.ReadByte();
        }

        public static bool Contains(this EPlayer origin, EPlayer other)
        {
            return (origin & other) != 0;
        }

        public static bool IsPlayer(this EPlayer player)
        {
            return EPlayer.Players.Contains(player);
        }

        public static bool IsSpectator(this EPlayer player)
        {
            return EPlayer.Spectator.Contains(player);
        }
    }
}
