using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerUnit : Unit
{
    private HealerStats _stats => StatContainer.Stats as HealerStats;

    public float MinHealDistance => _stats.MinHealDistance;
    public float MaxHealDistance => _stats.MaxHealDistance;
    public float HealDelay => _stats.HealDelay;
    public float HealAmount => _stats.HealAmount;

    public HealCommand HealCmd => _healCommand ??= GetComponent<HealCommand>();
    private HealCommand _healCommand;

    public void Heal(Unit target)
    {
        StartCoroutine(HealCmd.Realize(target));
    }
    public override void StopAllCommands()
    {
        base.StopAllCommands();
        _healCommand.IsHealing = false;
    }
}
