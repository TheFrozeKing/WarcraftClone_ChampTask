using System;
using System.Collections.Generic;

[Serializable]
public class UnitStats
{
    public string Name;
    public float Speed;
    public float Health;
    public List<ResourceType> ResourceTypesNeeded;
    public List<int> ResourceAmountsNeeded;
    public int DetectionRadius;
}
