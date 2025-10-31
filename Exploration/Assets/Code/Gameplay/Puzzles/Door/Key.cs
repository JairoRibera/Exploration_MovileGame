using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool hasKey = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasKey = true;
            gameObject.SetActive(false);
        }
    }
}
