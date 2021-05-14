﻿namespace Gameplay
{
    public enum EAction : int
    {
        Empty,
        ConnectInitialization,
        Ping,

        SetupPlayer,
        DisconnectPlayerAwaitingState,
        PreparePlayer,
        SetupPlayerDeck,

        SyncTimersSettings,

        TimerStarted,
        TimerElapsed,

        SwitchGameState,

        RequestPlayerFinished,
        ReportPlayerStatus,
        SetPlayerStatus,

        AskJoinPlayer,
        AskPrepare,
        AskDisconnectPlayerAwaitingState,

    }

    public static class EActionExtension
    {
        public static Packet Write(this Packet packet, EAction action)
        {
            packet.Write((int)action);
            return packet;
        }

        public static EAction ReadEAction(this Packet packet)
        {
            return (EAction)packet.ReadInt();
        }
    }
}
