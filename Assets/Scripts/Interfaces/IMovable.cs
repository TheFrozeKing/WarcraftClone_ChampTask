using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable 
{
    public Vector3 MovePoint { get;}
    public void MoveTo(Vector3 movePoint);

    public void Stop();
}
