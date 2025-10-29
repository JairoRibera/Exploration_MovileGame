using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowTheAlpha : MonoBehaviour
{
    public bool front = false;
    public Transform player;
    public LayerMask Wall;
    public Vector3 Direction;
    public GameObject cameraSilueta;
    public GameObject personajeNegro;
    //public float fadedAplha = 0.3f;
    public void DetectWall()
    {

        //Aquí estamos calculando el vector dirección desde la cámara hacia el jugador.
        Vector3 direction = player.position - transform.position;
        //Esto calcula la longitud del vector, es decir, la distancia real entre la cámara y el jugador.
        float distance = direction.magnitude;
        //El rayo sale desde la cámara, viaja en dirección al jugador, y si choca con algo en medio (antes de llegar al jugador), entra al if.
        if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, distance, Wall))
        {
            cameraSilueta.SetActive(true);
            personajeNegro.SetActive(true);
            front = true;
            Debug.Log("Hay una pared: " + hit.collider.name);
        }
        else
        {
            cameraSilueta.SetActive(false);
            personajeNegro.SetActive(false);
            front = false;
        }

    }
    void Update()
    {
        DetectWall();
    }
    private void OnDrawGizmos()
    {
        if (player == null) return;
        Gizmos.color = Color.red;
        Vector3 direction = player.position - transform.position;
        Gizmos.DrawRay(transform.position, direction.normalized * direction.magnitude);
    }
}
