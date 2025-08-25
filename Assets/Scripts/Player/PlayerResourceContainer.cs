using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceContainer : MonoBehaviour
{
    /*[Header("Caps")]
    public int MaxWoodAmount = 10;
    public int MaxStoneAmount = 10;
    public int MaxMetalAmount = 10;
    public int MaxFoodAmount = 10;*/

    public Dictionary<ResourceType, int> ResourceCaps = new() 
    {
        {ResourceType.Wood, 10},
        {ResourceType.Stone, 10},
        {ResourceType.Metal, 10},
        {ResourceType.Food, 10}
    };
    
    /*[Header("Amounts")]
    public int WoodAmount = 5;
    public int StoneAmount = 5;
    public int MetalAmount = 5;
    public int FoodAmount = 5;*/

    public Dictionary<ResourceType, int> ResourceAmounts = new()
    {
        {ResourceType.Wood, 0},
        {ResourceType.Stone, 0},
        {ResourceType.Metal, 0},
        {ResourceType.Food, 0}
    };
}
