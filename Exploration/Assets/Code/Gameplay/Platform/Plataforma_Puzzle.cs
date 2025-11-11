using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Puzzle : MonoBehaviour
{
    public Vector3 PosInitial;
    //public GameObject posInicial;
    public bool canMove;
    public float speed;
    //public Transform platformPos;
    public Transform posFinal;
    public bool goingEnd;
    private float Tiempo = 2f;
    private float contador = 0;
    // Start is called before the first frame update
    void Start()
    {
        PosInitial = transform.position;
        contador = Tiempo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        if (goingEnd)
        {
            // Mueve hacia la posición final
            transform.position = Vector3.MoveTowards(transform.position, posFinal.position, speed * Time.deltaTime);

            // Si llegó (o está muy cerca)
            if (Vector3.Distance(transform.position, posFinal.position) < 0.01f)
            {
                contador -= Time.deltaTime;
                if(contador <= 0)
                {
                    goingEnd = false; // Cambia dirección
                    contador = Tiempo;
                }
            }
        }
        else
        {
            // Mueve hacia la posición inicial
            transform.position = Vector3.MoveTowards(transform.position, PosInitial, speed * Time.deltaTime);

            // Si volvió al inicio
            if (Vector3.Distance(transform.position, PosInitial) < 0.01f)
            {
                contador -= Time.deltaTime;
                if (contador <= 0)
                {
                    goingEnd = true; // Cambia dirección
                    contador = Tiempo;
                }
                //goingEnd = true; // Cambia dirección
            }
        }
    }
}
