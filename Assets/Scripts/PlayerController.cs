using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Transform player, centerPoint;

    public float mouseYPosition = 1.0f;
    private float moveFB, moveLR;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 5.0f;

	void Start () {
        player = GameObject.Find("Player").transform;
        centerPoint = GameObject.Find("CenterPoint").transform;
	}
	
	void Update () {
        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = player.rotation * movement;

        if(movement.magnitude > 0.01f) {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            player.rotation = Quaternion.Slerp(player.rotation, turnAngle, Time.deltaTime * rotationSpeed);
        }

        player.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        centerPoint.position = new Vector3(player.position.x, player.position.y + mouseYPosition, player.position.z);
	}
}
