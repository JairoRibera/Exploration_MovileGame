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
    //private Vector3 input;
    private Camera camera; // Asignar la cámara desde el inspector
    public float rotationSpeed = 10f;
    public float velocidadsubida = 10f;
    public bool canClimb = false;
    private bool isClimbing = false;
    private Transform ladderTransform;
    public float exitLadderPush = 5f;


    [Header("GROUND CHECKER")]
    public Transform groundCheckCenter;
    //asi no detectara al player ni a otros objetos que estorban
    public LayerMask groundLayer;
    public bool isGrounded;
    public float distanceRay = 1f;
    // Start is called before the first frame update
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

        // Convertir input a dirección relativa a la cámara
        Vector3 camForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(camera.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = (camForward * vertical + camRight * horizontal).normalized;

        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = _rb.velocity.y; // Conservar la velocidad vertical (gravedad, etc.)
        _rb.velocity = velocity;
        if (isClimbing)
        {
            Escalar();
            return;
        }
        //Rotacion
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        //if (!canMove) return;
        if (canClimb == true)
        {
            //canClimb = !canClimb;
            // Entrar o salir del modo escalera
            isClimbing = !isClimbing;

            if (isClimbing == true)
            {
                _rb.useGravity = false;
                _rb.velocity = Vector3.zero;
                AlignToLadder();
            }
            else
            {
                ExitLadder();
            }
        }

        if (isClimbing)
        {
            Escalar();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (canClimb /*&& Input.GetKeyDown(KeyCode.E)*/)
        {
            if (!isClimbing)
                StartClimbing();
            else
                ExitLadder();
        }

    }
    public void Escalar()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 climbDirection = Vector3.up * vertical * velocidadsubida;
        _rb.velocity = climbDirection;
        //// Opcional: salir al llegar arriba (si no hay más ladder)
        //if (!Physics.Raycast(transform.position, ladderTransform.forward, 1f))
        //{
        //    ExitLadder();
        //}
        // Si el jugador no se mueve y toca el suelo, salir de la escalera
        if (Physics.Raycast(groundCheckCenter.position, Vector3.down, distanceRay, groundLayer))
        {
            isClimbing = false;
            _rb.useGravity = true;
            _rb.velocity = transform.forward * exitLadderPush; // pequeño empuje al salir
        }
    }
    private void StartClimbing()
    {
        isClimbing = true;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        AlignToLadder();
    }
    private void AlignToLadder()
    {
        // Hace que el jugador mire hacia la escalera
        Vector3 lookDir = ladderTransform.position - transform.position;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(lookDir);
            transform.rotation = rot;
        }
    }
    private void ExitLadder()
    {
        isClimbing = false;
        _rb.useGravity = true;
        _rb.velocity = transform.forward * exitLadderPush; // pequeño empuje al salir
        //canMove = true;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            _rb.useGravity = false;
            canClimb = true;
            ladderTransform = other.transform;
            //canMove = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            canClimb = false;
            if (isClimbing) ExitLadder();
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundCheckCenter.position, Vector3.down * distanceRay);
    }
}
