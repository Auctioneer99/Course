using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameDirector
{
    public IEnumerable<Player> Players => _players;
    private IEnumerable<Player> _players;

    private int _playersCount;

    private IPlayground _playground;

    private TurnProvider _turnProvider;

    private int _round = 0;

    public GameDirector(IPlayground playground, int playersCount)
    {
        _playground = playground;
        _playersCount = playersCount;
        Initialize();
    }

    private void Initialize()
    {
        _players = new List<Player>();
    }

    public Player AddPlayer()
    {

        return null;
    }

    public IUnit CurrentTurn()
    {
        return _turnProvider.Provide();
    }

    public void StartGame()
    {
        IEnumerable<Team> _teams = _players.Select(p => p.Team);
        _turnProvider = new TurnProvider(_teams);
    }
}
