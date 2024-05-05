using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    void Start()
    {
        ammoText.text = AmmoTracker.instance.GetAmmoCount().ToString();

        AmmoTracker.instance.OnAmmoChanged += UpdateAmmoCount;
        GameObject.FindWithTag("Player").GetComponent<Health>().OnHealthChanged += UpdateHealth;
    }

    void UpdateAmmoCount(int count)
    {
        ammoText.text = count.ToString();
    }

    void UpdateHealth(int count)
    {
        Debug.Log(count);
    }
}
