using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsHandler : MonoBehaviour
{
    [SerializeField] private Dropdown _screenSizeDropdown;
    [SerializeField] private Toggle _windowedToggle;
    [SerializeField] private Toggle _audioToggle;
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private GameObject _warningPanel;
    private void Start()
    {
        PlayerSettingsXMLHandler.Instance.OnReset += OnReset;
        OnReset();
    }
    public void OnReset()
    {
        for(int i = 0; i < _screenSizeDropdown.options.Count; i++)
        {
            if (_screenSizeDropdown.options[i].text == PlayerSettingsXMLHandler.Instance.PlayerSettings.ScreenSize)
            {
                _screenSizeDropdown.value = i;
                _screenSizeDropdown.RefreshShownValue();
            }
        }

        _windowedToggle.isOn = PlayerSettingsXMLHandler.Instance.PlayerSettings.IsWindowed;
        ToggleWindowedSetting(_windowedToggle.isOn);

        _audioToggle.isOn = PlayerSettingsXMLHandler.Instance.PlayerSettings.IsAudioOn;
        ToggleAudioSetting(_audioToggle.isOn);

        _volumeSlider.value = PlayerSettingsXMLHandler.Instance.PlayerSettings.Volume;
        PlayerSettingsXMLHandler.Instance.IsSaved = true;
    }

    public void SetScreenSize(int index)
    {
        PlayerSettingsXMLHandler.Instance.PlayerSettings.ScreenSize = _screenSizeDropdown.options[index].text;
        PlayerSettingsXMLHandler.Instance.IsSaved = false;

        string widthStr = "";
        string heightStr = "";

        for(int i = 0; i < 4; i++)
        {
            widthStr += _screenSizeDropdown.options[index].text[i];
        }

        for(int i = 5; i < _screenSizeDropdown.options[_screenSizeDropdown.value].text.Length; i++)
        {
            heightStr += _screenSizeDropdown.options[index].text[i];
        }

        int width = int.Parse(widthStr);
        int height = int.Parse(heightStr);

        Screen.SetResolution(width, height, !PlayerSettingsXMLHandler.Instance.PlayerSettings.IsWindowed);
        PlayerSettingsXMLHandler.Instance.IsSaved = false;
    }

    public void ToggleWindowedSetting(bool isOn)
    {
        _windowedToggle.transform.GetChild(0).gameObject.SetActive(isOn);
        _windowedToggle.transform.GetChild(1).gameObject.SetActive(!isOn);
        PlayerSettingsXMLHandler.Instance.PlayerSettings.IsWindowed = isOn;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, !PlayerSettingsXMLHandler.Instance.PlayerSettings.IsWindowed);
        PlayerSettingsXMLHandler.Instance.IsSaved = false;
    }

    public void ToggleAudioSetting(bool isOn)
    {
        _audioToggle.transform.GetChild(0).gameObject.SetActive(isOn);
        _audioToggle.transform.GetChild(1).gameObject.SetActive(!isOn);
        PlayerSettingsXMLHandler.Instance.PlayerSettings.IsAudioOn = isOn;
        PlayerSettingsXMLHandler.Instance.IsSaved = false;
    }

    public void SetVolume(float volume)
    {
        PlayerSettingsXMLHandler.Instance.PlayerSettings.Volume = volume;
        PlayerSettingsXMLHandler.Instance.IsSaved = false;
    }

    public void TryClose()
    {
        if (PlayerSettingsXMLHandler.Instance.IsSaved)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _warningPanel.SetActive(true);
        }
    }
}

