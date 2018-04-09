using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionPlayerMovement : MonoBehaviour {

	float turnSmoothVelocity;
	public float turnSmoothTime = .1f;
	float newAnimationSpeed;
	float animationSpeed;
	float currentVelocity;
	public float animationSmooth = 6f;


	
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
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}


		bool running = Input.GetKey (KeyCode.LeftShift);

		if (running) {
			newAnimationSpeed = 1;
		} 
		else {
			newAnimationSpeed = 0;
		}


		animationSpeed = Mathf.Lerp (animationSpeed, newAnimationSpeed, Time.deltaTime * animationSmooth);
		Vector3 charVelocity = controller.velocity;

		if (Input.GetButtonDown ("Jump") && running) {
			playerAnimator.SetBool ("runJump", true);
			playerAnimator.SetBool ("jump", false);
		} 
		else if (Input.GetButtonDown ("Jump") && (inputDir.magnitude>0)) {
			playerAnimator.SetBool ("runJump", false);
			playerAnimator.SetBool ("jump", true);
		}
		else {
		playerAnimator.SetBool ("runJump", false);
		playerAnimator.SetBool ("jump", false);
		}

		playerAnimator.SetFloat ("forward", inputDir.magnitude);
		playerAnimator.SetFloat ("speedPercent", animationSpeed);

	}
}
