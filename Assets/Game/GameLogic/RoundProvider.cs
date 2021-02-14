using System;
using System.Collections.Generic;
using System.Linq;

public class TurnProvider
{
    private Team _advantage;
    private Team[] _teams;

    private List<IUnit> _originQueue;
    private List<IUnit> _currentQueue;
    private List<IUnit> _delayQueue;

    public TurnProvider(IEnumerable<Team> teams)
    {
        _teams = teams.ToArray();
        Initialize();
    }

    private void Initialize()
    {
        if (_teams.Length == 0)
        {
            throw new Exception("There are no teams specified");
        }
        _advantage = _teams[0];
    }

    public void SetQueue(IEnumerable<IUnit> units)
    {
        _originQueue = units.ToList();
        _currentQueue = units.ToList();
        _delayQueue = new List<IUnit>();
    }

    public void SetDelay(IUnit unit)
    {
        if (_delayQueue.Contains(unit))
        {
            return;
        }
        if (_currentQueue.Contains(unit))
        {
            _currentQueue.Remove(unit);
            _delayQueue.Add(unit);
        }
        else
        {
            throw new Exception("Unit does not exist in queue");
        }
    }

    public IUnit Provide()
    {

        return null;
    }

    public List<IUnit> GetSequence()
    {

        return null;
    }
}
