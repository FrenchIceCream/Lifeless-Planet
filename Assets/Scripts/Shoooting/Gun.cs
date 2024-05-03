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
    [SerializeField] float bulletDelay;
    [SerializeField] int bulletsPerBurst = 3;
    [SerializeField] float spreadIntensity;
    [Header("SFX")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField, Range(0, 1)] float shootSFXVolume = 1f;


    enum GunType {   Single, Burst, Auto    }
    
    //[SerializeField] 
    GunType currentGunType;
    InputManager inputManager;
    bool readyToShoot = true;
    bool isShooting;
    bool allowReset = true;
    int burstBulletsLeft;

    void Start()
    {
        inputManager = InputManager.instance;
        burstBulletsLeft = bulletsPerBurst;   
    }

    void Update()
    {
        switch (currentGunType)
        {
            case GunType.Single:
            case GunType.Burst:
                isShooting = inputManager.FirePressed();
                break;
            case GunType.Auto:
                isShooting = inputManager.FireHeld();
                break;
        }

        if (isShooting && readyToShoot)
        {
            burstBulletsLeft = bulletsPerBurst;
            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);

        Vector3 shootDirection = CalculateDirectionAndBurst().normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.transform.forward = shootDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootDirection * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, bulletLifeTime);

        if (allowReset)
        {
            StartCoroutine(ResetShot());
            allowReset = false;
        }

        if (currentGunType == GunType.Burst && burstBulletsLeft > 0)
        {
            burstBulletsLeft--;
            Shoot();
        }
    }

    Vector3 CalculateDirectionAndBurst()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawnPoint.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(bulletDelay);
        readyToShoot = true;
        allowReset = true;
    }
}