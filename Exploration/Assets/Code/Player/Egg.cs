using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public Transform player;
    public int index;

    public float distanciaBase = 1.2f;
    public float separacion = 0.5f;

    public float amplitud = 0.15f;
    public float velocidad = 3f;
    public float suavizado = 10f;

    float desfase;

    void Start()
    {
        desfase = index * 0.6f;
    }

    void LateUpdate()
    {
        Vector3 atras = -player.forward;
        Vector3 basePos =
            player.position +
            atras * (distanciaBase + index * separacion);

        float flotacion =
            Mathf.Sin(Time.time * velocidad + desfase) * amplitud;

        Vector3 objetivo = basePos + Vector3.up * flotacion;

        transform.position = Vector3.Lerp(transform.position,objetivo,suavizado * Time.deltaTime);

        // Opcional: que miren en la misma dirección que el jugador
        transform.rotation = player.rotation;
    }
}
