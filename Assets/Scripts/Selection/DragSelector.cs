using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSelector : MonoBehaviour
{
    private Vector2 _startMousePos;
    private Vector2 _endMousePos;
    private RectTransform _selectionBoxImage;
    private Rect _selectionBox;

    [SerializeField] private RectTransform _selectionImagePrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = Input.mousePosition;
            _selectionBoxImage = Instantiate(_selectionImagePrefab, _startMousePos, Quaternion.identity, transform);
            _selectionBox = new();
        }
        if (Input.GetMouseButton(0))
        {
            _endMousePos = Input.mousePosition;
            float sizeX = Mathf.Abs(_endMousePos.x - _startMousePos.x);
            float sizeY = Mathf.Abs(_endMousePos.y - _startMousePos.y);

            _selectionBoxImage.localScale = new Vector2(sizeX, sizeY);
            _selectionBoxImage.position = ((_endMousePos - _startMousePos)/2 + _startMousePos);
            DrawSelection();
        }
        if(Input.GetMouseButtonUp(0))
        {
            _endMousePos = Input.mousePosition;
            Destroy(_selectionBoxImage.gameObject);
            SelectObjects();
        }
    }

    private void DrawSelection()
    {
        _selectionBox.xMin = Mathf.Min(Input.mousePosition.x, _startMousePos.x);
        _selectionBox.xMax = Mathf.Max(Input.mousePosition.x, _startMousePos.x);
        _selectionBox.yMin = Mathf.Min(Input.mousePosition.y, _startMousePos.y);
        _selectionBox.yMax = Mathf.Max(Input.mousePosition.y, _startMousePos.y);
    }

    private void SelectObjects()
    {
        foreach(var selectable in SelectionSystem.SelectableObjects)
        {
            if(_selectionBox.Contains(Camera.main.WorldToScreenPoint((selectable as MonoBehaviour).transform.position)))
            {
                SelectionSystem.DragSelect(selectable);
            }
        }
    }
}
