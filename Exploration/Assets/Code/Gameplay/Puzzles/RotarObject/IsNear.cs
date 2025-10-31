using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsNear : MonoBehaviour
{
    public bool isNear = false;
    public bool canInteract = false;
    public GameObject TextA;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            TextA.SetActive(true);
            isNear = true;
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TextA.SetActive(false);
            isNear = false;
            canInteract = false;
        }
    }
}
