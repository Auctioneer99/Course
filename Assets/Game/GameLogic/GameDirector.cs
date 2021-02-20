using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameDirector
{
    public GameState GameState => _gameState;
    private GameState _gameState;

    public IDictionary<int, Player> Players => _players;
    private IDictionary<int, Player> _players;

    private int _playersCount;

    public IPlayground Playground => _playground;
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
        _players = new Dictionary<int, Player>();
        _gameState = GameState.WaitingPlayers;
    }

    public void AddPlayer(int id, Player player)
    {
        if (_players.Count >= _playersCount)
        {
            return;
        }
        foreach (var pair in _players)
        {
            if (pair.Value.Team == player.Team)
            {
                return;
            }
        }

        _players[id] = player;

        if (_players.Count >= _playersCount)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        _gameState = GameState.InProgress;
        IEnumerable<Team> _teams = _players.Select(p => p.Value.Team);
        _turnProvider = new TurnProvider(_teams);
    }

    public IUnit CurrentTurn()
    {
        return _turnProvider.Provide();
    }
}
