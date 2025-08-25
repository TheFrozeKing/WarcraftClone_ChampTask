using UnityEngine;
using UnityEngine.UI;

public class GameSettingsPanelHandler : MonoBehaviour
{
    [SerializeField] private InputField _mapSizeInput;
    [SerializeField] private Dropdown _difficultyDropdown;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private PlayerPanel _playerPanel;
    public void SetDifficulty(int index)
    {
        _gameSettings.Difficulty = _difficultyDropdown.options[index].text;
    }

    private void Awake()
    {
        _gameSettings.MapSize = 2500;
        _gameSettings.Difficulty = "Лёгкая";
        _gameSettings.Players.Clear();

        PlayerData playerData = new() {Name = "Игрок 1"};

        _gameSettings.MainPlayer = playerData;
        _playerPanel.Initialize(playerData);
        _gameSettings.Players.Add(playerData);
    }
    public void CheckMapInput(string sizeStr)
    {
        float finalSize;
        int size = int.Parse(sizeStr);

        if(size >= 2500 && size <= 10000)
        {
            int roundedSizeSqrt = Mathf.RoundToInt(Mathf.Sqrt(size));

            finalSize = Mathf.Pow(roundedSizeSqrt, 2);

            _mapSizeInput.text = finalSize.ToString();
        }
        else
        {
            finalSize = Mathf.Clamp(size, 2500, 10000);
            _mapSizeInput.text = finalSize.ToString(); 
        }
        _gameSettings.MapSize = (int)finalSize;
    }
}
