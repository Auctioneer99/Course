using System.Collections.Generic;
using System.Collections.Immutable;

public class Unit
{
    public int Id => _id;
    private int _id;

    public Team Team => _team;
    private Team _team;

    public HealthComponent Health => _health;
    private HealthComponent _health;

    public AttackComponent Attack => _attack;
    private AttackComponent _attack;

    public MoveComponent Moves => _moves;
    private MoveComponent _moves;

    public InitiativeComponent Initiative => _initiative;
    private InitiativeComponent _initiative;

    public IEnumerable<AbilityData> Abilities => _abilities.ToImmutableList();
    private List<AbilityData> _abilities;

    public Unit(Team team, int health, int attack, int moves, int initiative, List<AbilityData> abilities)
    {
        _team = team;
        _health = new HealthComponent(health);
        _attack = new AttackComponent(attack);
        _moves = new MoveComponent(moves);
        _initiative = new InitiativeComponent(initiative);
        _abilities = abilities;
        Initialize();
    }

    private void Initialize()
    {

    }
}
