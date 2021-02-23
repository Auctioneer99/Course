using System;

public class AbilityData
{
    public ReloadComponent Reload => _reload;
    private ReloadComponent _reload;

    public Ability Ability => _ability;
    private Ability _ability;

    public AbilityData(Ability ability, int reload)
    {
        _ability = ability;
        _reload = new ReloadComponent(reload);
    }
}

public class ReloadComponent
{
    public event Action<ReloadEvent> BeforeReload;
    public event Action<ReloadEvent> AfterReload;

    public event Action Unprepared;

    public Reload Current => _current;
    private Reload _current;

    public MaxReload Max => _max;
    private MaxReload _max;

    public ReloadComponent(int maxValue)
    {
        _current = new Reload(maxValue);
        _max = new MaxReload(maxValue, _current);
        Initialize();
    }

    private void Initialize()
    {

    }

    public void Update()
    {
        ReloadEvent e = new ReloadEvent(Current.Amount, 1, this);
        BeforeReload?.Invoke(e);
        Current.Amount -= e.Amount;
        AfterReload?.Invoke(e);
    }

    public void Unprepare()
    {
        _current.Amount = _max.Max;
        Unprepared?.Invoke();
    }
}

public class ReloadEvent
{
    public int InitialValue;
    public int Amount;
    public ReloadComponent Component;

    public ReloadEvent(int origin, int amount, ReloadComponent component)
    {
        InitialValue = origin;
        Amount = amount;
        Component = component;
    }
}

public class Reload
{
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

    public Reload(int amount)
    {
        _amount = amount;
    }
}

public class MaxReload
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

    public Reload Reload => _reload;
    private Reload _reload;


    public MaxReload(int value, Reload reload)
    {
        _value = value;
        _reload = reload;
        Initialize();
    }

    private void Initialize()
    {
        _reload.Changed += MaxMoveWatcher;
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
            _reload.Amount += delta;
        }
        else
        {
            if (e.FinalValue < _reload.Amount)
            {
                _reload.Amount = e.FinalValue;
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

public enum Ability
{
    MeleeAttack,
    Move,
    MultiShot
}