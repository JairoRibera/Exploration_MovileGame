using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    private JoystickRotator joystickRotator;
    private IsNear isNearRef;
    private CameraOrbit CameraOrbit;
    public Transform camerapos;
    public Transform posCam;
    public float speed;
    public GameObject TextA;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        joystickRotator = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickRotator>();
        isNearRef = GameObject.FindGameObjectWithTag("Palanca").GetComponent<IsNear>();
        CameraOrbit = Camera.main.GetComponent<CameraOrbit>();

    }

    // Update is called once per frame
    void Update()
    {
        CambioDeCamara();
    }
    public void CambioDeCamara()
    {
        if(isNearRef.isNear == true && Input.GetKeyDown(KeyCode.JoystickButton0) || isNearRef.isNear == true && Input.GetKeyDown(KeyCode.E))
        {
            joystickRotator.GetComponent<JoystickRotator>().enabled = true;
            CameraOrbit.GetComponent<CameraOrbit>().enabled = false;
            playerController.canMove = false;
            playerController._rb.velocity = Vector3.zero;
            camerapos.position = posCam.position;
            camerapos.transform.LookAt(joystickRotator.target.position);

        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Q))
        {
            isNearRef.isNear = false;
            joystickRotator.GetComponent<JoystickRotator>().enabled = false;
            CameraOrbit.GetComponent<CameraOrbit>().enabled = true;
            playerController.canMove = true;
            camerapos.position = CameraOrbit.target.position;
        }

    }
    public void DeActivateText()
    {
        if(isNearRef.isNear == true)
        {
            TextA.SetActive(false);
        }
    }
}
