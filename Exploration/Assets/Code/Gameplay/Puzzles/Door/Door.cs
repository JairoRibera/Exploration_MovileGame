using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool onRange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onRange = false;
        }
    }
    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
