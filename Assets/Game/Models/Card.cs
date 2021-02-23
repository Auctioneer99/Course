public class Card
{
    public UnitEnum Unit => _unit;
    private UnitEnum _unit;

    public int Cost => _cost;
    private int _cost;

    public Card(int cost, UnitEnum unit)
    {
        _cost = cost;
        _unit = unit;
    }
}

public enum UnitEnum
{
    Token
}

public class CardFactory
{
    public Card Token()
    {
        return new Card(0, UnitEnum.Token);
    }
}
