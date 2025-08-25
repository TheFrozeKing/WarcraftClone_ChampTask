using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class HealerStats : UnitStats
{
    public float MinHealDistance;
    public float MaxHealDistance;
    public float HealDelay;
    public float HealAmount;
}
