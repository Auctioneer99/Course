namespace Gameplay
{
    public enum EGameState
    {
        Init = 0,
        Mulligan = 1,
        ChoosePlayer = 2,
        RoundStart = 3,
        RoundEnd = 4,
        TurnStart = 5,
        TurnEnd = 6,
        Turn = 7,
        End = 8,
        PlayingCardStart = 9,
        PlayingCardEnd = 10,
        PlayingCard = 11,

        NonSyncTurnStart = 1 << 11,
    }
}
