using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento Player")]
    public float moveSpeed = 5f;
    private Rigidbody _rb;
    private bool canMove = true;
    private Vector3 input;
    public Camera camera; // Asignar la cámara desde el inspector
    public float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!canMove) return;


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

        // Convertir input a dirección relativa a la cámara
        Vector3 camForward = Vector3.Scale(camera.transform.forward, new Vector3(1,0,1)).normalized;
        Vector3 camRight = Vector3.Scale(camera.transform.right, new Vector3(1,0,1)).normalized;
        Vector3 moveDir = (camForward * vertical + camRight * horizontal).normalized;

        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = _rb.velocity.y; // Conservar la velocidad vertical (gravedad, etc.)
        _rb.velocity = velocity;

        //Rotacion
        if(moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
    public void MovePlayer()
    {
        if (canMove == true)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            _rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
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
