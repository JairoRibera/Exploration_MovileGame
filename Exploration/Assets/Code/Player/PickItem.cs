using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickItem : MonoBehaviour
{
    [Header("Settings")]
    public float grabDistance = 3f;
    public float holdDistance = 2f;
    public LayerMask grabbableLayer;
    //public float followDelay = 0.15f; // Nuevo: control del delay
    //public string grabInput = "e";
    public float speed;
    [Header("References")]
    public Transform holdPoint; // Donde se sostiene el objeto (frente al jugador)
    private Rigidbody heldObjectRb = null;
    private List<Rigidbody> heldObjects = new List<Rigidbody>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            TryGrabObject();
        }
    }
    private void FixedUpdate()
    {
        if (heldObjects.Count == 0)
            return;

        // Mover el primer objeto al holdPoint
        MoveObjectTowards(heldObjects[0], holdPoint.position);

        // Mover el resto en cadena
        for (int i = 1; i < heldObjects.Count; i++)
        {
            Rigidbody prev = heldObjects[i - 1];
            Rigidbody current = heldObjects[i];

            MoveObjectTowards(current, prev.position - prev.transform.forward * 1.0f);
            // distancia entre objetos (1.0f), ajustable
        }

    }

    void MoveObjectTowards(Rigidbody rb, Vector3 targetPos)
    {
        Vector3 direction = targetPos - rb.position;
        float dist = direction.magnitude;

        if (dist < 0.05f)
        {
            rb.MovePosition(targetPos);
        }
        else
        {
            Vector3 move = direction.normalized * speed * Time.fixedDeltaTime;
            if (move.magnitude > dist)
                move = direction;

            rb.MovePosition(rb.position + move);
        }

        // rotación suave hacia adelante como tu versión anterior
        rb.MoveRotation(Quaternion.Slerp(rb.rotation,holdPoint.rotation,Time.fixedDeltaTime * 10f));
    }
    public void TryGrabObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer))
        {
            //Rigidbody targetRb = hit.collider.GetComponent<Rigidbody>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null && !heldObjects.Contains(rb))
            {
                rb.useGravity = false;
                rb.freezeRotation = true;
                rb.GetComponent<Collider>().enabled = false;

                heldObjects.Add(rb);
            }
        }
    }
}
