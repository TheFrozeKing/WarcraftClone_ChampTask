using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : MonoBehaviour
{
    private NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = 1;
    }

    public void Realize(Vector3 movePoint)
    {
        _agent.destination = movePoint;
    }

    public void Stop()
    {
        _agent.destination = transform.position;
    }
}
