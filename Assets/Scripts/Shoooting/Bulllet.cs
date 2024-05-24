using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour
{   
    [SerializeField] string[] tagsToHit = {"Enemy", "Boss"};
    [SerializeField] int bulletDamage = 10;
    void OnCollisionEnter(Collision collision)
    {
        foreach (string tag in tagsToHit)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
