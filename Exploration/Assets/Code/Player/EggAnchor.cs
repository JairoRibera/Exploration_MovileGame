using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggAnchor : MonoBehaviour
{
    public float distancia = 1.2f;
    public float suavizado = 12f;

    public Vector3 anchorPos { get; private set; }

    void Start()
    {
        anchorPos = transform.position;
    }

    void LateUpdate()
    {
        Vector3 target = transform.position - transform.forward * distancia;

        anchorPos = Vector3.Lerp(anchorPos,target,suavizado * Time.deltaTime);
    }
}
