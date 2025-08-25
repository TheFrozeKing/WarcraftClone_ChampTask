using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreCommand : MonoBehaviour
{
    public bool IsStoring = false;
    private WorkerUnit _worker;

    private void Start()
    {
        _worker = GetComponent<WorkerUnit>();
    }

    public IEnumerator Realize(IStorage target, Dictionary<ResourceType, int> resourceAmounts)
    {
        Debug.Log("start");
        if (IsStoring)
        {
        Debug.Log("is already storing");
            yield break;
        }
        IsStoring = true;
        Debug.Log("Starting storing");

        GameObject storage = (target as MonoBehaviour).gameObject;
        Debug.Log($"{storage.name} storage found");

        _worker.MoveTo(storage.transform.position);
        Debug.Log($"worker now moving to {storage.transform.position}, worker movepos is {_worker.MovePoint}");

        yield return new WaitUntil(() => ReachChecker.IsWithinReach(transform.position, storage.transform, 2) || !storage);

        if (!storage)
        {
            IsStoring = false;
            _worker.Stop();
            _worker.Store();
            yield break;
        }
        yield return null;

        _worker.Stop();
        target.Store(resourceAmounts);
        IsStoring = false;
        _worker.Mine();
    }
}
