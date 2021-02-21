using System.Collections.Generic;

public static class UnitFactory
{
    public static Unit Warrior()
    {
        return new Unit(Team.Red, 100, 20, 3, 10, new List<AbilityData>() { new AbilityData(Ability.MeleeAttack, 1) });
    }
}
