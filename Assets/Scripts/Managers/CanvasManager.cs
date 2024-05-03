using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{    
    public void ChangeCanvas(Canvas canvas)
    {
        canvas.enabled = true;
        GetComponent<Canvas>().enabled = false;
    }
}
