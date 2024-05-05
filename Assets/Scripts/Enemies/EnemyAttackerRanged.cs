using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackerRanged : EnemyAttacker
{
    EnemyShooter enemyShooter;

    void Awake()
    {
        enemyShooter = GetComponent<EnemyShooter>();
    }

    protected override bool AttackPossible()
    {
        return base.AttackPossible();
    }

    protected override IEnumerator PerformAttack()
    {
        OnAttackStart();
        enemyShooter.Shoot();
        float t = 0;
        while (t < attackDuration)
        {
            yield return null;
            t += Time.deltaTime;
        }
        OnAttackEnd();
    }
}
