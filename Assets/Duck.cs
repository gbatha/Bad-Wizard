using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour {

	Vector3 moveVector;
	Rigidbody myBod;
	float t = 0;
	float endTime = 1;
	AudioSource audioSource;
	
	public GameObject player;
	public float turnOffset = 0f;
	
	enum States{
		walk,
		quack,
		stand
	}
	States myState = States.walk;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player Capsule");
		myBod = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float tplus = Time.deltaTime;
		t+= tplus;
		
		if (Mathf.Floor (Random.value * 100)==0)
		{
			audioSource.spatialBlend = 1.0f;
			audioSource.Play();
		
		}
		switch(myState)
		{
			case States.walk:
				if (t<=tplus)
				{
					endTime = 1 + Random.value*3;
					moveVector = new Vector3(-1+(Random.value*2),0,-1+(Random.value*2));
					if (Vector3.Distance(transform.position,player.transform.position)>8)//go near player if too far
					{
						moveVector = (player.transform.position - transform.position).normalized;
					}
					
				}
				//look where you're walking
				float rotationTarget = Vector3.Angle(transform.forward, moveVector) + turnOffset;
				float rotationAngle = Mathf.Lerp(transform.localEulerAngles.y, rotationTarget, Time.deltaTime * 5f);
				Vector3 rot = transform.localEulerAngles;
				rot.y = rotationAngle;
				transform.localEulerAngles = rot;

				myBod.AddForce(moveVector*0.8f,ForceMode.VelocityChange);
				if (t>endTime)
					ChangeState(States.stand);
				break;
				
			case States.stand:
				if (t<tplus)
				{
					endTime = 1 + Random.value*6;
				}
				if (t>1)
					ChangeState(States.walk);
				break;
				
		}
		
	}
	
	void ChangeState(States newState){
		myState = newState;
		t=0;
	}
}
