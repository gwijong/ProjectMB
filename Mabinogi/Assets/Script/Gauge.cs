using UnityEngine;

/// <summary> 각종 게이지들</summary>
public class Gauge
{
    private float _current;  //현재 수치
    private float _max;  //최대 수치
    private float _fillableRate = 1.0f; // 채울 수 있는 최대 비율

    public Gauge(float value = 0.0f, float fillable = 1.0f)
    {
        _max = value;
        _current = fillable * _max;
    }

    /// <summary> 수치가 비었는지 체크</summary>
    public bool IsEmpty { get { return _current <= 0; } } 

    /// <summary> 현재 남은 비율</summary>
    public float Rate
    {
        get
        {
            return _current / _max;
        }
        set
        {
            if (value > _fillableRate) value = _fillableRate; //1을 넘지 못 하도록 예외 처리

            value = Mathf.Clamp(value, 0, 1);
            _current = _max * value;
        }
    }
    /// <summary> 현재 수치</summary>
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

    /// <summary> 최대 수치</summary>
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
            if (_current > calculateFillable) _current = calculateFillable; //넘지 못 하도록 예외 처리
        }
    }

    /// <summary> 채울 수 있는 비율</summary>
    public float FillableRate
    {
        get
        {
            return _fillableRate;
        }

        set
        {
            if (_fillableRate > 1.0f) _fillableRate = 1.0f;  //넘지 못 하도록 예외 처리
            else _fillableRate = value;

            float calculateFillable = _max * _fillableRate;
            if (_current > calculateFillable) _current = calculateFillable; //넘지 못 하도록 예외 처리
        }
    }
}