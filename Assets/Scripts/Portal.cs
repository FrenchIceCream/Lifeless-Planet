using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] Material offMaterial;
    [SerializeField] Material onMaterial;
    void Start()
    {
        GetComponent<MeshRenderer>().material = offMaterial;
    }

    public void ActivatePortal()
    {
        GetComponent<MeshRenderer>().material = onMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().HasKey())
        {
            other.gameObject.GetComponent<PlayerController>().SetKey(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
