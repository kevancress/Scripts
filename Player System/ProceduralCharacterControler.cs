using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCharacterControler : MonoBehaviour {
    float turnSmoothVelocity;
    public float turnSmoothTime = .1f;
    public float moveSpeed=6f;
    public float gravity = 9.8f;
    public float jumpSpeed = 100f;
    public Transform centerOfMass;
    public float animationSpeed;
    public float phaseStart;
    public float phaseEnd;


    float leftright;
    float forwardback;
    float updown = 0;
    Vector3 cameraForward;
    Vector3 forwardMoveVector;
    Vector3 horizontalMoveVector;
    Vector3 moveVector;
    Vector3 currentMoveVelocity;
    Vector3 acceleration;
    Quaternion targetRotation;
    Quaternion currentRotation;
    Vector3 vec3CurrentRotation;
    Vector3 lastMoveVelocity;
    Vector3 rotationMoveVelocity;

    float trackDistance;
    Vector3 lastPosition=Vector3.zero;
    Vector3 currentPosition;
    float Distance;

    Animator playerAnimator;
    Transform cameraT;
    CharacterController controller;
    IkControler ikController;
    


    void Start () {
        playerAnimator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        Vector3 lastMoveVelocity = controller.transform.position;
        ikController = GetComponent<IkControler>();
    }

    // Update is called once per frame
    void Update() {

        leftright = Input.GetAxis("Horizontal");
        forwardback = Input.GetAxis("Vertical");
        updown = 0;
        cameraForward = cameraT.forward;

        //get forward input vector
        forwardMoveVector = new Vector3(cameraForward.x * forwardback, 0f, cameraForward.z * forwardback);
        forwardMoveVector = forwardMoveVector.normalized;

        //get horizontal input vector
        horizontalMoveVector = new Vector3(-cameraForward.z * -leftright, 0f, cameraForward.x * -leftright);
        horizontalMoveVector = horizontalMoveVector.normalized;

        //combine & Multiply by move speed
        moveVector = horizontalMoveVector + forwardMoveVector;
        moveVector = moveVector.normalized;
        moveVector *= moveSpeed;

        //add gravity
        moveVector.y -= gravity;

        //move character
        controller.Move((moveVector * Time.deltaTime));

        //calculate current velocity store previous value
        lastMoveVelocity = currentMoveVelocity;
        currentMoveVelocity = controller.velocity;

        //Calculate acceleration before updating last move velocity
        acceleration = currentMoveVelocity - lastMoveVelocity;

        //calculate current facing direction based on velocity
        if (currentMoveVelocity.magnitude > .1f)
        {
            rotationMoveVelocity = currentMoveVelocity;
        }
        targetRotation = Quaternion.LookRotation(new Vector3(rotationMoveVelocity.x, 0f, rotationMoveVelocity.z));
        currentRotation = controller.transform.rotation;
        controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, turnSmoothTime * Time.deltaTime);

        if (currentMoveVelocity.magnitude > .1f)
        {
            ikController.ikActive = false;
        }
        else
        {
            ikController.ikActive = true;
        }

        playerAnimator.SetFloat("Speed", currentMoveVelocity.magnitude);
        playerAnimator.speed = currentMoveVelocity.magnitude;



    }
}
