using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RepairCommand : MonoBehaviour
{
    private WorkerUnit _worker;
    public bool IsRepairing;

    private void Awake()
    {
        _worker = GetComponent<WorkerUnit>();
    }
    public IEnumerator Realize(Building target)
    {
        if (IsRepairing)
        {
            yield break;
        }
        IsRepairing = true;


        while (target.gameObject)
        {
            if (ReachChecker.IsWithinReach(transform.position, target.transform, 2))
            {
                _worker.Stop();
                Repair(target);
                yield return new WaitForSeconds(1/_worker.RepairSpeed);
            }
            else
            {
                Vector3 targetPos = target.transform.position;
                _worker.MoveTo(targetPos);

                yield return new WaitUntil(() => ReachChecker.IsWithinReach(transform.position, target.transform, 2) || !target.gameObject);

                if (!target.gameObject)
                {
                    break;
                }
            }
        }

        IsRepairing = false;
    }

    private void Repair(IDamagable target)
    {
        target.TakeDamage(-_worker.RepairAmount);
    }
}
