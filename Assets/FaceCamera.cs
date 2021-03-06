﻿using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	private float t;
	private GameObject myCam;
	private AudioSource mySnd;
	private Light light;
	private ParticleSystem particles;
	// Use this for initialization
	void Start () {
		myCam = GameObject.Find("Main Camera");
		mySnd = GetComponent<AudioSource>();
		light = GetComponentInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.forward = transform.position - myCam.transform.position;
		t += 1;
		light.intensity /= 1.5f;
		if (t>60f)
		{
			particles.enableEmission = false;
			GetComponent<MeshRenderer>().enabled = false;
		}
		if (t>120f)
			Destroy(gameObject);
	}
}
