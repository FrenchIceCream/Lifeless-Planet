using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackerMelee : EnemyAttacker
{
    [Tooltip("The list of colliders to turn on/off when making melee attacks")]
    public List<Collider> weaponColliders = new List<Collider>();

    protected override IEnumerator PerformAttack()
    {
        OnAttackStart();
        SetWeaponColliders(true);
        float t = 0;
        while (t < attackDuration)
        {
            yield return null;
            t += Time.deltaTime;
        }
        SetWeaponColliders(false);
        OnAttackEnd();
    }

    protected void SetWeaponColliders(bool activation)
    {
        foreach (Collider c in weaponColliders)
        {
            if (c != null)
            {
                c.enabled = activation;
            }
        }
    }
}
