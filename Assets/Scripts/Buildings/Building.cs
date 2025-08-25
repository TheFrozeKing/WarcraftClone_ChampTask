using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable, IOwnable, IRaycastable, ISelectable
{
    [SerializeField] protected BuildingStatContainer StatContainer;
    [SerializeField] protected LayerMask GroundLayer;
        
    public Player Owner => _owner;
    [SerializeField] private Player _owner;

    public float CurrentHealth => _health;
    private float _health;

    public bool IsSelected { get; set; }

    #region Construction

    private Collider _collider;
    private NavMeshModifierVolume _navMeshModifierVolume;
    private bool _isHittingAnotherObject = false;
    private bool _canBuildHere = true;

    #endregion
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _navMeshModifierVolume = GetComponent<NavMeshModifierVolume>();        
        SelectionSystem.SelectableObjects.Add(this);        
    }
    public void Initialize(Player owner)
    {
        _owner = owner;
        Owner.OnBuildingsCalled += AddMyselfToOwnerList;
    }

    public void AddMyselfToOwnerList()
    {
        Owner.Buildings.Add(this);
    }

    public void ShadowSpawn()
    {
        StartCoroutine(ShadowFollow());
    }

    private IEnumerator ShadowFollow()
    {
        _navMeshModifierVolume.enabled = false;
        _collider.isTrigger = true;

        Rigidbody tempRb = transform.AddComponent<Rigidbody>();
        tempRb.constraints = RigidbodyConstraints.FreezeAll;

        Camera mainCam = Camera.main;

        while (!Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundLayer))
            {
                transform.position = hit.point;
            }
            yield return null;
        }

        Destroy(tempRb);
        tempRb = null;

        if(_canBuildHere)
        {
            StartCoroutine(FinishBuilding());
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public IEnumerator FinishBuilding()
    {
        _collider.isTrigger = false;
        _navMeshModifierVolume.enabled = true;

        yield return new WaitUntil(() => _health == StatContainer.Stats.Durability);
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, StatContainer.Stats.Durability);
        if(_health <= 0)
        {
            Destroy(this);
        }
    }
    public void OnSelectionStatusChanged()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        _isHittingAnotherObject = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isHittingAnotherObject = false;
    }
}
