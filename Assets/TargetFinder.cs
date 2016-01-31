using UnityEngine;
using System.Collections;

public class TargetFinder : MonoBehaviour {

	public Camera myCamera;
	public GameObject currentTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//find closest target to camera pointing forward
		float closestAngle = 180f;
		GameObject[] targets = GameObject.FindGameObjectsWithTag("targetable");
		for(int i = 0; i<targets.Length; i++)
		{
			Vector3 targetPos = targets[i].transform.position;
			Vector3 fromVector = myCamera.transform.forward;
			Vector3 toVector = (targetPos - myCamera.transform.position).normalized;
			float compareAngle = Vector3.Angle(fromVector,toVector);
			if (compareAngle<closestAngle)
			{
				closestAngle = compareAngle;
				currentTarget = targets[i];
			}
		}
		Debug.DrawLine(myCamera.transform.position,currentTarget.transform.position,Color.red,1);
	}
}
