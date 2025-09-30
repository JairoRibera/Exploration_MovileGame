using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Touch_ejemplo_dos : MonoBehaviour
{

    public Text miTexto;
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            //GetTouch, simboliza el primer dedo, en su interior se encuentra que es un vector 2
            miTexto.text = "posicion dedo 1 " + Input.GetTouch(0).position;
        }
        else
        {
            miTexto.text = "no hay dedos";
        }
    }
}
