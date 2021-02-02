public class Player
{
    public string Name => _name;
    private string _name;

    public Team Team => _team;
    private Team _team;

    public Player(string Name, Team team)
    {
        _name = Name;
        _team = team;
    }
}
