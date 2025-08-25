using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHintPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _hint;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hint.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hint.SetActive(false);
    }
}
