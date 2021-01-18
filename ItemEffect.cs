using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemEffect
{
    public string EffectName { get; set; }
    public string EffectDescription { get; set; }
    public float EffectDamage { get; set; }
    public string EffectTargets { get; set; }

    public ItemEffect()
    {
        //Default
    }

    public ItemEffect(string en, string ed, float edam, string et)
    {
        this.EffectName = en;
        this.EffectDescription = ed;
        this.EffectDamage = edam;
        this.EffectTargets = et;
    }
}
