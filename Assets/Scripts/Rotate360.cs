using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    [SerializeField] bool moveUpAndDown = false;
    [SerializeField] float upAndDownMovement = 0.5f;
    Vector3 offset = new Vector3(0, 0, 0);
    void Update()
    {
        if (moveUpAndDown)
            gameObject.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * Mathf.PI) * upAndDownMovement, 0);
        gameObject.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
