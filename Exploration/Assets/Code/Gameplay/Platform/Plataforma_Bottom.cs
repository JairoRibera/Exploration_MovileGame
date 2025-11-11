using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Bottom : MonoBehaviour
{
    public bool haPulsado;
    public float speed;
    public Vector3 PosInitial;
    public bool isInEnd;
    public Transform posFinal;
    // Start is called before the first frame update
    void Start()
    {
        PosInitial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Viaje();
    }
    public void Viaje()
    {
        if (haPulsado)
        {
            if (!isInEnd)
            {
                // Va hacia la posición final
                transform.position = Vector3.MoveTowards(transform.position, posFinal.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, posFinal.position) < 0.01f)
                {
                    isInEnd = true;
                    haPulsado = false;
                }
            }
            else
            {
                // Vuelve a la posición inicial
                transform.position = Vector3.MoveTowards(transform.position, PosInitial, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, PosInitial) < 0.01f)
                {
                    isInEnd = false;
                    haPulsado = false;
                }
            }
        }
    }
    // Esta función la puedes llamar desde el botón del escenario
    public void Activar()
    {
        haPulsado = true;
    }
}
