using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    void Start()
    {
        healthBar.maxValue = GameObject.FindWithTag("Boss").GetComponent<Health>().GetMaxHealth();
        healthBar.value = healthBar.maxValue;
        GameObject.FindWithTag("Boss").GetComponent<Health>().OnHealthChanged += UpdateHealth;
    }
    void UpdateHealth(int count)
    {
        healthBar.value = count;
        if (count <= 0)
            Invoke("LoadWinScene", 1f);
    }

    void LoadWinScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
