using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggOrbit : MonoBehaviour
{
    //public EggAnchor anchor;//anchor: referencia a un objeto que marca la posición base (normalmente el jugador).
    public int index;//index: posición del huevo en la fila (0, 1, 2…), sirve para separarlos.

    public float alturaSobreSuelo = 0.4f;//qué tan alto flota sobre el suelo.
    public LayerMask groundMask;//qué capas se consideran suelo.
    public float rayodistance;//distancia del raycast hacia abajo.
    public float separacion = 0.5f;//distancia entre huevos.
    public float amplitud = 0.15f;//altura del movimiento de flotación.
    public float velocidad = 3f;//velocidad del movimiento sinusoidal.
    public bool rampa; //indica si hay rampa debajo.
    float desfase;//offset para que cada huevo flote diferente.
    Vector3 vel;
    Vector3 posInicial;
    public Escalar escalarRef;
    public static bool isGrab;
    void Start()
    {
        desfase = index * 0.6f;
        //Cada huevo tiene un desfase distinto para que no floten todos sincronizados.
        //Cuanto mayor el index, más tarde sube/baja.
        posInicial = transform.position;
    }
    void FixedUpdate()
    {
        if(isGrab == false)
        {
            Orbit();
        }
        else
        {
            OrbitPJ();
            ClimgOrbit();
        }
    }
    void ClimgOrbit()
    {
        if(escalarRef.isClimbing == true)
        {
            Debug.Log("BolaSSubiendo");
            EggAnchor.Instance.ClimbEgg();
        }
    }
    public void Orbit()
    {
        float flotacion = Mathf.Sin(Time.time * velocidad + desfase) * amplitud;
        Vector3 pos = posInicial;
        pos.y += flotacion;

        transform.position = pos;
    }
    void OrbitPJ()
    {
        Vector3 atras = -EggAnchor.Instance.transform.forward;//Coloca el huevo detrás del jugador.

        Vector3 basePos = EggAnchor.Instance.anchorPos + atras * (index * separacion);//Cada huevo se separa usando index.

        //Flotar
        float flotacion = Mathf.Sin(Time.time * velocidad + desfase) * amplitud;
        //Movimiento sinusoidal arriba/abajo.
        //Desfase diferente por huevo a efecto natural.

        // Raycast al suelo
        Ray ray = new Ray(basePos + Vector3.up, Vector3.down);
        float sueloY = basePos.y;
        //Lanza un rayo desde arriba hacia abajo para detectar suelo.

        if (Physics.Raycast(ray, out RaycastHit hit, rayodistance, groundMask))
        {
            sueloY = hit.point.y;//Guarda la altura del impacto.
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

        //transform.position = Vector3.SmoothDamp(transform.position, basePos, ref vel, 0.08f);
        transform.position = Vector3.SmoothDamp(transform.position, basePos, ref vel, 0.08f);

    }
    private void OnDrawGizmos()
    {
        ////Vector3 atras = -EggAnchor.Instance.transform.forward;
        //Vector3 basePos =   EggAnchor.Instance.anchorPos + atras * (index * separacion);
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, Vector3.down * rayodistance);
        ////Ray ray = new Ray(basePos + Vector3.up, Vector3.down);
    }
}

