using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggOrbit : MonoBehaviour
{
    public EggAnchor anchor;
    public int index;

    public float separacion = 0.5f;
    public float amplitud = 0.15f;
    public float velocidad = 3f;
    public float suavizado = 15f;

    float desfase;

    void Start()
    {
        desfase = index * 0.6f;
    }

    void FixedUpdate()
    {
        Vector3 atras = -anchor.transform.forward;

        Vector3 basePos =
            anchor.anchorPos +
            atras * (index * separacion);

        float flotacion =
            Mathf.Sin(Time.time * velocidad + desfase) * amplitud;

        Vector3 objetivo = basePos + Vector3.up * flotacion;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            objetivo,
            ref _vel,
            0.08f
        );
    }

    Vector3 _vel;
}

