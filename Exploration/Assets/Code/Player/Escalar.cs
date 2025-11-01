using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalar : MonoBehaviour
{
    [Header("Escalar")]
    public float velocidadsubida = 10f;
    public bool canClimb = false;//booleana para saber si puede escalar
    public bool isClimbing = false;//booleana para saber si está escalando
    private Transform ladderTransform;//transform de la escalera
    public float exitLadderPush = 5f;//Fuerza para salir de la escalera
    private Rigidbody _rb;

    [Header("GROUND CHECKER")]
    public Transform groundCheckCenter;
    //asi no detectara al player ni a otros objetos que estorban
    public LayerMask groundLayer;
    public bool isGrounded;
    public float distanceRay;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Lanzamos un rasho, para saber si está tocando el suelo
        isGrounded = Physics.Raycast(groundCheckCenter.position, Vector3.down, distanceRay, groundLayer);
        isGrounded = Physics.Raycast(groundCheckCenter.position, Vector3.down, distanceRay, groundLayer);
        CheckForLadderBelow();
    }
    private void FixedUpdate()
    {
        if (canClimb == true)
        {
            // Entrar o salir del modo escalera
            //Esto hace que cada vez que canClimb sea verdadero (en cada frame de FixedUpdate),
            //cambie continuamente entre subir y no subir, miles de veces por segundo.
            //Por eso parece que el jugador “parpadea” o “tiembla” en la escalera.
            //isClimbing = !isClimbing;
            if (isClimbing == true)
            {
                EscalarLadder();
                _rb.useGravity = false;
                _rb.velocity = Vector3.zero;
                AlignToLadder();

            }
            else
            {
                ExitLadder();
            }
        }
        //if (isClimbing)
        //{
            //EscalarLadder();
        //}

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
        //Hacemos un vector, que será la resta de la pos de la escalera y del jugador
        Vector3 lookDir = ladderTransform.position - transform.position;
        lookDir.y = 0;
        //Si el valor de Y es distinto de 0 entonces rotamos el jugador para que mire a la escalera
        if (lookDir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(lookDir);
            transform.rotation = rot;

        }
    }
    public void EscalarLadder()
    {
        playerController.canMove = false;
        //Obtenemos el input vertical
        float vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * vertical * Time.deltaTime*velocidadsubida); 
        
        //Creamos un vector para la subida y le añadimos la velocidadsubida
        //Vector3 climbDirection = Vector3.up * vertical * velocidadsubida;
        //_rb.velocity = climbDirection;
        //Si el input es menor a 0, es decir que está pulsando para bajar y está tocando el suelo
        if (vertical < 0 && isGrounded)
        {
            //Activamos la gravedad, ponemos en falso el isClimbing, y le añadimos una addforce para que se aleje del collider
            isClimbing = false;
            _rb.useGravity = true;
            _rb.AddForce(transform.forward * exitLadderPush, ForceMode.VelocityChange);
            playerController.canMove = true;
        }
    }
    private void ExitLadder()
    {
        playerController.canMove = true;
        isClimbing = false;
        _rb.useGravity = true;
        _rb.velocity = transform.forward * exitLadderPush; // pequeño empuje al salir
        //_rb.AddForce(transform.forward * exitLadderPush, ForceMode.VelocityChange);
        //canMove = true;

    }
    void CheckForLadderBelow()
    {
        // Lanzamos un raycast hacia abajo para ver si hay una escalera justo debajo
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            if (hit.collider.CompareTag("Stair"))
            {
                canClimb = true;
                ladderTransform = hit.collider.transform;

                // Si el jugador pulsa hacia abajo, empieza a escalar
                if (Input.GetAxisRaw("Vertical") < 0 && !isClimbing)
                {
                    StartClimbing();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            canClimb = true;
            ladderTransform = other.transform;
            StartClimbing();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            playerController.canMove = true;
            canClimb = false;
            if (isClimbing) ExitLadder();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundCheckCenter.position, Vector3.down * distanceRay);
    }
}
