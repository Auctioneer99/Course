using System;
using System.Collections.Generic;

public class Player
{
    public event Action OnEndPlayingCards;
    public event Action<ChangeEvent> OnManaChange;
    public event Action OnDrawCard;
    public event Action<Card> OnCardAdd;

    public string Name => _name;
    private string _name;

    public int Mana
    {
        get => _mana;
        set
        {
            ChangeEvent e = new ChangeEvent(_mana, value);
            _mana = value;
            OnManaChange?.Invoke(e);
        }
    }
    private int _mana;

    public Team Team => _team;
    private Team _team;

    public List<Card> Hand = new List<Card>();
    public List<Card> Deck = new List<Card>();

    public Player(string Name, Team team)
    {
        _name = Name;
        _team = team;
    }

    public void EndPlayingCards()
    {
        OnEndPlayingCards?.Invoke();
    }

    public void DrawCard()
    {
        OnDrawCard?.Invoke();
    }

    public void AddCard(Card card)
    {
        Hand.Add(card);
        OnCardAdd?.Invoke(card);
    }
}
