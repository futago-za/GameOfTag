using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Transform player, centerPoint;
    private CharacterController charaCon;
    private Vector3 moveDirection;

    private float moveFB, moveLR;
    public float mouseYPosition = 1.0f;
    public float moveSpeed = 2.0f;
    public float jumpPower = 10f;
    public float rotationSpeed = 5.0f;
    public float gravity = 10f;

	void Start () {
        player = GameObject.Find("Player").transform;
        centerPoint = GameObject.Find("CenterPoint").transform;
        charaCon = player.GetComponent<CharacterController>();
	}
	
	void Update () {
        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;

        if (charaCon.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                moveDirection.y = jumpPower;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;

        moveDirection = new Vector3(moveLR, moveDirection.y, moveFB);
        moveDirection = player.rotation * moveDirection;

        if (moveDirection.magnitude > 0.01f) {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            player.rotation = Quaternion.Slerp(player.rotation, turnAngle, Time.deltaTime * rotationSpeed);
        }

        charaCon.Move(moveDirection * Time.deltaTime);
        centerPoint.position = new Vector3(player.position.x, player.position.y + mouseYPosition, player.position.z);
	}
}
