using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowTheAlpha : MonoBehaviour
{
    public bool front = false;
    public Transform player;
    public LayerMask Wall;
    public Vector3 Direction;
    public MeshRenderer materialObjeto;
    private float alpha = 0.3f;
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
            front = true;
            Debug.Log("Hay una pared: " + hit.collider.name);
        }
        else
        {
            front = false;
        }
        materialObjeto = hit.collider.GetComponent<MeshRenderer>();
        if (materialObjeto != null)
        {
            // Obtener color del material
            Color color = materialObjeto.material.color;
            //hacemos un float para conseguir el alpha
            //float alpha = color.a;
            color.a = alpha;
            materialObjeto.material.color = color;
            Debug.Log("Alpha del material: " + alpha);
        }
        else return;
    }
    public void FadedAlpha()
    {

    }
    // Update is called once per frame
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
