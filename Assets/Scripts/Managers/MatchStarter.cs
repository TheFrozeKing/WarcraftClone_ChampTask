using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class MatchStarter : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private PlayerController _mainPlayerController;
    [SerializeField] private Player _playerPrefab;
    public List<Player> Players;
    public void StartMatch(List<TownHall> availableBases)
    {
        for (int i = 0; i < _gameSettings.Players.Count; i++)
        {
            Player newPlayer = Instantiate(_playerPrefab, null);
            newPlayer.name = "Player " + (i+1); 
            newPlayer.Data = _gameSettings.Players[i];
            Players.Add(newPlayer);
        }

        for (int i = 0;i < _gameSettings.Players.Count; i++)
        {
            TownHall availableBase = availableBases[i];
            availableBase.Initialize(Players[i]);
            availableBase.TakeDamage(-5000);
        }

        foreach(Player player in Players) 
        {
            if (player.Data == _gameSettings.Players[0])
            {
                Debug.Log("dib dab");
                _mainPlayerController.Owner = player;
            }
        }
    }
}
