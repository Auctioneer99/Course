namespace Gameplay
{
    public enum ELocation : short
    {
        Undefined = 0,
        Hand = 1 << 0,
        Deck = 1 << 1,
        Graveyard = 1 << 2,
        Mulligan = 1 << 3,
        Field = 1 << 4,



    }
}
