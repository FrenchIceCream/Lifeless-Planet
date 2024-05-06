using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTracker : MonoBehaviour
{
    public delegate void OnAmmoChangedDelegate(int newValue);
    public event OnAmmoChangedDelegate OnAmmoChanged;
    public static AmmoTracker instance;
    [SerializeField] int maxAmmo = 30;
    int ammoCount;
    public int GetAmmoCount() => ammoCount;
    public int GetMaxAmmo() => maxAmmo;
    public bool HasAmmo() => ammoCount > 0;
    public void AddAmmo(int amount)
    {
        if (ammoCount + amount > maxAmmo)
            ammoCount = maxAmmo;
        else
            ammoCount += amount;

        if (OnAmmoChanged != null)
            OnAmmoChanged(ammoCount);
    }
    public void SubtractAmmo(int amount)
    {
        if (ammoCount - amount < 0)
            ammoCount = 0;
        else
            ammoCount -= amount;

        if (OnAmmoChanged != null)
            OnAmmoChanged(ammoCount);
    }

    void Awake()
    {
        if (FindObjectsByType<AmmoTracker>(FindObjectsSortMode.None).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            ammoCount = maxAmmo;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetAmmo()
    {
        ammoCount = maxAmmo;
        if (OnAmmoChanged != null)
            OnAmmoChanged(ammoCount);
    }
}
