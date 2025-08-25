using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GetInsideArcherTowerCommand : MonoBehaviour
{
    public void Realize(ArcherTowerUnit tower)
    {
        tower.AddArcher(gameObject);
    }
}
