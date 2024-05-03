using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    void Awake()
    {
        foreach (Gun gun in FindObjectsByType<Gun>(FindObjectsSortMode.None)) 
        {
            gun.OnShoot += UpdateAmmoCount;
        }
    }

    void Start()
    {
        ammoText.text = AmmoTracker.instance.GetAmmoCount().ToString();
    }

    void UpdateAmmoCount(int count)
    {
        ammoText.text = count.ToString();
    }
}
