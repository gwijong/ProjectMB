using UnityEngine;

public class Gauge
{
    private float _current;
    private float _max;
    private float _fillableRate;
    public bool IsEmpty { get { return _current <= 0; } }
    public float Rate
    {
        get
        {
            return _current / _max;
        }
        set
        {
            if (value > _fillableRate) value = _fillableRate;

            value = Mathf.Clamp(value, 0, 1);
            _current = _max * value;
        }
    }
    public float Current
    {
        get
        {
            return _current;
        }

        set
        {
            _current = Mathf.Clamp(value, 0, _max * _fillableRate);
        }
    }

    public float Max
    {
        get
        {
            return _max;
        }

        set
        {
            _max = value;

            float calculateFillable = _max * _fillableRate;
            if (_current > calculateFillable) _current = calculateFillable;
        }
    }

    public float FillableRate
    {
        get
        {
            return _fillableRate;
        }

        set
        {
            _fillableRate = value;

            float calculateFillable = _max * _fillableRate;
            if (_current > calculateFillable) _current = calculateFillable;
        }
    }
}