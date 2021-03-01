using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text _round, _gameState;
    [SerializeField]
    private UnitUI _unitUI;
    [SerializeField]
    private PlayerUI _player1, _player2;
    public UnitUI UnitUI => _unitUI;
    [SerializeField]
    private CardsUI _cardsUI;

    public Camera Camera 
    { 
        get
        {
            return _camera;
        }
        set
        {
            _camera = value;
            _cardsUI.Camera = value;
        }
    }
    private Camera _camera;

    public GameDirector Director
    {
        get => _gameDirector;
        set 
        {
            _gameDirector = value;
            _gameDirector.RoundChanged += ChangeRound;
            _gameDirector.GameStateChanged += ChangeGameState;
            _gameDirector.PlayerConnected += AddPlayer;
            _gameDirector.OnLocalPlayerSet += LocalPlayerSet;
            ChangeRound(_gameDirector.Round);
            ChangeGameState(_gameDirector.GameState);
        }
    }

    private GameDirector _gameDirector;
    public Client Client
    {
        get
        {
            return _client;
        }
        set
        {
            _client = value;
            _cardsUI.Client = value;
        }
    }
    private Client _client;

    private void Awake()
    {
        UIProvider.Register(this);
    }

    private void LocalPlayerSet(Player player)
    {
        if (player != null)
        {
            foreach (Card c in player.Hand)
            {
                _cardsUI.AddCard(c);
            }
        }
        player.OnCardAdd += _cardsUI.AddCard;
    }



    private void AddPlayer(Player player)
    {
        if (_player1.Player == null)
        {
            _player1.Player = player;
        }
        else
        {
            _player2.Player = player;
        }
    }

    private void ChangeRound(int value)
    {
        _round.text = value.ToString();
    }

    private void ChangeGameState(GameState state)
    {
        _gameState.text = state.ToString();
    }
}
