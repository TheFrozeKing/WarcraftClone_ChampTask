using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerPanelNameInput : MonoBehaviour, IPointerExitHandler, IPointerClickHandler
{
    private PlayerData _playerData;
    private InputField _inputField;
    private bool _isSelected;
    public event Action NameChanged;

    public void Initialize(PlayerData pData)
    {
        _playerData = pData;
        _inputField = GetComponent<InputField>();
        _inputField.text = _playerData.Name;
        _playerData.NameChangedExternally += () =>
        {
            _inputField.text = _playerData.Name;
            transform.parent.name = _playerData.Name;
        };
    }

    public void ChangeAvailabilityVisual(bool isAvailable)
    {
        if(isAvailable)
        {
            _inputField.textComponent.color = Color.white;
        }
        else
        {
            _inputField.textComponent.color = Color.red;
        }
    }

    public void ChangeName(string input)
    {
        _playerData.Name = input;
        NameChanged?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isSelected = false;
        _inputField.interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelected = !_isSelected;
        _inputField.interactable = _isSelected;
    }
}
