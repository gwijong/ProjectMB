using UnityEngine;

public class Gauge
{
    private float _current;
    private float _max;
    private float _fillableRate = 1.0f;

    public Gauge(float value = 0.0f, float fillable = 1.0f)
    {
        _max = value;
        _current = fillable * _max;
    }

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
            if (_fillableRate > 1.0f) _fillableRate = 1.0f;
            else _fillableRate = value;

            float calculateFillable = _max * _fillableRate;
            if (_current > calculateFillable) _current = calculateFillable;
        }
    }
}