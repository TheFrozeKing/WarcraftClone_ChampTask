using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelColorButton : MonoBehaviour
{
    private Button _button;
    private Image _image;
    private PlayerData _playerData;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void Initialize(PlayerData pData)
    {
        _playerData = pData;
        _image = GetComponent<Image>();
        OnClick();
    }
        
    public void OnClick()
    {
        Color color = new(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f),1);
        Debug.Log(transform.parent.name);
        _image.color = color;
        _playerData.Color = color;
    }
}
