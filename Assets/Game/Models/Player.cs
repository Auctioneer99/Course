using System.Collections.Generic;

public class Player
{
    public string Name => _name;
    private string _name;

    public int Mana
    {
        get => _mana;
        set
        {
            _mana = value;
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
}
