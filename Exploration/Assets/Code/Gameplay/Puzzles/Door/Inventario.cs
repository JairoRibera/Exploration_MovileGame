using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    private Door doorRef;
    private Key keyRef;
    // Start is called before the first frame update
    void Start()
    {
        doorRef = GameObject.FindWithTag("Door").GetComponent<Door>();
        keyRef = GameObject.FindWithTag("Key").GetComponent<Key>();
    }

    // Update is called once per frame
    void Update()
    {
        IsComplete();
    }
    public void IsComplete()
    {
        if (keyRef.hasKey == true)
        {
            Debug.Log("Se ha completado");
            doorRef.OpenDoor();
        }
    }
}
