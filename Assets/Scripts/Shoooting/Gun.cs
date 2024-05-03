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

    [Header("SFX")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField, Range(0, 1)] float shootSFXVolume = 1f;

    enum GunType {   Single, Burst, Auto    }
    GunType currentGunType;
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

        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward.normalized * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, bulletLifeTime);
    }
}
