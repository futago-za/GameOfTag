using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour {

    public Transform playerCam, character, centerPoint;

    private float mouseX, mouseY;
    public float mouseSensitivity = 10.0f;
    public float mouseYPosition = 1.0f;

    private float moveFB, moveLR;
    public float moveSpeed = 2.0f;

    private float zoom;
    public float zoomSpeed = 2.0f;

    public float zoomMin = -2.0f;
    public float zoomMax = -10.0f;

    public float rotationSpeed = 5.0f;

	void Start () {
        this.zoom = -3.0f;
	}

    void Update() {

        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (zoom > zoomMin)
            zoom = zoomMin;

        if (zoom < zoomMax)
            zoom = zoomMax;

        playerCam.transform.localPosition = new Vector3(0, 0, zoom);

        if (Input.GetMouseButton(1)) {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");
        }

        mouseY = Mathf.Clamp(mouseY, -60.0f, 60.0f);
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = character.rotation * movement;
        character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition, character.position.z);

        if(Input.GetAxis("Vertical") > 0 | Input.GetAxis("Vertical") < 0) {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);
        }
	}
}
