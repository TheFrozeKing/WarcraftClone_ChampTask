using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
///  ласс, сохран€ющий/загружающий настройки игрока в/из формат/а XML
/// </summary>
public class PlayerSettingsXMLHandler : MonoBehaviour
{
    public event Action OnReset;
    public bool IsSaved;
    public static PlayerSettingsXMLHandler Instance;
    public PlayerSettings PlayerSettings { get; private set; }
    private PlayerSettings _oldPlayerSettings;
    private const string _settingsPath = "PlayerSettings.xml";

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        PlayerSettings = new();

        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
            Directory.CreateDirectory("Resources/Xml");
            Directory.CreateDirectory("Resources/Json");
        }
        else if (!Directory.Exists("Resources/Xml"))
        {
            Directory.CreateDirectory("Resources/Xml");
        }

        try
        {
            LoadSettings();
        }
        catch
        {
            SaveSettings();
        }

        _oldPlayerSettings = (PlayerSettings)PlayerSettings.Clone();
        IsSaved = true;
    }

    public void LoadSettings()
    {
        PlayerSettings = DataParser.ParseXMLData<PlayerSettings>(_settingsPath);
    }

    public void SaveSettings()
    {
        _oldPlayerSettings = (PlayerSettings)PlayerSettings.Clone();
        DataParser.WriteXMLData(_settingsPath, PlayerSettings);
        IsSaved = true;
    }

    public void ResetSettings()
    {
        PlayerSettings = (PlayerSettings)_oldPlayerSettings.Clone();
        OnReset.Invoke();
        IsSaved = true;
    }

    
}
