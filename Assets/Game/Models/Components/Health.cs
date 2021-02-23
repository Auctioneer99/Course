using System;

public class Health
{
    public event Action<DamageEvent> BeforeTakeDamage;
    public event Action<DamageEvent> AfterTakeDamage;

    public event Action<ChangeEvent> Changed;

    public int Amount {
        get
        {
            return _amount;
        }
        set
        {
            int old = _amount;
            _amount = value;
            ChangeEvent e = new ChangeEvent(old, _amount);
            Changed?.Invoke(e);
        }
    }

    private int _amount;

    public Health(int amount)
    {
        _amount = amount;
    }

    public void ApplyDamage(DamageEvent e)
    {
        BeforeTakeDamage?.Invoke(e);
        Amount -= e.Damage.Total;
        AfterTakeDamage?.Invoke(e);
    }
}

public class MaxHealth
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

    public Health Health => _health;
    private Health _health;


    public MaxHealth(int value, Health health)
    {
        _value = _max = value;
        _health = health;
        Initialize();
    }

    private void Initialize()
    {
        _health.Changed += MaxHealthWatcher;
        Changed += UpdateCurrentHealth;
    }

    private void MaxHealthWatcher(ChangeEvent e)
    {
        e.FinalValue = Math.Min(e.FinalValue, _max);
    }

    private void UpdateCurrentHealth(ChangeEvent e)
    {
        int delta = e.InitialValue - e.FinalValue;

        if (delta > 0)
        {
            _health.Amount += delta;
        }
        else
        {
            if (e.FinalValue < _health.Amount)
            {
                _health.Amount = e.FinalValue;
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

public class HealthComponent
{
    public event Action Dead;

    public Health Current => _current;
    private Health _current;

    public MaxHealth Max => _max;
    private MaxHealth _max;

    public HealthComponent(int maxValue)
    {
        _current = new Health(maxValue);
        _max = new MaxHealth(maxValue, _current);
        Initialize();
    }

    private void Initialize()
    {
        _current.Changed += DeadWatcher;
    }

    private void DeadWatcher(ChangeEvent e)
    {
        if (e.FinalValue <= 0)
        {
            Dead?.Invoke();
        }
    }
}

public class DamageEvent
{
    public DamageObject Damage => _damage;
    private DamageObject _damage;

    public Unit Invoker => _invoker;
    private Unit _invoker;

    public Unit Target => _target;
    private Unit _target;


    public DamageEvent(DamageObject damage, Unit invoker, Unit target)
    {
        _damage = damage;
        _invoker = invoker;
        _target = target;
    }
}

public class DamageObject
{
    public DamageType Type => _type;
    private DamageType _type;

    public int Base => _base;
    private int _base;

    public int Total => _total;
    public int _total;

    public DamageObject(int amount, DamageType type)
    {
        _base = _total = amount;
        _type = type;
    }
}

public enum DamageType 
{
    Empty
}

public class ChangeEvent
{
    public int InitialValue;
    public int FinalValue;

    public ChangeEvent(int initial, int final)
    {
        InitialValue = initial;
        FinalValue = final;
    }
}