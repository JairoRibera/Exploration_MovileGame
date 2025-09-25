using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Centro del escenario

    [Header("Orbit Settings")]
    public float rotationSpeed = 50f; // Velocidad de rotación
    public float zoomSpeed = 5f;      // Velocidad de zoom
    public float minZoom = 5f;        // Distancia mínima
    public float maxZoom = 20f;       // Distancia máxima

    private float currentZoom = 10f;  // Zoom inicial
    private float rotation_horizontal = 0f;           // Rotación horizontal
    private float pitch = 30f;        // Rotación vertical fija (como Captain Toad)

    void Update()
    {
        // --- Rotación ---
        float horizontal = Input.GetAxis("OrbitalCamera"); // A/D o flechas
        rotation_horizontal += horizontal * rotationSpeed * Time.deltaTime;

        //// --- Zoom ---
        //float scroll = Input.GetAxis("Mouse ScrollWheel");
        //currentZoom -= scroll * zoomSpeed;
        //currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        // Calcula la posición de la cámara en órbita
        Vector3 offset = new Vector3(0, 0, -currentZoom);
        Quaternion rotation = Quaternion.Euler(pitch, rotation_horizontal, 0);

        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position);
    }
}
