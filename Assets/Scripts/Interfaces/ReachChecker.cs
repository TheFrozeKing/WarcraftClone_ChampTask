using UnityEngine;

public static class ReachChecker
{
    public static bool IsWithinReach(Vector3 selfPos, Transform target, float maxDist)
    {
        if(Physics.Raycast(selfPos, (target.position - selfPos), out RaycastHit hit, maxDist))
        {
            if (hit.collider.transform == target || Vector3.Distance(selfPos, target.position) <= 2)
            {
                return true;
            }
        }
        return false;
    }
}