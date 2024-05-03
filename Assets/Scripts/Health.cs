using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int currentHealth;

    public int GetHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;


    public void Heal(int amount)
    {
        if (currentHealth + amount > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += amount;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth - amount < 0)
        {
            currentHealth = 0;
            //todo player death
            Destroy(gameObject);
        }
        else
            currentHealth -= amount;
    }
}
