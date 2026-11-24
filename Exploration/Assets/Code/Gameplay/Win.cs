using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public GameObject WinText;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinText.SetActive(true);
        }
    }
}
