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
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        input = new Vector3(_horizontal, 0f, _vertical);
        input = transform.TransformDirection(input);
    }
    private void FixedUpdate()
    {
        //no usar input en fixedupdate
        //Hay que normalizar el input para que no se mueva mas rapido en diagonal
        Vector3 _velocity = input.normalized * moveSpeed;
        //No podemor modificar la cvelocity en el eje Y del rigibody, o caera muy despacio
        _velocity.y = _rb.velocity.y;
        _rb.velocity = _velocity;
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
}
