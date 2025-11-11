using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public Door doorRef;
    public Key keyRef;
    // Start is called before the first frame update
    void Start()
    {
        //doorRef = GameObject.FindWithTag("Door").GetComponent<Door>();
        keyRef = GameObject.FindWithTag("Key").GetComponent<Key>();
    }

    // Update is called once per frame
    void Update()
    {
        IsComplete();
    }
    public void IsComplete()
    {
        if ( doorRef.onRange == true && keyRef.hasKey == true)
        {
            //Debug.Log("Se ha completado");
            doorRef.OpenDoor();
        }
    }
}
