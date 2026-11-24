using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform InitialPos;
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            other.transform.position = InitialPos.transform.position;
        }
    }
}
