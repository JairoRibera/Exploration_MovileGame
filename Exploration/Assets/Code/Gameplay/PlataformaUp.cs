using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaUp : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public int currentPoint;
    public Transform platformPos;

    // Update is called once per frame
    void Update()
    {
        platformPos.position = Vector3.MoveTowards(platformPos.position, points[currentPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(platformPos.position, points[currentPoint].position) < 0.01f)
        {
            currentPoint++;
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
