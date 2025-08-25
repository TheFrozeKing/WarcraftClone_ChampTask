using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, �������� � ���� ��������� �������� ����
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] public List<PlayerData> Players = new List<PlayerData>();
    [SerializeField] public PlayerData MainPlayer = new() { Color = Color.white, Name = "�����" };
    public string Difficulty = "˸����";
    public int MapSize = 2500;
}
