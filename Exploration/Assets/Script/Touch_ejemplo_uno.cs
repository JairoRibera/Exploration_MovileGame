using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_ejemplo_uno : MonoBehaviour
{
    public Text miTextodedos; //Esto dice cuantos dedos estan tocando la pantalla
    // Update is called once per frame
    void Update()
    {
        //Input.touchCount representa la cantidad de dedos que estan tocando la pantalla en ese momento con un int
        miTextodedos.text = "tengo " + Input.touchCount + " dedos en la pantalla";
    }
}
