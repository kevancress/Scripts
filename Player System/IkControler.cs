using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IkControler : MonoBehaviour {
    Animator animator;
    public bool ikActive = false;
    public GameObject rightRaycast;
    public GameObject leftRaycast;
    RaycastHit rightHit;
    RaycastHit leftHit;
    public Vector3 offset;

    void Start () {
        animator = GetComponent<Animator>();
        
		
	}
	
    void OnAnimatorIK()
    {

        Physics.Raycast(leftRaycast.transform.position, Vector3.down, out leftHit, 100);

        Physics.Raycast(rightRaycast.transform.position, Vector3.down, out rightHit, 100);
        if (animator)
        {
            if (ikActive)
            {
                if (rightHit.point != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, (rightHit.point+offset));
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(Tangent(rightHit.normal)));

                }

                if (leftHit.point != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, (leftHit.point+offset));
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Tangent(leftHit.normal)));
                }
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                }


            }
        }
    }

    public Vector3 Tangent(Vector3 normal)
    {
        Vector3 tangent = Vector3.Cross(normal, gameObject.transform.forward);

        if (tangent.magnitude == 0)
        {
            tangent = Vector3.Cross(normal, Vector3.up);
        }

        Vector3 tangentRot = new Vector3(-tangent.z, tangent.y, tangent.x);
        
        return tangentRot;
    }
}
