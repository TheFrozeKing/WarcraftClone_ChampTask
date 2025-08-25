using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, хранящий в себе настройки основной игры
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] public List<PlayerData> Players = new List<PlayerData>();
    [SerializeField] public PlayerData MainPlayer = new() { Color = Color.white, Name = "Игрок" };
    public string Difficulty = "Лёгкий";
    public int MapSize = 2500;
}
