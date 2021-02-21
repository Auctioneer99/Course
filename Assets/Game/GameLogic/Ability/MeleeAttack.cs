using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack
{
    public event Action<AbilityData> BeforeUse;
    public event Action<AbilityData> AfterUse;

    public void Use(AbilityData data, Unit invoker, Unit target)
    {
        DamageObject damage = new DamageObject(invoker.Attack.Max, DamageType.Empty);
        DamageEvent e = new DamageEvent(damage, invoker, target);

        BeforeUse?.Invoke(data);

        e.Target.Health.Current.ApplyDamage(e);
        data.Reload.Unprepare();

        AfterUse?.Invoke(data);

    }
}

public class MultiShot
{
    public event Action<AbilityData> BeforeUse;
    public event Action<AbilityData> AfterUse;

    public void Use(AbilityData data, Unit invoker, Unit[] targets)
    {
        BeforeUse?.Invoke(data);

        int attack = invoker.Attack.Max / 2;

        foreach (Unit u in targets)
        {
            DamageObject damage = new DamageObject(attack, DamageType.Empty);
            DamageEvent e = new DamageEvent(damage, invoker, u);
            e.Target.Health.Current.ApplyDamage(e);
        }
        data.Reload.Unprepare();

        AfterUse?.Invoke(data);
    }
}

public class UseEvent
{
    public Ability Ability;

}