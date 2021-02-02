using System.Collections.Generic;
using System.Collections.Immutable;

public class Unit
{
    public int Id => _id;
    private int _id;

    public Team Team => _team;
    private Team _team;

    public Attribute Health;
    public Attribute Attack;
    public Attribute Moves;
    public Attribute Initiative;

    public IEnumerable<Ability> Abilities => _abilities.ToImmutableList();
    private List<Ability> _abilities;

    public Unit(Team team, int health, int attack, int moves, int initiative, List<Ability> abilities)
    {
        _team = team;
        Health = new Attribute(health);
        Attack = new Attribute(attack);
        Moves = new Attribute(moves);
        Initiative = new Attribute(initiative);
        _abilities = abilities;
    }
}
