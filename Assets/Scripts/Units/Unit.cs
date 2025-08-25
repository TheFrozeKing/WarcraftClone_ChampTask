using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(MoveCommand))]
public class Unit : MonoBehaviour, IMovable, IOwnable, IDamagable, IRaycastable, ISelectable
{
    [SerializeField] protected UnitStatContainer StatContainer;
    public float CurrentHealth => _health;
    private float _health;

    public Player Owner => _owner;
    [SerializeField] private Player _owner;
    [SerializeField] protected LayerMask GroundLayer;

    public bool IsSelected { get; set; }
    private GameObject _selectionOutline;

    public Vector3 MovePoint => _agent.destination;
    private NavMeshAgent _agent;

    #region Commands

    public MoveCommand MoveCmd => _moveCommand ??= GetComponent<MoveCommand>();
    private MoveCommand _moveCommand;

    #endregion
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        SelectionSystem.SelectableObjects.Add(this);
        _selectionOutline = transform.GetChild(transform.childCount - 1).gameObject;        
    }

    public void Initialize(Player owner)
    {
        _owner = owner;
        owner.OnUnitsCalled += AddMyselfToOwnerList;
        _agent.speed = StatContainer.Stats.Speed;
        _health = StatContainer.Stats.Health;
    }

    private void AddMyselfToOwnerList()
    {
        Owner.Units.Add(this);
    }

    public void MoveTo(Vector3 movePoint)
    {
        MoveCmd.Realize(movePoint);
    }

    public void Stop()
    {
        MoveCmd.Stop();
    }

    public virtual void StopAllCommands()
    {
        StopAllCoroutines();
        MoveCmd.Stop();
    }

    public void OnSelectionStatusChanged()
    {
        _selectionOutline.SetActive(IsSelected);
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage,0,StatContainer.Stats.Health);
        if(_health < 0)
        {
            _owner.OnUnitsCalled -= AddMyselfToOwnerList;
            Destroy(this);
        }
    }

    public List<T> GetNearbyObjectsOfType<T>(float sphereCastDistance)
    {
        List<T> objectHits = new();

        Collider[] objects = Physics.OverlapSphere(transform.position, sphereCastDistance, ~GroundLayer);

        foreach (Collider collider in objects)
        {
            if (collider.TryGetComponent(out T tObject))
            {
                objectHits.Add(tObject);
            }
        }
        if (objectHits.Count < 1 && sphereCastDistance < 100)
        {
            objectHits = GetNearbyObjectsOfType<T>(sphereCastDistance * 2);
        }
        return objectHits;
    }

}
