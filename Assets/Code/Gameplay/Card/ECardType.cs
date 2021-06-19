namespace Gameplay
{
    public enum ECardType
    {
        Undefined = 0,
        Unit = 1 << 0,
        Leader = 1 << 1,
        Spell = 1 << 2,

        All = Unit | Leader | Spell
    }
}
