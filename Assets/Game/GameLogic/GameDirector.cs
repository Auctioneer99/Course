using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameDirector
{
    public event Action<GameState> GameStateChanged;
    public event Action<Player> PlayerConnected;
    public event Action<int> RoundChanged;

    public GameState GameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            GameStateChanged?.Invoke(_gameState);
        }
    }
    private GameState _gameState;

    public IDictionary<int, Player> Players => _players;
    private IDictionary<int, Player> _players;

    public int PlayersCount => _playersCount;
    private int _playersCount;

    public Playground Playground => _playground;
    private Playground _playground;

    public TurnProvider TurnProvider => _turnProvider;
    private TurnProvider _turnProvider;

    public int Round 
    {
        get => _round;
        private set
        {
            _round = value;
            RoundChanged?.Invoke(_round);
        }
    }
    private int _round;

    public GameDirector(Playground playground, int playersCount)
    {
        _playground = playground;
        _playersCount = playersCount;
        _players = new Dictionary<int, Player>();
        _gameState = GameState.WaitingPlayers;
        _round = 0;
    }

    public GameDirector() 
    {
        _playground = SimplePlaygroundFactory.NullPlayground();
        _players = new Dictionary<int, Player>();
    }

    public void Copy(GameDirector director)
    {
        _playground.SetField(director.Playground.Tiles.Values);
        _players = director.Players;
        GameState = director.GameState;
        _playersCount = director.PlayersCount;
        Round = director.Round;

        foreach(var p in _players)
        {
            PlayerConnected?.Invoke(p.Value);
        }
    }

    public bool TryAddPlayer(int id, Player player)
    {
        if (_players.Count >= _playersCount)
        {
            return false;
        }
        foreach (var pair in _players)
        {
            if (pair.Value.Team == player.Team)
            {
                return false;
            }
        }

        _players[id] = player;
        PlayerConnected?.Invoke(player);


        if (_players.Count >= _playersCount)
        {
            StartGame();
        }
        return true;
    }

    public void StartGame()
    {
        GameState = GameState.InProgress;

        /*
        CardFactory factory = new CardFactory();

        for (int i = 0; i < 5; i++)
        {
            player.Hand.Add(factory.Token());
        }*/

        //IEnumerable<Team> _teams = _players.Select(p => p.Value.Team);
        //_turnProvider = new TurnProvider(_teams);
    }
}
