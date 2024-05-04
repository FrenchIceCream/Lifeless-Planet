using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which handles enemy attacks
/// </summary>
public abstract class EnemyAttacker : MonoBehaviour
{
    [Header("Attack Settings")]
    [Tooltip("The amount of time needed to complete an attack.")]
    public float attackDuration = 0.5f;
    [Tooltip("The minimum amount of time between attacks.")]
    public float cooldownDuration = 1.0f;
    [Header("Timing by animation clip")]
    [Tooltip("The attack animation clip to use for timing")]
    bool canAttack = true;
    protected virtual bool AttackPossible() => canAttack;

    public bool Attack()
    {
        if (AttackPossible())
        {
            StartCoroutine("PerformAttack");
            return true;
        }
        return false;
    }

    protected virtual IEnumerator PerformAttack()
    {
        OnAttackStart();
        yield return null;
        OnAttackEnd();
    }

    protected IEnumerator Cooldown()
    {
        float t = 0;
        while (t < cooldownDuration)
        {
            yield return null;
            t += Time.deltaTime;
        }
        canAttack = true;
    }

    protected void OnAttackStart()
    {
        canAttack = false;
    }

    protected void OnAttackEnd()
    {
        StartCoroutine("Cooldown");
    }
}
