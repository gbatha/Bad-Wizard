using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour {

	Vector3 moveVector;
	Rigidbody myBod;
	float t = 0;
	float endTime = 1;
	
	public GameObject player;
	
	enum States{
		walk,
		quack,
		stand
	}
	States myState = States.walk;

	// Use this for initialization
	void Start () {
		myBod = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float tplus = Time.deltaTime;
		t+= tplus;
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
