using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WorkerUnit : Unit
{
    #region Resources

    private Dictionary<ResourceType, int> _resourceAmounts = new() { { ResourceType.Wood, 0 }, { ResourceType.Stone, 0 }, { ResourceType.Metal, 0 }, { ResourceType.Wheat, 0 } };
    private Dictionary<ResourceType, int> _resourceLimits = new(){ { ResourceType.Wood, 5 }, { ResourceType.Stone, 5 }, { ResourceType.Metal, 5 }, {ResourceType.Wheat, 5 } };
       
    private ResourceType _currentMiningMode;

    #endregion

    #region Stats
    private BuilderStats _stats => StatContainer.Stats as BuilderStats;

    public float MiningSpeed => _stats.MineSpeed;
    public float RepairAmount => _stats.RepairAmount;
    public float RepairSpeed => _stats.RepairSpeed;

    #endregion

    #region Commands

    public MineCommand MineCmd => _mineCommand ??= GetComponent<MineCommand>();
    private MineCommand _mineCommand;

    public StoreCommand StoreCmd => _storeCommand ??= GetComponent<StoreCommand>();
    private StoreCommand _storeCommand;

    public RepairCommand RepairCmd => _repairCmd ??= GetComponent<RepairCommand>();
    private RepairCommand _repairCmd;

    #endregion

    #region Mining and Storing
    public void GetResource(Resource resource)
    {
        _resourceAmounts[resource.Type] += resource.Amount;
        if (_resourceAmounts[resource.Type] >= _resourceLimits[resource.Type])
        {
            StopAllCommands();
            Store();
            _resourceAmounts[resource.Type] = _resourceLimits[resource.Type];
        }
    }

    public void Mine()
    {
        StartCoroutine(MineCmd.Realize(GetClosestResource()));
        Debug.Log(_currentMiningMode);
    }
    public void Mine(ResourceObject target)
    {
        Debug.Log(_currentMiningMode);
        _currentMiningMode = target.Type;
        StartCoroutine(MineCmd.Realize(target));
    }

    public void Store()
    {
        Debug.Log("go store");
        StartCoroutine(StoreCmd.Realize(GetClosestStorage(), _resourceAmounts));
    }

    #endregion

    #region Closest   

    public IStorage GetClosestStorage()
    {
        List<IStorage> storages = GetNearbyObjectsOfType<IStorage>(5);

        float nearestDistance = float.MaxValue;
        IStorage nearestStorage = null;

        foreach (IStorage storage in storages)
        {
            if(Vector3.Distance(transform.position, (storage as MonoBehaviour).transform.position) < nearestDistance)
            {
                nearestStorage = storage;
            }
        }

        return nearestStorage;
    }

    public ResourceObject GetClosestResource()
    {
        List<ResourceObject> resources = GetNearbyObjectsOfType<ResourceObject>(5);
        List<ResourceObject> sortedResources = new();

        foreach(var resource in resources)
        {
            if(resource.Type == _currentMiningMode)
            {
                sortedResources.Add(resource);
            }
        }


        float nearestDistance = float.MaxValue;
        ResourceObject nearestResource = null;
        
        foreach(ResourceObject resource in resources)
        {
            float resourceDistance = Vector3.Distance(transform.position, resource.transform.position);
            if (resourceDistance < nearestDistance)
            {
                nearestDistance = resourceDistance;
                nearestResource = resource;
            }
        }

        return nearestResource;
    }

    #endregion
  
    public void Repair(Building target)
    {
        StartCoroutine(RepairCmd.Realize(target));
    }

    public override void StopAllCommands()
    {
        base.StopAllCommands();
        MineCmd.IsMining = false;
        StoreCmd.IsStoring = false;
        RepairCmd.IsRepairing = false;
    }
}


