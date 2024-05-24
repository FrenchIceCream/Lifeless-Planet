using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Slider healthBar;

    // UIManager instance;

    // void Awake()
    // {
    //     if (instance != null)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    //     else
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

    void Start()
    {
        ammoText.text = AmmoTracker.instance.GetAmmoCount().ToString();

        AmmoTracker.instance.OnAmmoChanged += UpdateAmmoCount;
        GameObject.FindWithTag("Player").GetComponent<Health>().OnHealthChanged += UpdateHealth;
        healthBar.maxValue = GameObject.FindWithTag("Player").GetComponent<Health>().GetMaxHealth();
        healthBar.value = healthBar.maxValue;
    }

    void UpdateAmmoCount(int count)
    {
        ammoText.text = count.ToString();
    }

    void UpdateHealth(int count)
    {
        healthBar.value = count;
    }
}
