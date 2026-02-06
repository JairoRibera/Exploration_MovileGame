using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsNear : MonoBehaviour
{
    [Header("Referencia al detector")]
    public IsNear isNearDetector;

    [Header("Joystick Rotator a controlar")]
    public JoystickRotator joystickRotator;

    [Header("Nombre del hijo que será target")]
    public string childName;

    private bool hasSetTarget = false;
    public bool isNear = false;
    public bool canInteract = false;
    public GameObject TextA;
    private void Update()
    {
        // Si el jugador está cerca y no se ha cambiado el target todavía
        if (isNearDetector.isNear && !hasSetTarget)
        {
            Transform child = transform.Find(childName);
            if (child != null)
            {
                joystickRotator.target = child;
                Debug.Log("Target cambiado a: " + child.name);
                hasSetTarget = true; // para que no se repita cada frame
            }
            else
            {
                return;
            }
        }

        // Si el jugador se aleja, podemos volver a un target por defecto
        if (!isNearDetector.isNear && hasSetTarget)
        {
            joystickRotator.target = transform; // opcional: vuelve al padre
            Debug.Log("Target vuelto al padre: " + transform.name);
            hasSetTarget = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            TextA.SetActive(true);
            isNear = true;
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TextA.SetActive(false);
            isNear = false;
            canInteract = false;
        }
    }
}
