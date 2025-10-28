using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    //[Header("Target")]
    public Transform target; // Centro del escenario

    [Header("Orbit Settings")]
    public float rotationSpeed = 50f; // Velocidad de rotación
    public float zoomSpeed = 5f;      // Velocidad de zoom
    public float minZoom = 5f;        // Distancia mínima
    public float maxZoom = 20f;       // Distancia máxima

    [Header("Pitch Settings")]
    public float minPitch = 10f;      // Límite inferior de ángulo vertical
    public float maxPitch = 80f;      // Límite superior de ángulo vertical

    private float currentZoom = 10f;  // Zoom inicial
    private float rotation_horizontal = 0f;           // Rotación horizontal
    private float pitch = 30f;        // Rotación vertical fija (como Captain Toad)

    public bool canOrbit = true;
    public void Start()
    {
        canOrbit = true;
    }
    void Update()
    {
        if(canOrbit == true)
        {
            float horizontal = Input.GetAxis("OrbitalCameraHorizontal"); // A/D o flechas
            rotation_horizontal += horizontal * rotationSpeed * Time.deltaTime;

            float vertical = Input.GetAxis("OrbitalCameraVertical"); // W/S o flechas
            pitch += vertical * rotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            //// --- Zoom ---
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            currentZoom -= scroll * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }
    }

    void LateUpdate()
    {
        if (canOrbit == true) 
        {
            // Calcula la posición de la cámara en órbita
            Vector3 offset = new Vector3(0, 0, -currentZoom);
            Quaternion rotation = Quaternion.Euler(pitch, rotation_horizontal, 0);

            transform.position = target.position + rotation * offset;
            transform.LookAt(target.position);
        }
    }
}
