using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaUp : MonoBehaviour
{
    public float speed;
    public Transform posFinal;
    private Vector3 posInicio;
    public bool isDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        posInicio = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetected();
    }
    public void PlayerDetected()
    {
        if (isDetected == true)
        {
            transform.position = new Vector3(posFinal.transform.position.x, posFinal.transform.position.y, posFinal.transform.position.z) *speed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isDetected = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isDetected = false;
    //    }
    //}
}
