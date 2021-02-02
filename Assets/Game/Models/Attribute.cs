using System;

public class Attribute
{
    public event Action<int> BeforeChange;
    public event Action<int> AfterChange;

    private int _current;
    private int _maximum;
    private int _original;

    public int Current
    {
        get
        {
            return _current;
        }
        set
        {
            BeforeChange?.Invoke(value);
            _current += value;
            AfterChange?.Invoke(_current);
        }
    }

    public int Maximum
    {
        get
        {
            return _maximum;
        }
        set
        {
            _maximum = value;
            _current = Math.Min(_current, _maximum);
        }
    }

    public int Original
    {
        get
        {
            return _original;
        }
    }

    public Attribute(int maximum)
    {
        _original = maximum;
        _current = maximum;
        _maximum = maximum;
    }
}
