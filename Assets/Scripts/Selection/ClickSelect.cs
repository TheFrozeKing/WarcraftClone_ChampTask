using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSelect : MonoBehaviour
{
    [SerializeField] private LayerMask _clickableMask;
    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _clickableMask))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    SelectionSystem.ShiftClickSelect(hit.collider.GetComponent<ISelectable>());
                }
                else
                {
                    SelectionSystem.ClickSelect(hit.collider.GetComponent<ISelectable>());
                }
            }
            else
            {
                if(!Input.GetKey(KeyCode.LeftShift) && !SelectionSystem.IsMouseOverUI)
                {
                    SelectionSystem.DeselectAll();
                }
            }
        }
    }
}
