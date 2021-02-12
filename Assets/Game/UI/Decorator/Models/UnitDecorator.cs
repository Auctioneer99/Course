using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDecorator : IUnitDecorator
{
    public IUnit Unit => _unit;
    private IUnit _unit;

    public int Id => _unit.Id;

    public Team Team => _unit.Team;

    public Attribute Health => _unit.Health;

    public Attribute Attack => _unit.Attack;

    public Attribute Moves => _unit.Moves;

    public Attribute Initiative => _unit.Initiative;

    public IEnumerable<Ability> Abilities => _unit.Abilities;

    public UnitDecorator(IUnit unit)
    {
        _unit = unit;
    }
}
