using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Rookie Attack", menuName = "Abilities/Rookie Attack")]
public class RookieAttack : Ability {
    public float damageBase;
    public float range;
    public GameObject raycastMarker;
    public GameObject LighningBolt;
    GameObject rayMarker = null;
    public Vector3 offset;
    public float DimLight = .1f;
    public float DimTime = 2f;


    public override void OnUseBasic()
    {
        base.OnUseBasic();
    }

    public override void OnUseHold()
    {
        base.OnUseHold();
        RaycastHit hit;
        CameraManager cameraManager = CameraManager.instance;
        Camera cam = cameraManager.mainCamera;

        DaylightManager daylightManager = DaylightManager.instance;
        
        
        

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            
            Quaternion markerRot = Quaternion.LookRotation(raycastMarker.transform.right,hit.normal);

            if (rayMarker == null)
            {
                rayMarker = Instantiate(raycastMarker, hit.point, markerRot);
                daylightManager.DimSun();
            }
            else
            {
                rayMarker.transform.position = hit.point;
                rayMarker.transform.rotation = markerRot;
            }
        }


        

        
    }

    public override void EndHold()
    {
        base.EndHold();
        RaycastHit hit;
        CameraManager cameraManager = CameraManager.instance;
        Camera cam = cameraManager.mainCamera;

        DaylightManager daylightManager = DaylightManager.instance;
        Destroy(rayMarker);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            GameObject bolt = Instantiate(LighningBolt, hit.point, Quaternion.Euler(0f,0f,-90f));
            Destroy(bolt, .5f);
            daylightManager.ResetIntensity();
        }
        else
        {
            Debug.Log("no Hit");
        }
    }
}
