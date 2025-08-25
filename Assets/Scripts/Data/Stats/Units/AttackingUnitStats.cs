using System;

[Serializable]
public class AttackingUnitStats : UnitStats
{
    public float MinAttackRadius;
    public float MaxAttackRadius;
    public float AttackDelay;
    public float Damage;
}
