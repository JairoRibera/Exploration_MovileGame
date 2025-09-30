using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tocuh_4 : MonoBehaviour
{
    public Camera Micamara;
    public Text miTexto;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Ray ray = Micamara.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                miTexto.text = "he chocado con un " + (hit.transform.gameObject.name);
            }
        }
    }
}
