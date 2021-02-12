using System.Collections.Generic;
using System.Collections.Immutable;

public class Unit : IUnit
{
    public int Id => _id;
    private int _id;

    public Team Team => _team;
    private Team _team;

    public Attribute Health => _health;
    private Attribute _health;

    public Attribute Attack => _attack;
    private Attribute _attack;

    public Attribute Moves => _moves;
    private Attribute _moves;

    public Attribute Initiative => _initiative;
    private Attribute _initiative;

    public IEnumerable<Ability> Abilities => _abilities.ToImmutableList();
    private List<Ability> _abilities;

    public Unit(Team team, int health, int attack, int moves, int initiative, List<Ability> abilities)
    {
        _team = team;
        _health = new Attribute(health);
        _attack = new Attribute(attack);
        _moves = new Attribute(moves);
        _initiative = new Attribute(initiative);
        _abilities = abilities;
    }
}
