using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour
{
    [SerializeField] int bulletDamage = 10;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
