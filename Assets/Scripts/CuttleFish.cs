using UnityEngine;
using System.Collections;

public class CuttleFish : MonoBehaviour {

	private Rigidbody myBod;
	public GameObject target;
	// Use this for initialization
	void Start () {
		myBod = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 moveVector = target.transform.position - transform.position;
		//moveVector += Vector3(Random.value*2,Random.value*2,Random.value*2);
		moveVector.Normalize();
		myBod.AddForce(moveVector,ForceMode.VelocityChange);
	}
}
