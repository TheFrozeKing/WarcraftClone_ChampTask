using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCommand : MonoBehaviour
{
    private WorkerUnit _worker;
    public bool IsMining;

    private void Awake()
    {
        _worker = GetComponent<WorkerUnit>();
    }

    public IEnumerator Realize(ResourceObject target)
    {
        if(IsMining)
        {
            yield break;
        }
        IsMining = true;

        while (target.gameObject)
        {
            if(ReachChecker.IsWithinReach(transform.position, target.transform, 2))
            {
                _worker.Stop();
                Mine(target);
                yield return new WaitForSeconds(_worker.MiningSpeed);
            }
            else
            {
                Vector3 targetPos = target.transform.position;
                _worker.MoveTo(targetPos);
                
                yield return new WaitUntil(() => ReachChecker.IsWithinReach(transform.position, target.transform, 2) || !target.gameObject || target.transform.position != targetPos);
                
                if (!target.gameObject)
                {
                    break;
                }
            }
        }

        IsMining = false;
        _worker.Mine();
    }

    private void Mine(ResourceObject target)
    {
        Resource resource = target.Mine(1);
        _worker.GetResource(resource);
    }

}
