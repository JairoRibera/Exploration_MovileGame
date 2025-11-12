using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickItem : MonoBehaviour
{
    [Header("Settings")]
    public float grabDistance = 3f;
    public float holdDistance = 2f;
    private float moveSmoothness = 10f;
    public LayerMask grabbableLayer;
    public float followDelay = 0.15f; // Nuevo: control del delay
    //public string grabInput = "e";

    [Header("References")]
    public Transform holdPoint; // Donde se sostiene el objeto (frente al jugador)

    private Rigidbody heldObjectRb = null;
    private Transform heldObjectOriginalParent;
    private Vector3 currentVelocity; // Para SmoothDamp

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (heldObjectRb == null)
            {
                TryGrabObject();
            }
            else
            {
                return;
                //ReleaseObject();
            }
        }
        //if (Input.GetKeyDown(KeyCode.JoystickButton0))
        //{
        //    if (heldObjectRb == null)
        //    {
        //        TryGrabObject();
        //    }
        //    else
        //    {
        //        return;
        //        //ReleaseObject();
        //    }
        //}


    }
    private void FixedUpdate()
    {
        // Mover el objeto agarrado hacia el punto de sujeción
        if (heldObjectRb != null)
        {
            Vector3 targetPosition = holdPoint.position;
            //Vector3 directionToMove = targetPosition - heldObjectRb.position;
            Vector3 smoothedPosition = Vector3.SmoothDamp(heldObjectRb.position, targetPosition, ref currentVelocity, followDelay);
            //heldObjectRb.velocity = directionToMove * moveSmoothness;
            heldObjectRb.MovePosition(smoothedPosition);
            // Rotación suave para que mire hacia adelante
            Quaternion targetRotation = holdPoint.rotation;
            heldObjectRb.MoveRotation(Quaternion.Slerp(heldObjectRb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }
    }
    void TryGrabObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer))
        {
            Rigidbody targetRb = hit.collider.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                heldObjectRb = targetRb;
                heldObjectOriginalParent = heldObjectRb.transform.parent;
                heldObjectRb.transform.parent = holdPoint; // opcional: parenting visual
                heldObjectRb.useGravity = false;
                heldObjectRb.freezeRotation = true;
                targetRb.GetComponent<Collider>().enabled = false;
            }
        }
    }

    //void ReleaseObject()
    //{
    //    if (heldObjectRb != null)
    //    {
    //        heldObjectRb.useGravity = true;
    //        heldObjectRb.freezeRotation = false;
    //        heldObjectRb.velocity = Vector3.zero;
    //        heldObjectRb.transform.parent = heldObjectOriginalParent;
    //        heldObjectRb.GetComponent<Collider>().enabled = true;
    //        heldObjectRb = null;
    //    }
    //}
}
