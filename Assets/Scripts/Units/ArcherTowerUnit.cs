using UnityEngine;

public class ArcherTowerUnit : AttackingUnit
{
    private ArcherTowerStats _stats => StatContainer.Stats as ArcherTowerStats;

    private int _currentArcherAmount = 0;
    public int Capacity => _stats.Capacity;
    public float DamagePerArcher => _stats.DamagePerArcher;
        
    public void AddArcher(GameObject archer)
    {
        if(_currentArcherAmount < _stats.Capacity)
        {
            archer.SetActive(false);
            archer.transform.parent = transform;
            archer.transform.position = transform.position;
            _currentArcherAmount++;
        }
    }

    public override void Attack()
    {
        _stats.Damage = _currentArcherAmount * _stats.DamagePerArcher;
        base.Attack();
    }

    public override void Attack(IDamagable target)
    {
        _stats.Damage = _currentArcherAmount * _stats.DamagePerArcher;
        base.Attack(target);
    }
}
