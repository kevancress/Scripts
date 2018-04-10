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

	void Start () {
		playerAnimator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();
	}
	
	void Update () {
        // get input info
		Vector2 axisinput = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		Vector2 inputDir = axisinput.normalized;


        //set character rotation
		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

        //check if the character should be running
		bool running = Input.GetButton("Run");

		if (running) {
			newAnimationSpeed = 1;
		} 
		else {
			newAnimationSpeed = 0;
		}

        // set animation speed
        animationSpeed = Mathf.Lerp (animationSpeed, newAnimationSpeed, Time.deltaTime * animationSmooth);
		Vector3 charVelocity = controller.velocity;

        //set animatior values 
        playerAnimator.SetFloat("forward", inputDir.magnitude);
        playerAnimator.SetFloat("speedPercent", animationSpeed);

        /*  old jump code
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
        */


        playerAnimator.SetFloat ("forward", inputDir.magnitude);
		playerAnimator.SetFloat ("speedPercent", animationSpeed);
        
	}
}
