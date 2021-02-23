using System;
public class MoveComponent
{
    public Move Current => _current;
    private Move _current;

    public MaxMove Max => _max;
    private MaxMove _max;

    public MoveComponent(int maxValue)
    {
        _current = new Move(maxValue);
        _max = new MaxMove(maxValue, _current);
        Initialize();
    }

    private void Initialize()
    {

    }
}

public class Move
{
    public event Action<MoveEvent> BeforeMove;
    public event Action<MoveEvent> AfterMove;

    public event Action<ChangeEvent> Changed;

    public int Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            ChangeEvent e = new ChangeEvent(_amount, value);
            _amount = value;
            Changed?.Invoke(e);
        }
    }

    private int _amount;

    public Move(int amount)
    {
        _amount = amount;
    }

    public void MakeMove(MoveEvent data)
    {
        BeforeMove?.Invoke(data);

        Amount -= data.Amount;

        AfterMove?.Invoke(data);
    }

}

public class MoveEvent
{
    public int Amount;
    public Unit Owner;
    public Tile Origin;
    public Tile End;
}

public class MaxMove
{
    public event Action<ChangeEvent> Changed;

    public event Action<ChangeEvent> BeforeGet
    {
        add
        {
            int oldV = _max;
            _beforeGet += value;
            int newV = UpdateMaxValue();
            ChangeEvent e = new ChangeEvent(newV, oldV);
            Changed?.Invoke(e);
        }
        remove
        {
            int oldV = _max;
            _beforeGet -= value;
            int newV = UpdateMaxValue();
            ChangeEvent e = new ChangeEvent(newV, oldV);
            Changed?.Invoke(e);
        }
    }
    private event Action<ChangeEvent> _beforeGet;

    public int Max => _max;
    private int _max;

    public int Origin => _value;
    private int _value;

    public Move Move => _move;
    private Move _move;


    public MaxMove(int value, Move move)
    {
        _value = value;
        _move = move;
        Initialize();
    }

    private void Initialize()
    {
        _move.Changed += MaxMoveWatcher;
        Changed += UpdateCurrentMove;
    }

    private void MaxMoveWatcher(ChangeEvent e)
    {
        e.FinalValue = Math.Min(e.FinalValue, _max);
    }

    private void UpdateCurrentMove(ChangeEvent e)
    {
        int delta = e.InitialValue - e.FinalValue;

        if (delta > 0)
        {
            _move.Amount += delta;
        }
        else
        {
            if (e.FinalValue < _move.Amount)
            {
                _move.Amount = e.FinalValue;
            }
        }
    }

    private int UpdateMaxValue()
    {
        ChangeEvent e = new ChangeEvent(_value, _value);
        _beforeGet?.Invoke(e);
        return e.FinalValue;
    }
}
