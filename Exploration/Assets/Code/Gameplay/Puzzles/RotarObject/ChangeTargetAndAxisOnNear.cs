
using UnityEngine;

public class ChangeTargetAndAxisOnNear : MonoBehaviour
{
    [System.Serializable]
    //Define un “hijo rotable” con información de qué objeto es (child) y sobre qué ejes puede rotar (rotX, rotY, rotZ).
    public class RotatableChild
    {
        public Transform child;    // Hijo que será el target
        public bool rotX;
        public bool rotY;
        public bool rotZ;
    }
    [Header("Detector de proximidad")]
    public IsNear isNearDetector; //isNearDetector: referencia al script IsNear que detecta si el jugador está dentro del trigger.

    [Header("Joystick Rotator")]
    public JoystickRotator joystickRotator; // joystickRotator: referencia al script que rota los objetos con el joystick.

    [Header("Lista de hijos rotables")]
    public RotatableChild[] rotatableChildren; //rotatableChildren: lista de hijos configurables con ejes de rotación.

    private bool hasSetTarget = false; //hasSetTarget: bandera para no reasignar el target cada frame mientras el jugador sigue dentro del trigger.

    void Update()
    {
        //Se ejecuta solo una vez cuando el jugador entra, gracias a !hasSetTarget.      
        //Itera sobre la lista de rotatableChildren y asigna al primer hijo encontrado:
        //joystickRotator.target = rc.child  el hijo pasa a ser el objeto que rotará.
        //canRotX, canRotY, canRotZ  se ajustan según la configuración de ese hijo.
        //hasSetTarget = true evita que vuelva a asignar cada frame.
        if (isNearDetector.isNear && !hasSetTarget)
        {
            foreach (var rc in rotatableChildren)
            {
                // Puedes usar una condición para decidir cuál hijo es el activo
                // Por ejemplo, el primero de la lista o por proximidad
                if (rc.child != null)
                {
                    joystickRotator.target = rc.child;
                    joystickRotator.canRotX = rc.rotX;
                    joystickRotator.canRotY = rc.rotY;
                    joystickRotator.canRotZ = rc.rotZ;

                    Debug.Log("Target cambiado a: " + rc.child.name + " | Ejes: " +
                        "X=" + rc.rotX + " Y=" + rc.rotY + " Z=" + rc.rotZ);

                    hasSetTarget = true;
                    break; // solo asigna el primero encontrado
                }
            }
        }
        //Vuelve el target al padre(transform) o a un estado por defecto.
        //Desactiva todos los ejes(canRotX = false, etc.) para que el joystick no siga rotando nada.
        //hasSetTarget = false permite que la próxima vez que el jugador entre, se vuelva a asignar el target del hijo.
        if (!isNearDetector.isNear && hasSetTarget)
        {
            // Opcional: vuelve al padre
            joystickRotator.target = transform;
            joystickRotator.canRotX = false;
            joystickRotator.canRotY = false;
            joystickRotator.canRotZ = false;
            Debug.Log("Target vuelto al padre: " + transform.name);

            hasSetTarget = false;
        }
    }
}
