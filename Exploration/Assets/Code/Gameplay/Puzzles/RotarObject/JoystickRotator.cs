using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickRotator : MonoBehaviour
{
    [Header("Objeto a Rotar")]
    public Transform target; // Objeto que rotará

    [Header("Velocidad de rotación")]
    public float rotationSpeed = 100f;

    [Header("Velocidad del encaje (snap)")]
    public float snapSpeed = 5f; // Qué tan rápido se alinea al ángulo más cercano

    private float lastAngle;
    private bool hasLastAngle = false;
    private bool isSnapping = false; // Si el objeto está encajando

    // Ángulos válidos a los que se "encaja"
    private readonly float[] snapAngles = { -180f, -90f, 0f, 90f, 180f };

    public IsNear isNear;

    void Update()
    {
        if (isNear.isNear == true) 
        {
            // Leer joystick izquierdo
            float horizontal = Input.GetAxis("OrbitalCameraHorizontal");
            float vertical = Input.GetAxis("OrbitalCameraVertical");

            Vector2 stickInput = new Vector2(horizontal, vertical);

            // Si el stick está centrado, iniciar el encaje
            if (stickInput.magnitude < 0.3f)
            {
                hasLastAngle = false;

                if (!isSnapping)
                    StartCoroutine(SnapToNearestAngle());

                return;
            }

            // Mientras se gira, cancelar cualquier encaje automático
            StopAllCoroutines();
            isSnapping = false;

            // Obtener ángulo actual del stick
            float currentAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;

            if (!hasLastAngle)
            {
                lastAngle = currentAngle;
                hasLastAngle = true;
                return;
            }

            // Diferencia angular entre frames
            float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);

            // Rotar en eje Z según dirección
            if (deltaAngle > 0)
                target.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime, Space.Self);
            else if (deltaAngle < 0)
                target.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);

            lastAngle = currentAngle;
        }

    }

    private System.Collections.IEnumerator SnapToNearestAngle()
    {
        isSnapping = true;

        // Obtener ángulo Z actual normalizado a [-180, 180]
        float currentZ = NormalizeAngle(target.localEulerAngles.z);

        // Buscar el ángulo más cercano en el array
        float nearest = snapAngles[0];
        float minDiff = Mathf.Abs(Mathf.DeltaAngle(currentZ, nearest));

        foreach (float a in snapAngles)
        {
            float diff = Mathf.Abs(Mathf.DeltaAngle(currentZ, a));
            if (diff < minDiff)
            {
                minDiff = diff;
                nearest = a;
            }
        }

        // Suavizar la rotación hacia el ángulo más cercano
        float elapsed = 0f;
        float duration = 1f / snapSpeed;
        float startAngle = currentZ;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float smoothAngle = Mathf.LerpAngle(startAngle, nearest, t);

            Vector3 localEuler = target.localEulerAngles;
            localEuler.z = smoothAngle;
            target.localEulerAngles = localEuler;

            yield return null;
        }

        // Fijar ángulo final exacto
        Vector3 finalEuler = target.localEulerAngles;
        finalEuler.z = nearest;
        target.localEulerAngles = finalEuler;

        isSnapping = false;
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
