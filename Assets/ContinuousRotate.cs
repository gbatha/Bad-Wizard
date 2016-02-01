using UnityEngine;
using System.Collections;

public class ContinuousRotate : MonoBehaviour {
	public Vector3 speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rot = transform.localEulerAngles;
		rot.x += speed.x * Time.deltaTime;
		rot.y += speed.y * Time.deltaTime;
		rot.z += speed.z * Time.deltaTime;
		transform.localEulerAngles = rot;
	}
}
