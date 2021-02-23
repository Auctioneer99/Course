using System;

public class InitiativeComponent
{
    public event Action<ChangeEvent> Changed;

    public event Action<ChangeEvent> BeforeGet
    {
        add
        {
            int oldValue = _max;
            _beforeGet += value;
            int newValue = UpdateMaxValue();
            ChangeEvent e = new ChangeEvent(newValue, oldValue);
            Changed?.Invoke(e);
        }
        remove
        {
            int oldValue = _max;
            _beforeGet -= value;
            int newValue = UpdateMaxValue();
            ChangeEvent e = new ChangeEvent(newValue, oldValue);
            Changed?.Invoke(e);
        }
    }
    private event Action<ChangeEvent> _beforeGet;

    public int Max => _max;
    private int _max;

    public int Origin => _value;
    private int _value;

    public InitiativeComponent(int value)
    {
        _value = _max = value;
        Initialize();
    }

    private void Initialize()
    {

    }

    private int UpdateMaxValue()
    {
        ChangeEvent e = new ChangeEvent(_value, _value);
        _beforeGet?.Invoke(e);
        return e.FinalValue;
    }
}
