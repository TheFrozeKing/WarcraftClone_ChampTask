using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    private PlayerData _playerData;
    [SerializeField] private PlayerPanelColorButton _colorButton;
    [SerializeField] PlayerPanelNameInput _nameInput;

    public void Initialize(PlayerData pData)
    {
        _playerData = pData;
        _colorButton.Initialize(_playerData);
        _nameInput.Initialize(_playerData);
    }

    public void SetAvailabilityVisual(bool isAvailable) 
    {
        _nameInput.ChangeAvailabilityVisual(isAvailable);
    }

}
