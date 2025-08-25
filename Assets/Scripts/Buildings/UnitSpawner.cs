using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour, IOwnable
{
    [SerializeField] private Transform _spawnPos;

    [SerializeField] private Dictionary<UnitTypes,Unit> _unitPrefabDict; 
    [SerializeField] private List<Unit> _unitPrefabList;

    public Player Owner => _owner;
    private Player _owner;


    private void Awake()
    {
        _unitPrefabDict = new()
        {
            {UnitTypes.Worker, _unitPrefabList[0] },
            {UnitTypes.Archer, _unitPrefabList[1] },
            {UnitTypes.ArcherTower, _unitPrefabList[2] },
            {UnitTypes.Catapult, _unitPrefabList[3] },
            {UnitTypes.Heavy, _unitPrefabList[4] },
            {UnitTypes.Spearsman, _unitPrefabList[5] },
            {UnitTypes.Healer, _unitPrefabList[6] }
        };
    }
    public void Initialize(Player owner)
    {
        _owner = owner;
    }

    public void SpawnUnit(UnitTypes unit, int amount)
    {
        for(int i = 0; i < amount;  i++)
        {
            Unit newUnit = Instantiate(_unitPrefabDict[unit], _spawnPos.position, Quaternion.identity);
            newUnit.Initialize(_owner);
        }
    }
}

public enum UnitTypes
{
    Worker, Archer, ArcherTower, Catapult, Heavy, Spearsman, Healer
}