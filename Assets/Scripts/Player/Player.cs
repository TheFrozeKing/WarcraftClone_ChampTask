using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerResourceContainer ResourceContainer { get; private set; }
    public PlayerData Data { get; set; }

    public List<Unit> Units = new();
    public List<Building> Buildings = new();

    public event Action OnUnitsCalled;
    public event Action OnBuildingsCalled;

    public void OpenUnitConnection()
    {
        OnUnitsCalled?.Invoke();
    }

    public void CloseUnitConnection()
    {
        Units.Clear();
    }

    public void OpenBuildingConnection()
    {
        OnBuildingsCalled?.Invoke();
    }

    public void CloseBuildingConnection()
    {
        Buildings.Clear();
    }
}
