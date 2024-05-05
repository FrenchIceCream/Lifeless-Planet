using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] float force;
    [SerializeField] float bulletLifeTime;
    Transform target;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 dir = target.position - bulletSpawn.position;
        rb.AddForce(dir * force);
        Destroy(bullet, bulletLifeTime);
    }
}
