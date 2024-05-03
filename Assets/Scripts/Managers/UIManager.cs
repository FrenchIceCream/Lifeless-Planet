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
    }

    void UpdateAmmoCount(int count)
    {
        ammoText.text = count.ToString();
    }
}
