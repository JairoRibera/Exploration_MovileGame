using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNear : MonoBehaviour
{
    public bool isNear = false;
    public bool canInteract = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            isNear = true;
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNear = false;
            canInteract = false;
        }
    }
}
