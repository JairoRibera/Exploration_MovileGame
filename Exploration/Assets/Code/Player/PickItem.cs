using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickItem : MonoBehaviour
{
    [Header("Settings")]
    public float grabDistance = 3f;
    public LayerMask grabbableLayer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            TryGrabObject();
        }
    }
    public void TryGrabObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer))
        {
            Debug.Log("Agarrar webo");
            EggOrbit.isGrab = true;
        }
    }
}
