using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento Player")]
    public float moveSpeed = 5f;
    public Rigidbody _rb;
    public bool canMove = true;
    private Camera camera; // Asignar la cámara desde el inspector
    public float rotationSpeed = 10f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }
    private void FixedUpdate()
    {

        ////no usar input en fixedupdate
        ////Hay que normalizar el input para que no se mueva mas rapido en diagonal
        //Vector3 _velocity = input.normalized * moveSpeed;
        ////No podemor modificar la cvelocity en el eje Y del rigibody, o caera muy despacio
        if (!canMove) return;
        // Obtener input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(canMove == true)
        {
            // Convertir input a dirección relativa a la cámara
            Vector3 camForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 camRight = Vector3.Scale(camera.transform.right, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDir = (camForward * vertical + camRight * horizontal).normalized;
            Vector3 velocity = moveDir * moveSpeed;
            velocity.y = _rb.velocity.y; // Conservar la velocidad vertical (gravedad, etc.)
            _rb.velocity = velocity;
            //Rotacion
            if (moveDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma")) transform.parent = collision.transform;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma")) transform.parent = null;
    }
}
