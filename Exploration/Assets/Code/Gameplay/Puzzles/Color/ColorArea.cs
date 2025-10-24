using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class ColorArea : MonoBehaviour
{
    public ColorType type;
    public bool isCorrect = false;
    //public Transform point;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //Acceder al componente coloritem del objeto que haya entrado en el area
        ColorItem _item = other.GetComponent<ColorItem>();
        //Si ha encontrado un scrupt Color Item
        if (_item != null && type == _item.colorType)
        {
            isCorrect = true;
            //_item.transform.position = point.transform.position;
            //_item.GetComponent<Rigidbody>().isKinematic = true; //Esta linea congela al objeto cuando entra al area
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Acceder al componente coloritem del objeto que haya entrado en el area
        ColorItem _item = other.GetComponent<ColorItem>();
        //Si ha encontrado un scrupt Color Item
        if (_item != null && type == _item.colorType)
        {
            isCorrect = false;
        }
    }
}
