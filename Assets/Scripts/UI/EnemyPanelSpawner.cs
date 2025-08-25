using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyPanelSpawner : MonoBehaviour
{
    [SerializeField] private PlayerPanel _enemyPanelPrefab;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private PlayerPanel _playerPanel;
    private List<PlayerData> _enemyDatas = new();
    private List<PlayerPanel> _playerPanels = new();
    private void Start()
    {
        SpawnCycle(0);
        _playerPanels.Add(_playerPanel);
        _playerPanel.transform.GetComponentInChildren<PlayerPanelNameInput>().NameChanged += CheckNameAvailabilities;
    }
    private void SpawnEnemyPanels(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            PlayerPanel newPanel = Instantiate(_enemyPanelPrefab, transform);
            PlayerData newData = new() { Name = "Противник " + (newPanel.transform.GetSiblingIndex()+1)};
            newPanel.transform.name = newData.Name;
            newPanel.Initialize(newData);

            newPanel.transform.GetComponentInChildren<PlayerPanelNameInput>().NameChanged += CheckNameAvailabilities;

            _enemyDatas.Add(newData);
            _playerPanels.Add(newPanel);
        }
        _gameSettings.Players.AddRange(_enemyDatas);
    }

    public void CheckNameAvailabilities()
    {
        foreach (PlayerPanel enemy in _playerPanels)
        {
            bool isAvailable = true;
            foreach (PlayerPanel secondEnemy in _playerPanels)
            {
                if(enemy.transform.name == secondEnemy.transform.name && !enemy.CompareTag(secondEnemy.tag))
                {
                    isAvailable = false;
                }
            }
            enemy.SetAvailabilityVisual(isAvailable);
        }

        for (int i = 0; i < _gameSettings.Players.Count; i++)
        {
            PlayerData enemy = _gameSettings.Players[i];
            for (int j = 0; j < _gameSettings.Players.Count; j++)
            {
                if (enemy.Name == _gameSettings.Players[j].Name && enemy != _gameSettings.Players[j])
                {
                    _playerPanels[i].SetAvailabilityVisual(false);
                    break;
                }
                else
                {
                    _playerPanels[i].SetAvailabilityVisual(true);
                }
            }
        }
    }

    private void DestroyAllPanels()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        try
        {
            _gameSettings.Players.RemoveRange(1, _gameSettings.Players.Count - 1);
            _enemyDatas.Clear();
            _playerPanels.RemoveRange(1,_playerPanels.Count-1);
        }
        catch
        {

        }
    }

    public async void SpawnCycle(int amount)
    {
        DestroyAllPanels();
        await Task.Delay(50);
        SpawnEnemyPanels(amount + 1);
    }

}
