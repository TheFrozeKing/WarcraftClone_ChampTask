using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GetInsideArcherTowerCommand))]
public class ArcherUnit : AttackingUnit
{
    private GetInsideArcherTowerCommand GetInsideArcherTowerCmd => _getInsideArcherTowerCommand ??= GetComponent<GetInsideArcherTowerCommand>();
    private GetInsideArcherTowerCommand _getInsideArcherTowerCommand;

    public void GetInsideArcherTower(ArcherTowerUnit tower)
    {
        GetInsideArcherTowerCmd.Realize(tower);
    }
}
