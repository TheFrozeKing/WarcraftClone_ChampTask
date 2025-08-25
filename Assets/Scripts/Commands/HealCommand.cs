using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HealCommand : MonoBehaviour
{
    private HealerUnit _healer;
    public bool IsHealing;

    private void Awake()
    {
        _healer = GetComponent<HealerUnit>();
    }
    public IEnumerator Realize(Unit target)
    {
        if (IsHealing)
        {
            yield break;
        }
        IsHealing = true;


        while (target.gameObject)
        {
            if (ReachChecker.IsWithinReach(transform.position, target.transform, _healer.MaxHealDistance) && Vector3.Distance(transform.position, target.transform.position) > _healer.MinHealDistance)
            {
                _healer.Stop();
                Heal(target);
                yield return new WaitForSeconds(_healer.HealDelay);
            }
            else
            {
                Vector3 targetPos = target.transform.position;
                _healer.MoveTo(targetPos);

                yield return new WaitUntil(() => ReachChecker.IsWithinReach(transform.position, target.transform, 2) || !target.gameObject);

                if (!target.gameObject)
                {
                    break;
                }
            }
        }

        IsHealing = false;
    }

    private void Heal(IDamagable target)
    {
        target.TakeDamage(-_healer.HealAmount);
    }
}
