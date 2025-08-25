using System.Collections.Generic;
using UnityEngine;

public class AttackingUnit : Unit
{
    #region Stats
    private AttackingUnitStats _stats => StatContainer.Stats as AttackingUnitStats;

    public float MinAttackRadius => _stats.MinAttackRadius;
    public float MaxAttackRadius => _stats.MaxAttackRadius;
    public float AttackDelay => _stats.AttackDelay;
    public float Damage => _stats.Damage;

    #endregion

    public AttackCommand AttackCmd => _attackCommand ??= GetComponent<AttackCommand>();
    private AttackCommand _attackCommand;

    public virtual void Attack()
    {
        StartCoroutine(AttackCmd.Realize(GetClosestEnemy()));
    }


    public virtual void Attack(IDamagable target)
    {
        StartCoroutine(AttackCmd.Realize(target));
    }

    public override void StopAllCommands()
    {
        base.StopAllCommands();
        AttackCmd.IsAttacking = false;
    }
    public IDamagable GetClosestEnemy()
    {
        List<IDamagable> damagables = GetNearbyObjectsOfType<IDamagable>(5);
        List<IDamagable> enemies = new();

        foreach(var d in damagables)
        {
            if ((d as IOwnable).Owner != this.Owner)
            {
                enemies.Add(d);
            }
        }

        float closestDistance = float.MaxValue;
        IDamagable closestEnemy = null;

        foreach(var d in enemies)
        {
            float distance = Vector3.Distance((d as MonoBehaviour).transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = d;
            }
        }

        return closestEnemy;
    }

}
