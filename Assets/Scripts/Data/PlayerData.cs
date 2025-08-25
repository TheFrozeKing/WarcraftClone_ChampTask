using System;
using UnityEngine;

/// <summary>
/// Класс, хранящий данные отдельного игрока
/// </summary>
[Serializable]
public class PlayerData
{
    public Color Color;
    public string Name;
    public Action NameChangedExternally;
}
