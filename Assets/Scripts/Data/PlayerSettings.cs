using System;

/// <summary>
/// Класс, хранящий настройки игрока
/// </summary>
public class PlayerSettings : ICloneable
{
    public string ScreenSize = "1920:1080";
    public bool IsWindowed = true;
    public bool IsAudioOn = true;
    public float Volume = 0.5f;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
