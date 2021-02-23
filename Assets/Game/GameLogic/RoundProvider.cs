using System;
using System.Collections.Generic;
using System.Linq;

public class TurnProvider
{
    private Team _advantage;
    private Team[] _teams;

    private List<Unit> _originQueue;
    private List<Unit> _currentQueue;
    private List<Unit> _delayQueue;

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

    public void SetQueue(IEnumerable<Unit> units)
    {
        _originQueue = units.ToList();
        _currentQueue = units.ToList();
        _delayQueue = new List<Unit>();
    }

    public void SetDelay(Unit unit)
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

    public Unit Provide()
    {
        return _currentQueue.FirstOrDefault(u => u.Moves.Current.Amount != 0);
    }

    public List<Unit> GetSequence()
    {

        return null;
    }
}
