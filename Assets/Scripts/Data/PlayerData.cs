using System;
using UnityEngine;

/// <summary>
/// �����, �������� ������ ���������� ������
/// </summary>
[Serializable]
public class PlayerData
{
    public Color Color;
    public string Name;
    public Action NameChangedExternally;
}
