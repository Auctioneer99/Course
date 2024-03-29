﻿using System;

namespace Gameplay
{
    // divide this enum to EPlayer and ERole
    public enum EPlayer : byte
    {
        Undefined = 0,
        Player1 = 1 << 0,
        Player2 = 1 << 1,
        Player3 = 1 << 2,
        Neutral = 1 << 5,
        Spectators = 1 << 6,
        Server = 1 << 7,

        Players = Player1 | Player2 | Player3,

        NonAuthority = Players | Spectators
    }

    public static class EPlayerExtensions
    {
        public static EPlayer GetEPlayer(this int value)
        {
            return (EPlayer)value; 
        }

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
