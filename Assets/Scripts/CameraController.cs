using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

/// <summary>
/// This class uses processed input from the input manager to control the vertical rotation of the camera
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 60f;
    [SerializeField] bool invert = true;
    InputManager inputManager;
    Camera controledCamera;

    void Awake()
    {
        controledCamera = Camera.main;
    }

    void Start()
    {
        inputManager = InputManager.instance;
    }

    int waitForFrames = 3;
    int framesWaited = 0;

    void Update()
    {
        if (framesWaited <= waitForFrames)
        {
            framesWaited += 1;
            return;
        }
        ProcessRotation();
    }


    void ProcessRotation()
    {
        float verticalLookInput = inputManager.GetVerticalLookAxis();
        Vector3 cameraRotation = controledCamera.transform.rotation.eulerAngles;
        float newXRotation;
        if (invert)
        {
            newXRotation  = cameraRotation.x - verticalLookInput * rotationSpeed * Time.deltaTime;
        }
        else
        {
            newXRotation = cameraRotation.x + verticalLookInput * rotationSpeed * Time.deltaTime;
        }

        if (newXRotation < 270 && newXRotation >= 180)
        {
            newXRotation = 270;
        }
        else if (newXRotation > 90 && newXRotation < 180)
        {
            newXRotation = 90;
        }
        controledCamera.transform.rotation = Quaternion.Euler(new Vector3(newXRotation, cameraRotation.y, cameraRotation.z));
    }
}
