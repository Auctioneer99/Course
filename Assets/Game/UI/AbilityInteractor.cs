using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilitySelector
{
    public IProvider Provider => _provider;
    private IProvider _provider;

    public AbilityData Active => _active;
    private AbilityData _active;

    private AbilityProviderFactory _providerFactory;

    private Playground _playground;

    public AbilitySelector(Playground playground)
    {
        _playground = playground;
        _providerFactory = new AbilityProviderFactory(_playground);
    }

    public void SetAbility(AbilityData data, Tile origin)
    {
        _active = data;
        _provider = _providerFactory.Create(data.Ability, origin);
    }

    public List<Tile> GetPossibilities()
    {
        return _provider.Provide();
    }
}

public class AbilityProviderFactory
{
    private Playground _playground;
    private Dictionary<Ability, Func<Tile, IProvider>> _abilityProviders;

    public AbilityProviderFactory(Playground playground)
    {
        _playground = playground;
        Initialize();
    }

    public IProvider Create(Ability ability, Tile origin)
    {
        return _abilityProviders[ability](origin);
    }

    private void Initialize()
    {
        _abilityProviders = new Dictionary<Ability, Func<Tile, IProvider>>()
        {
            { Ability.MeleeAttack, MeleeAttack },

        };
    }

    private IProvider MeleeAttack(Tile tile)
    {
        return new MeleeAttackProvider(tile, _playground);
    }
}

public interface IProvider
{
    List<Tile> Provide();

    void Select(Tile tile);
}

public class MeleeAttackProvider : IProvider
{
    private Playground _playground;
    private Tile _origin;
    public MeleeAttackProvider(Tile tile, Playground playground)
    {
        _origin = tile;
        _playground = playground;
    }

    public List<Tile> Provide()
    {
        return _playground
            .TileConnections(_origin)
            .Select(pair => pair.Value)
            .Where(t => t.Unit.Team != _origin.Unit.Team)
            .ToList();
    }

    public void Select(Tile tile)
    {
        IServerCommand command = new MeleeAttackServerCommand(tile);
    }
}

public class MoveSelector
{ 

}

public class MultishotSelector
{
    private int _count = 3;

    private Stack<Tile> _selected;

    public void Select(Tile tile)
    {
        _selected.Push(tile);
        if (_selected.Count == _count)
        {
            //form command
        }
    }

    public Tile Undo()
    {
        return _selected.Pop();
    }
}
