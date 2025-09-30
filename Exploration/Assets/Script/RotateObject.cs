using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject Objetotarget;
    public Camera MiCamera;
    //Variable para rotacion
    public float rotationVelocity = 1.0f;

    private void FixedUpdate()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = MiCamera.ScreenPointToRay(touch.position);//si se pone mouse en vez de touch funciona con el raton
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.transform.GetComponent<Rigidbody>() != null)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        hit.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        hit.transform.GetComponent<Rigidbody>().AddTorque(touch.deltaPosition.y * rotationVelocity, -touch.deltaPosition.x * rotationVelocity, 0);
                    }
                

                }
            }
        }
    }
}
