using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    void Update()
    {
        gameObject.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
