using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LableTrackObject : MonoBehaviour {
	
	public GameObject Obj;
	public Camera mCamera;
	private RectTransform rt;
	Vector3 objPos;
	public Vector3 offset;
	void Start ()
	{
		rt = GetComponent<RectTransform>();
	}

	void LateUpdate ()
	{
		if (Obj != null)
		{	
			objPos = Obj.transform.position;
			objPos = objPos + offset;


			Vector2 pos = RectTransformUtility.WorldToScreenPoint(mCamera, Obj.transform.position);
			rt.position = pos;
		}
	}
}