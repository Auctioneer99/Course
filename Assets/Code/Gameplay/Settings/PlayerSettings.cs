﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerSettings : IDeserializable, IStateObjectCloneable<PlayerSettings>, ICensored
    {
        public EPlayer Player;
        public PlayerInfo PlayerInfo;
        public BattleDeck BattleDeck;

        public PlayerSettings() : this(EPlayer.Undefined)
        {
        }

        public PlayerSettings(EPlayer player) : this (player, new PlayerInfo())
        {
        }

        public PlayerSettings(EPlayer player, PlayerInfo info)
        {
            Player = player;
            PlayerInfo = info;
            BattleDeck = BattleDeck.Default();
        }

        public PlayerSettings(Packet packet)
        {
            FromPacket(packet);
        }

        public void Censor(EPlayer player)
        {
            BattleDeck.Censor(player);
        }

        public PlayerSettings Clone()
        {
            PlayerSettings settings = new PlayerSettings();
            settings.Copy(this);
            return settings;
        }

        public void Copy(PlayerSettings other)
        {
            PlayerInfo.Copy(other.PlayerInfo);
            BattleDeck.Copy(other.BattleDeck);
        }

        public void FromPacket(Packet packet)
        {
            PlayerInfo = new PlayerInfo(packet);
            BattleDeck = new BattleDeck(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(PlayerInfo)
                .Write(BattleDeck);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[PlayerSettings]");
            sb.Append($"\n\t{Player}");
            sb.Append($"\n\t{PlayerInfo}");
            sb.Append($"\n\t{BattleDeck}");
            return sb.ToString();
        }
    }
}
