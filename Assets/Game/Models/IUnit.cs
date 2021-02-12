using System.Collections.Generic;

public interface IUnit
{
    int Id { get; }

    Team Team { get; }

    Attribute Health { get; }
    Attribute Attack { get; }
    Attribute Moves { get; }
    Attribute Initiative { get; }

    IEnumerable<Ability> Abilities { get; }
}
