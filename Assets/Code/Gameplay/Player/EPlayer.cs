using System;

namespace Gameplay
{
    public enum EPlayer : byte
    {
        Undefined = 0,
        Server = 1 << 0,
        Player1 = 1 << 1,
        Player2 = 1 << 2,
        Player3 = 1 << 3,
        Spectators = 1 << 7,

        Players = Player1 | Player2 | Player3,

        NonAuthority = Players | Spectators
    }

    public static class EPlayerExtensions
    {
        public static Packet Write(this Packet packet, EPlayer player)
        {
            packet.Write((byte)player);
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
            return EPlayer.Spectators.Contains(player);
        }
    }
}
