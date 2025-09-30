using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_ejemplo_tres : MonoBehaviour
{
    public Text miTexto;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                miTexto.text = "bajas el dedo";
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                miTexto.text = "Mantienes el dedo";
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                miTexto.text = "Mantienes el dedo y lo mueves";
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                miTexto.text = "Levantas el dedo";
            }
            if (Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                miTexto.text = "El dedo se salió de la pantalla o algun problema ha sucedido";
            }
        }
        
    }
}
