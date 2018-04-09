using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float walkSpeed = 2;
	public float runSpeed = 6;

	public float turnTime = 0.2f;
	float turnVelocity;
	float gravity = -9.8f;
	public float speedTime = 0.1f;
	float speedVelocity;
	float currentSpeed;
	float velocityY;


	Animator playerAnimator;
	Transform cameraT;
	CharacterController controller;
	// Use this for initialization
	void Start () {
		playerAnimator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {

		Vector2 axisinput = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		Vector2 inputDir = axisinput.normalized;

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnVelocity, turnTime);
		}

		bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed)* inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedVelocity, speedTime);

		velocityY += Time.deltaTime * gravity;

		Vector3 charVelocity = transform.forward * currentSpeed + Vector3.up * velocityY;
		controller.Move (charVelocity * Time.deltaTime);


		if (controller.isGrounded) {
			velocityY = 0;
		}
			
		float animationSpeed = controller.velocity.magnitude/3;


			
			
		
		playerAnimator.SetFloat ("speedPercent", animationSpeed);
	}
}
