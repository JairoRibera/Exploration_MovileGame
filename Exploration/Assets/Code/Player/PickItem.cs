using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    [Header("Settings")]
    public float grabDistance = 3f;
    public float holdDistance = 2f;
    public float moveSmoothness = 10f;
    public LayerMask grabbableLayer;
    public string grabInput = "e";

    [Header("References")]
    public Transform holdPoint; // Donde se sostiene el objeto (frente al jugador)

    private Rigidbody heldObjectRb = null;
    private Transform heldObjectOriginalParent;

    void Update()
    {
        if (Input.GetKeyDown(grabInput))
        {
            if (heldObjectRb == null)
            {
                TryGrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }

        // Mover el objeto agarrado hacia el punto de sujeción
        if (heldObjectRb != null)
        {
            Vector3 targetPosition = holdPoint.position;
            Vector3 directionToMove = targetPosition - heldObjectRb.position;

            heldObjectRb.velocity = directionToMove * moveSmoothness;
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
            }
        }
    }

    void ReleaseObject()
    {
        if (heldObjectRb != null)
        {
            heldObjectRb.useGravity = true;
            heldObjectRb.freezeRotation = false;
            heldObjectRb.velocity = Vector3.zero;
            heldObjectRb.transform.parent = heldObjectOriginalParent;
            heldObjectRb = null;
        }
    }
}
