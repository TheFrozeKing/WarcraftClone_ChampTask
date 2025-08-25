using System.Collections.Generic;
using UnityEngine;

public class TownHall : Building, IStorage
{
    private BuildZoneBuildingStats _stats;
    [SerializeField] private TextMesh _nameDisplay;
    [SerializeField] private MeshRenderer _buildZoneRenderer;
    //[SerializeField] private Slider _healthSlider;
    private void Start()
    {
        _stats = StatContainer.Stats as BuildZoneBuildingStats;
    }
    new public void Initialize(Player owner)
    {
        base.Initialize(owner);
        _nameDisplay.text = owner.Data.Name;
        _buildZoneRenderer.material.color = owner.Data.Color;

        UnitSpawner spawner = GetComponent<UnitSpawner>();
        spawner.Initialize(owner);

        spawner.SpawnUnit(UnitTypes.Worker, 3);
        spawner.SpawnUnit(UnitTypes.Archer, 2);
    }    

    public void Store(Dictionary<ResourceType,int> incomingResources)
    {
        Dictionary<ResourceType, int> resourceAmounts = Owner.ResourceContainer.ResourceAmounts;
        Dictionary<ResourceType, int> copyContainer = new(resourceAmounts);

        foreach(KeyValuePair<ResourceType,int> resource in copyContainer)
        {
            try
            {
                resourceAmounts[resource.Key] += incomingResources[resource.Key];
                resourceAmounts[resource.Key] = Mathf.Clamp(resourceAmounts[resource.Key], 0, Owner.ResourceContainer.ResourceCaps[resource.Key]);
                incomingResources[resource.Key] = 0;
            }
            catch
            {

            }
        }
    }
}
