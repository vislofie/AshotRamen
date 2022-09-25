public class Effect
{
    /// <summary>
    /// Effect parameters
    /// </summary>
    public Effect(float value, float duration)
    {
        Value = value;
        _duration = duration;
    }

    public virtual float Value { get; private set; }

    protected float _duration;
    public virtual float Duration 
    {
        get
        {
            return _duration;
        }
        set
        {
            if (value <= _duration) _duration = value;
        }
    }
}