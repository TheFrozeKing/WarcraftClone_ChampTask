using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _scaleRange;

    private void Awake()
    {
        if (!_camera)
        {
            _camera = GetComponent<Camera>();
        }
    }
    private void Update()
    {
        float mouseWheel = -(int)Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * 10,-1,1);
        Scale(mouseWheel);

        float mouseX = -Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        float keyboardX = Input.GetAxisRaw("Horizontal");
        float keyboardY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(2))
        {
            Move(new(mouseX, mouseY));
        }
        Move(new(keyboardX, keyboardY));
    }
    private void Move(Vector2 moveDirection)
    {
        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y);
    }
    
    private void Scale(float mouseWheel)
    {
        float newFOV = _camera.fieldOfView + mouseWheel;
        newFOV = Mathf.Clamp(newFOV, _scaleRange.x, _scaleRange.y);
        _camera.fieldOfView = newFOV;
    }


}
