using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour
{   
    [SerializeField] string tagToHit = "Enemy";
    [SerializeField] int bulletDamage = 10;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagToHit))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
