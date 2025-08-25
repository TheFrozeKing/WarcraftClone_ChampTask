using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BuildingStats
{
    public int Durability = 100;
    public List<ResourceType> BuildResources;
    public List<int> BuildCost;
    public string Job = "";
}
