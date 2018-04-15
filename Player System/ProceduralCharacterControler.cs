using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCharacterControler : MonoBehaviour {
    public float turnSmoothTime = .1f;
    public float moveSmoothTime = .1f;
    public float baseMoveSpeed=6f;
    public float gravity = 9.8f;
    public float jumpHeight = 100f;
    public Transform centerOfMass;
    public float animationSpeed;
    public float phaseStart;
    public float phaseEnd;
    public float delayTime=1f;
    public float runMultiplier;
    public AnimationCurve jumpBlend;


    float leftright;
    float forwardback;
    float moveSpeed;
    float upDown;
    float crouchTimer=0f;
    bool isJumping = false;
    float currentIsJump;
    float targetIsJump;
    float currentJumpState;
    float targetJumpState;
    Vector3 cameraForward;
    Vector3 forwardMoveVector;
    Vector3 horizontalMoveVector;
    Vector3 targetMoveVector;
    Vector3 currentMoveVector;
    Vector3 currentMoveVelocity;
    Vector3 currentMoveVelocityVertical;
    Vector3 currentMoveVelocityHorizontal;
    Vector3 acceleration;
    Quaternion targetRotation;
    Quaternion currentRotation;
    Vector3 vec3CurrentRotation;
    Vector3 lastMoveVelocity;
    Vector3 rotationMoveVelocity;

    float trackDistance;
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
        ikController = GetComponent<IkControler>();
    }

    // Update is called once per frame
    void Update() {
        
        lastMoveVelocity = currentMoveVelocity;
        leftright = Input.GetAxis("Horizontal");
        forwardback = Input.GetAxis("Vertical");
        cameraForward = cameraT.forward;

        //get forward input vector
        forwardMoveVector = new Vector3(cameraForward.x * forwardback, 0f, cameraForward.z * forwardback);
        forwardMoveVector = forwardMoveVector.normalized;

        //get horizontal input vector
        horizontalMoveVector = new Vector3(-cameraForward.z * -leftright, 0f, cameraForward.x * -leftright);
        horizontalMoveVector = horizontalMoveVector.normalized;

        //combine & Multiply by move speed
        targetMoveVector = horizontalMoveVector + forwardMoveVector;
        targetMoveVector = targetMoveVector.normalized;
        checkRun();


        targetMoveVector *= moveSpeed;


        currentMoveVector = Vector3.Lerp(currentMoveVector, targetMoveVector, moveSmoothTime*Time.deltaTime);
        CrouchJump();
        // jump or add gravity



        

        //move character
        if(Input.GetButtonUp("Horizontal")|| Input.GetButtonUp("Vertical"))
        {
            InputDelay();
        }
        else
        {
            controller.Move((currentMoveVector * Time.deltaTime));
        }

        currentMoveVelocity = controller.velocity;
        currentMoveVelocityHorizontal = new Vector3(currentMoveVelocity.x, 0, currentMoveVelocity.z);
        currentMoveVelocityVertical = new Vector3(0, currentMoveVelocity.y, 0);
        

        if (Input.GetButton("Strafe"))
        {
            GetStrafeFacingDirection();
            if (Input.GetButtonUp("Strafe"))
            {
                InputDelay();
            }
        }
        else
        {
            if (currentMoveVelocityHorizontal.magnitude > .1)
            {
                GetFacingDirection();
            }

        }

        //calculate current velocity store previous value
        

        UpdateAnimator();

    }

    private void CrouchJump()
    {
        if (controller.isGrounded)
        {
            if (!isJumping)
            {
                if (Input.GetButton("CrouchJump"))
                {
                    crouchTimer += Time.deltaTime;
                    if (currentMoveVelocity.magnitude > .1)
                    {  
                        targetIsJump = .25f;
                    }

                    targetIsJump = 1;
                    w
                }
                if (Input.GetButtonUp("CrouchJump") && crouchTimer < 1f)
                {
                    upDown = jumpHeight * (crouchTimer + 1);
                    targetIsJump = 1;
                    targetJumpState = 1;
                    isJumping = true;
                    crouchTimer = 0;
                }
                else if (Input.GetButtonUp("CrouchJump") && crouchTimer > 1f)
                {
                    targetIsJump = 0;
                    targetJumpState = 0;
                    crouchTimer = 0;
                }
            }

            else if (isJumping)
            {
               
                Debug.Log("you landed");
                targetJumpState = 0;
                playerAnimator.SetFloat("IsJump", 1f);
                isJumping = false;
            }

            if (!Input.GetButton("CrouchJump"))
            {
                targetIsJump = 0;
                targetJumpState = 0;
            }


        }
       
        if (!controller.isGrounded)
        {
            if (isJumping)
            {
                targetJumpState = 1;
                targetIsJump = 1;
            }
            if (!isJumping)
            {
               // Debug.Log("you are falling");
            }
            
        }

        currentIsJump = playerAnimator.GetFloat("IsJump");
        currentIsJump= Mathf.Lerp(currentIsJump, targetIsJump, 10 * Time.deltaTime);
        playerAnimator.SetFloat("IsJump",currentIsJump);


        currentJumpState = playerAnimator.GetFloat("JumpState");
        currentJumpState = Mathf.Lerp(currentJumpState, targetJumpState, 10 * Time.deltaTime);
        playerAnimator.SetFloat("JumpState", currentJumpState);

        upDown -= gravity * Time.deltaTime;
        currentMoveVector.y = upDown;
    }

    private void checkRun()
    {
        if (Input.GetButton("Run"))
        {
            moveSpeed = baseMoveSpeed * runMultiplier;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
    }

    public void GetFacingDirection()
    {
        //calculate current facing direction based on velocity
        if (currentMoveVelocityHorizontal.magnitude > .1f)
        {
            rotationMoveVelocity = currentMoveVelocityHorizontal;
        }
        targetRotation = Quaternion.LookRotation(new Vector3(rotationMoveVelocity.x, 0f, rotationMoveVelocity.z));
        currentRotation = controller.transform.rotation;
        controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, turnSmoothTime * Time.deltaTime);
    }

    public void GetStrafeFacingDirection()
    {
        //calculate current facing direction based on velocity
        if (currentMoveVelocityHorizontal.magnitude > .1f)
        {
            rotationMoveVelocity = currentMoveVelocityHorizontal;
        }
        targetRotation = Quaternion.LookRotation(new Vector3(cameraForward.x, 0f, cameraForward.z));
        currentRotation = controller.transform.rotation;
        controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, turnSmoothTime * Time.deltaTime);
    }

    public void ToggleIK()
    {
        if (currentMoveVelocity.magnitude > .1f)
        {
            ikController.ikActive = false;
        }
        else
        {
            ikController.ikActive = false;
        }
    }

    public void UpdateAnimator()
    {
        playerAnimator.SetFloat("Speed", currentMoveVelocityHorizontal.magnitude);
        playerAnimator.SetFloat("Moving", currentMoveVelocity.magnitude);

    }

    IEnumerator InputDelay()
    {
        for (float i = 0; i < delayTime; i+=Time.deltaTime)
        {
            controller.Move((currentMoveVector * Time.deltaTime));
            yield return null;
        }
        yield break;

    }
}
