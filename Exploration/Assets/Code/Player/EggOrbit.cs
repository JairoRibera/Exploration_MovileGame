using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggOrbit : MonoBehaviour
{


    public EggAnchor anchor;
    public int index;

    public float alturaSobreSuelo = 0.4f;
    public LayerMask groundMask;
    public float rayodistance;
    public float separacion = 0.5f;
    public float amplitud = 0.15f;
    public float velocidad = 3f;
    public float suavizado = 15f;
    public bool rampa;
    float desfase;
    Vector3 vel;
    public Escalar escalarRef;
    void Start()
    {
        desfase = index * 0.6f;
    }
    void FixedUpdate()
    {
        Orbit();
        ClimgOrbit();
    }
    void ClimgOrbit()
    {
        if(escalarRef.isClimbing == true)
        {
            Debug.Log("BolaSSubiendo");
            anchor.ClimbEgg();
        }
    }
    void Orbit()
    {
        Vector3 atras = -anchor.transform.forward;

        Vector3 basePos = anchor.anchorPos + atras * (index * separacion);

        //Flotar
        float flotacion = Mathf.Sin(Time.time * velocidad + desfase) * amplitud;

        // Raycast al suelo
        Ray ray = new Ray(basePos + Vector3.up, Vector3.down);
        float sueloY = basePos.y;
        if (Physics.Raycast(ray, out RaycastHit hit, rayodistance, groundMask))
        {
            sueloY = hit.point.y;
            rampa = true;
        }
        else
        {
            rampa = false;
        }

        if (rampa == true)
        {
            basePos.y = sueloY + .5f + flotacion; // Si hay rampa, ajusta a la altura del suelo
        }
        else
        {
            // Si no hay rampa, bajamos la bola
            basePos.y -= alturaSobreSuelo; // Baja la bola si no hay rampa
        }


        basePos.y = sueloY + alturaSobreSuelo + flotacion;

        //Vector3 objetivo = basePos + Vector3.up * flotacion;

        transform.position = Vector3.SmoothDamp(transform.position, basePos, ref vel, 0.08f);

    }
    private void OnDrawGizmos()
    {
        Vector3 atras = -anchor.transform.forward;
        Vector3 basePos = anchor.anchorPos + atras * (index * separacion);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * rayodistance);
        //Ray ray = new Ray(basePos + Vector3.up, Vector3.down);
    }
}

