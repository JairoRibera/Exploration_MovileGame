using System.Collections;
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

    [Header("Detección de cercanía")]
    public IsNear isNear;

    [Header("Ejes de rotación")]
    public bool canRotX = false;
    public bool canRotY = false;
    public bool canRotZ = false;


    void Update()
    {
        if (target == null) return;
        if (isNear.isNear == true) 
        {
            if (!isNear || !isNear.isNear)
                return;

            // Leer joystick
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

            // Determinar el eje de rotación
            Vector3 rotationAxis = Vector3.zero;

            if (canRotX) rotationAxis = Vector3.right;
            else if (canRotY) rotationAxis = Vector3.up;
            else if (canRotZ) rotationAxis = Vector3.forward;

            // Aplicar rotación según dirección del joystick
            if (deltaAngle > 0)
                target.Rotate(rotationAxis * -rotationSpeed * Time.deltaTime, Space.Self);
            else if (deltaAngle < 0)
                target.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);

            lastAngle = currentAngle;
        }

    }

    private IEnumerator SnapToNearestAngle()
    {
        isSnapping = true;
        float currentAngle = 0f;

        if (canRotX) currentAngle = NormalizeAngle(target.localEulerAngles.x);
        else if (canRotY) currentAngle = NormalizeAngle(target.localEulerAngles.y);
        else if (canRotZ) currentAngle = NormalizeAngle(target.localEulerAngles.z);

        // Buscar el ángulo más cercano
        float nearest = snapAngles[0];
        float minDiff = Mathf.Abs(Mathf.DeltaAngle(currentAngle, nearest));

        foreach (float a in snapAngles)
        {
            float diff = Mathf.Abs(Mathf.DeltaAngle(currentAngle, a));
            if (diff < minDiff)
            {
                minDiff = diff;
                nearest = a;
            }
        }

        // Suavizar la rotación hacia el ángulo más cercano
        float elapsed = 0f;
        float duration = 1f / snapSpeed;
        float startAngle = currentAngle;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float smoothAngle = Mathf.LerpAngle(startAngle, nearest, t);

            Vector3 localEuler = target.localEulerAngles;

            if (canRotX) localEuler.x = smoothAngle;
            else if (canRotY) localEuler.y = smoothAngle;
            else if (canRotZ) localEuler.z = smoothAngle;

            target.localEulerAngles = localEuler;
            yield return null;
        }

        // Fijar ángulo final exacto
        Vector3 finalEuler = target.localEulerAngles;

        if (canRotX) finalEuler.x = nearest;
        else if (canRotY) finalEuler.y = nearest;
        else if (canRotZ) finalEuler.z = nearest;

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
