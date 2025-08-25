using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public ResourceType Type => _resourceType;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _currentAmount;

    public Resource Mine(int mineAmount)
    {
        _currentAmount -= mineAmount;

        if(_currentAmount <= 0)
        {
            Destroy(this);
        }

        return new Resource(_resourceType, mineAmount);
    }
}

public struct Resource
{
    public Resource(ResourceType type, int amount)
    {
        Type = type;
        Amount = amount;
    }

    public ResourceType Type;
    public int Amount;
}