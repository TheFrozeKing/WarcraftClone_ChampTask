using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WatchTowerStats : BuildZoneBuildingStats
{
    public float DetectionRadius = 15;
    public int Capacity = 5;
    public float MinRange = 0;
    public float MaxRange = 8;
    public int AttackDelay = 2;
    public float DamagePerArcher = 5;
}
