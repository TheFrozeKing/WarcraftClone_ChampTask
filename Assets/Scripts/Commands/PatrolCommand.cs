using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PatrolCommand : MonoBehaviour
{
    private Vector3 _oldPos;
    private Vector3 _nextPos;
    private AttackingUnit _attackingUnit;

    public bool IsPatroling;

    private void Awake()
    {
        _attackingUnit = GetComponent<AttackingUnit>();
    }

    public IEnumerator Realize(Vector3 patrolTo)
    {
        if (IsPatroling)
        {
            yield break;
        }
        IsPatroling = true;

        bool foundEnemy = false;
        _nextPos = patrolTo;

        while (!foundEnemy)
        {
            _attackingUnit.MoveTo(_nextPos);
            if(Vector3.Distance(transform.position, _nextPos) < 0.5f)
            {
                Vector3 shuffler = _nextPos;
                _nextPos = _oldPos;
                _oldPos = shuffler;
                _attackingUnit.MoveTo(_nextPos);
            }

            IDamagable nearestEnemy = GetNearestEnemy();

            if (nearestEnemy != null)
            {
                _attackingUnit.Attack(GetNearestEnemy());
                foundEnemy = true; 
            }
        }
    }

    public IDamagable GetNearestEnemy()
    {
        List<IDamagable> enemies = GetEnemiesNearSelf(5);

        float nearestDistance = float.MaxValue;
        IDamagable closestEnemy = null;
        
        foreach(IOwnable enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, (enemy as MonoBehaviour).transform.position);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                closestEnemy = enemy as IDamagable;
            }
        }

        return closestEnemy;
    }

    public List<IDamagable> GetEnemiesNearSelf(float sphereCastDistance)
    {
        List<IOwnable> objectHits = new();

        Collider[] objects = Physics.OverlapSphere(transform.position, sphereCastDistance);

        foreach (Collider collider in objects)
        {
            if (collider.TryGetComponent(out IOwnable tObject))
            {
                objectHits.Add(tObject);
            }
        }

        List<IDamagable> enemies = new();

        foreach(var ownable in objectHits)
        {
            if(ownable.Owner != _attackingUnit.Owner)
            {
                enemies.Add(ownable as IDamagable);
            }
        }
        return enemies;
    }
}
