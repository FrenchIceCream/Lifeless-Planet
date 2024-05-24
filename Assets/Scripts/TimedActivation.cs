using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedActivation : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] GameObject[] objectsToActivate;

    void Start()
    {
        Invoke("Activate", time);
    }
    
    void Activate()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
