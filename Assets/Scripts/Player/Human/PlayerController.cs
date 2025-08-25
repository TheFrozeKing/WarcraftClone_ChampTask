using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Player Owner { get; set; }
    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Owner.OpenUnitConnection();

                foreach(var unit in Owner.Units)
                {
                    if (unit.IsSelected)
                    {
                        if (unit is WorkerUnit worker)
                        {
                            if(hit.collider.TryGetComponent(out ResourceObject resource))
                            {
                                worker.Mine(resource);
                                Debug.Log("SEnding to mine");
                            }
                            else if(hit.collider.TryGetComponent(out Building building) && building.Owner == this.Owner)
                            {
                                worker.Repair(building);
                                Debug.Log("Sending to repair");
                            }
                        }
                        else if (unit is HealerUnit healer && hit.collider.TryGetComponent(out Unit healTarget) && healTarget.Owner == this.Owner)
                        {
                            healer.Heal(healTarget);
                            Debug.Log("sending to heal");

                        }
                        else if (unit is AttackingUnit attacker && hit.collider.TryGetComponent(out IDamagable damagable) && (damagable as IOwnable).Owner != this.Owner)
                        {
                            attacker.Attack(damagable);
                            Debug.Log("sending to attack");
                        }
                        else if(unit is IMovable movable)
                        {
                            movable.MoveTo(hit.point);
                        }
                    }
                }

                Owner.CloseUnitConnection();
            }

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Owner.OpenUnitConnection();

            foreach (var unit in Owner.Units)
            {
                unit.StopAllCommands();
            }

            Owner.CloseUnitConnection();
        }
    }
}
