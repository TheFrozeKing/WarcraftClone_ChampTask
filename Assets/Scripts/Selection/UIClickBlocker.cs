using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SelectionSystem.IsMouseOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SelectionSystem.IsMouseOverUI = false;
    }
}
