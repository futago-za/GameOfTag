using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Transform playerCam, centerPoint;
    //private GameObject playerCam, centerPoint;

    private float mouseX, mouseY;
    // Input.GetAxis("Mouse X")とInput.GetAxis("Mouse Y")にかけるべき?
    public float mouseSensitivity = 10.0f;

    void Start() {
        playerCam = GameObject.Find("PlayerCamera").transform;
        centerPoint = GameObject.Find("CenterPoint").transform;
        //playerCam = GameObject.Find("PlayerCamera");
        //centerPoint = GameObject.Find("CenterPoint");
    }

    void Update() {
        Cursor.lockState = CursorLockMode.Locked;

        playerCam.transform.localPosition = new Vector3(0, 0, -4);

        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, -60.0f, 60.0f);
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
       

        /*
        Ray ray = playerCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Vector3 direction = ray.direction.normalized * 30;
        playerCam.transform.LookAt(centerPoint.transform);
        centerPoint.transform.localRotation = Quaternion.Euler(direction.y, direction.x, 0); 
        */
    }
}
