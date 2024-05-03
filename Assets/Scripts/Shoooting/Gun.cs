using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime;
    [SerializeField] AudioClip shootSFX;

    InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.instance;   
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (!inputManager.FirePressed())
            return;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward.normalized * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, bulletLifeTime);
    }
}
