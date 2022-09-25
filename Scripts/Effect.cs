using UnityEngine;
public abstract class Effect
{
    public Effect(int hpSec, float duration)
    {
        HpSec = hpSec;
        Duration = duration;
    }
    public abstract void EffectValue();

    public abstract int HpSec { get; set; }

    public abstract float Duration { get; set; }
}

public class Buff : Effect
{
    public Buff(int hpSec,float duration) : base(hpSec,duration)
    {
    }

    public override int HpSec { get; set; }

    public override float Duration { get; set; }

    public override void EffectValue()
    {
         Health.PlayerHealth += HpSec;
    }
}

public class DeBuff : Effect
{
    public DeBuff(int hpSec, float duration) : base(hpSec, duration)
    {
    }

    public override int HpSec { get; set; }

    public override float Duration { get; set; }

    public override void EffectValue()
    {
        Health.PlayerHealth -= HpSec;
    }
}
