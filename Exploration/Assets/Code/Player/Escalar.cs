using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalar : MonoBehaviour
{
    [Header("Escalar")]
    public float velocidadsubida = 10f;
    public bool canClimb = false;
    private bool isClimbing = false;
    private Transform ladderTransform;
    public float exitLadderPush = 5f;
    public Rigidbody _rb;

    [Header("GROUND CHECKER")]
    public Transform groundCheckCenter;
    //asi no detectara al player ni a otros objetos que estorban
    public LayerMask groundLayer;
    public bool isGrounded;
    public float distanceRay;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (canClimb /*&& Input.GetKeyDown(KeyCode.E)*/)
        //{
        //    if (!isClimbing)
        //        StartClimbing();
        //    else
        //        ExitLadder();
        //}
        if (Physics.Raycast(groundCheckCenter.position, Vector3.down, distanceRay, groundLayer))
        {
            isGrounded = true;
        }
        else isGrounded = false;
    }
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            EscalarLadder();
        }
        //if (!canMove) return;
        if (canClimb == true)
        {
            StartClimbing();
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
    }
    public void EscalarLadder()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 climbDirection = Vector3.up * vertical * velocidadsubida;
        _rb.velocity = climbDirection;
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
        //_rb.AddForce(transform.forward * exitLadderPush, ForceMode.VelocityChange);
        //canMove = true;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundCheckCenter.position, Vector3.down * distanceRay);
    }
}
