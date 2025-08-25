using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : MonoBehaviour
{

    private AttackingUnit _attacker;
    public bool IsAttacking;
        
    private void Awake()
    {
        _attacker = GetComponent<AttackingUnit>();
    }

    public IEnumerator Realize(IDamagable target)
    {
        if (IsAttacking)
        {
            yield break;
        }
        IsAttacking = true;

        Transform targetTransform = (target as MonoBehaviour).transform;

        while (targetTransform.gameObject)
        {
            if (ReachChecker.IsWithinReach(transform.position, targetTransform, _attacker.MaxAttackRadius) && Vector3.Distance(_attacker.transform.position, targetTransform.position) > _attacker.MinAttackRadius)
            {
                _attacker.Stop();
                Attack(target);
                yield return new WaitForSeconds(_attacker.AttackDelay);
            }
            else
            {
                Vector3 targetPos = targetTransform.position;
                _attacker.MoveTo(targetPos);

                yield return new WaitUntil(() => ReachChecker.IsWithinReach(transform.position, targetTransform, 2) || !targetTransform.gameObject || targetTransform.position != targetPos);

                if (!targetTransform.gameObject)
                {
                    break;
                }
            }
        }

        IsAttacking = false;
        _attacker.Attack();
    }

    private void Attack(IDamagable target)
    {
        target.TakeDamage(_attacker.Damage);
    }
}
