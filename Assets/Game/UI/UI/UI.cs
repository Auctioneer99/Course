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

    public UnitUI UnitUI => _unitUI;

    public GameDirector Director
    {
        get => _gameDirector;
        set 
        {
            _gameDirector = value;
            _gameDirector.RoundChanged += ChangeRound;
            _gameDirector.GameStateChanged += ChangeGameState;
            ChangeRound(_gameDirector.Round);
            ChangeGameState(_gameDirector.GameState);
        }
    }

    private GameDirector _gameDirector;
    public Client Client { get; set; }

    private void Awake()
    {
        UIProvider.Register(this);
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
