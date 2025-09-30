using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;

public class TouchDrag : MonoBehaviour
{
    public Camera cam;
    private Transform Objectotarget;
    public float Speed = 5;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10))
                {
                    Objectotarget = hit.transform;
                }
            }
            if (touch.phase == TouchPhase.Moved && Objectotarget != null)
            {
                Vector3 touchPos = touch.position;
                touchPos.z = cam.WorldToScreenPoint(Objectotarget.position).z;                
                Vector3 targetPos = cam.ScreenToWorldPoint(touchPos);
                Objectotarget.position = Vector3.Lerp(Objectotarget.position, targetPos, Speed * Time.deltaTime);
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Objectotarget = null;
            }
        }
        
    }
}
