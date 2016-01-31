using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnalogKeyboard : MonoBehaviour
{
	private Vector3 dir = new Vector3();
	private Dictionary<KeyCode, Vector2> keys = new Dictionary<KeyCode, Vector2>();


	[SerializeField]
	GameObject[] spellEffects;

	[SerializeField]
	GameObject spellPrefab;

	ParticleSystem wandParticles;
	public Transform fireFrom;

	//every time we trigger a spell it's added to this string. once this string is 4 we cast the spell
	string castingSpell = "";
	int spellLength = 3; //number of spells required for it to cast
	GameObject[] castingEffects; //activated effects just for the current spell
	int castingIndex = 0;

	//spell cooldown
	float cooldownTime = 0.3f;
	float lastCastTime = 0f;

	void Start ()
	{
		wandParticles = GetComponent<ParticleSystem> ();
		Utils.PlayParticleSystem (wandParticles, false);
		castingEffects = new GameObject[spellLength];
		PopulateQwerty();
		//dir = new Vector3(10, 10, 10);
	}
	
	public Vector3 GetDir()
	{
		return dir;
	}
	
	void PopulateQwerty()
	{
		// Top row
		keys.Add(KeyCode.Q, new Vector2(-.75f,.25f));
		keys.Add(KeyCode.W, new Vector2(-.5f,.5f));
		keys.Add(KeyCode.E, new Vector2(-.5f,.75f));
		keys.Add(KeyCode.R, new Vector2(-.25f,1f));
		keys.Add(KeyCode.T, new Vector2(0f,1f));
		keys.Add(KeyCode.Y, new Vector2(0f,1f));
		keys.Add(KeyCode.U, new Vector2(.25f,.75f));
		keys.Add(KeyCode.I, new Vector2(.5f,.5f));
		keys.Add(KeyCode.O, new Vector2(.5f,.25f));
		keys.Add(KeyCode.P, new Vector2(.75f,.2f));
		keys.Add(KeyCode.LeftBracket, new Vector2(.75f,.2f));
		keys.Add(KeyCode.RightBracket, new Vector2(.75f,.1f));
		
		// middle row
		keys.Add(KeyCode.A, new Vector2(-1f,0f));
		keys.Add(KeyCode.S, new Vector2(-.75f,0f));
		keys.Add(KeyCode.D, new Vector2(-.5f,0f));
		keys.Add(KeyCode.F, new Vector2(-.25f,0f));
		keys.Add(KeyCode.G, new Vector2(0f,0f));
		keys.Add(KeyCode.H, new Vector2(.25f,0f));
		keys.Add(KeyCode.J, new Vector2(.5f,0f));
		keys.Add(KeyCode.K, new Vector2(.75f,0f));
		keys.Add(KeyCode.L, new Vector2(1f,0f));
		keys.Add(KeyCode.Colon, new Vector2(1f,0f));
		
		// bottom row
		keys.Add(KeyCode.Z, new Vector2(-.75f,-.25f));
		keys.Add(KeyCode.X, new Vector2(-.5f,-.5f));
		keys.Add(KeyCode.C, new Vector2(-.5f,-.75f));
		keys.Add(KeyCode.V, new Vector2(-.25f,-1f));
		keys.Add(KeyCode.B, new Vector2(0f,-1f));
		keys.Add(KeyCode.N, new Vector2(.25f,-1f));
		keys.Add(KeyCode.M, new Vector2(.5f,-.75f));
		keys.Add(KeyCode.Comma, new Vector2(.5f,-.5f));
		keys.Add(KeyCode.Period, new Vector2(.75f,-.25f));
		keys.Add(KeyCode.Slash, new Vector2(.75f,0f));
	
		//keys.Add(KeyCode.F, new Vector2(-.25f,0f));
	}
	
	void Update ()
	{
		
//		dir *= .9f;
		
		Vector3 newDir = Vector3.zero;
		
		// go thru the keys
		bool keysDown = false;
		foreach(KeyValuePair<KeyCode, Vector2> pair in keys)
		{
			if( Input.GetKey(pair.Key) )
			{
				newDir.x += pair.Value.x;
				newDir.y += pair.Value.y;
				keysDown = true;
			}
		}
		

		if (keysDown) {
			//play wand particles if we're casting
			Utils.PlayParticleSystem (wandParticles, true);
			dir = newDir;
			transform.localPosition = new Vector3 (dir.x, dir.y, 0f);
			float ang = Vector3.Angle (new Vector3 (0f, 1f, 0f), dir);
			float dist = Vector3.Distance (Vector3.zero, dir);
//			gameObject.GetComponent<Renderer>().material.color 
//			Debug.Log(ang+"   "+dist);

			//CONDITIONS
			if (dist < 0.5f) {
				Debug.Log(0);
				triggerCast ("0");
			} else if (ang < 60f && dir.x >= 0f) {
				Debug.Log(1);
				triggerCast ("1");
			}else if (ang < 120f && dir.x >= 0f) {
				Debug.Log(2);
				triggerCast ("2");
			}else if (ang < 180f && dir.x > 0f) {
				Debug.Log(3);
				triggerCast ("3");
			}else if (ang <= 180f && ang > 120f && dir.x <= 0f) {
				Debug.Log(4);
				triggerCast ("4");
			}else if (ang <= 120f && ang > 60f && dir.x <= 0f) {
				Debug.Log(5);
				triggerCast ("5");
			}else{
				Debug.Log(6);
				triggerCast ("6");
			}
		} else {
			//only play wand particles if we're casting
			Utils.PlayParticleSystem (wandParticles, false);
		}

		
		//Debug.Log(dir);
	}

	void triggerCast(string strIn){
		//make sure we're past the cooldown
		if (Time.time - lastCastTime > cooldownTime) {
			//make sure we're not appending this spell twice
			if (castingSpell.IndexOf (strIn) == -1) {
				castingSpell += strIn;
				//spawn the spell effect
				GameObject newEffect = null;
				//Spell view shrink factor since I moved it closer to the camera
				float shrinkRadius = 0.25f;
				switch (strIn) {
				case "0":
					newEffect = spellEffects [0].Spawn (transform.parent, new Vector3 (0f, 0f, 0.25f));
					break;
				case "1":
					newEffect = spellEffects [1].Spawn (transform.parent, new Vector3 (0f, 1f*shrinkRadius, 0.25f));
					break;
				case "2":
					newEffect = spellEffects [2].Spawn (transform.parent, new Vector3 (0.86f*shrinkRadius, 0.5f*shrinkRadius, 0.25f));
					break;
				case "3":
					newEffect = spellEffects [3].Spawn (transform.parent, new Vector3 (0.86f*shrinkRadius, -0.5f*shrinkRadius, 0.25f));
					break;
				case "4":
					newEffect = spellEffects [4].Spawn (transform.parent, new Vector3 (0f, -1f*shrinkRadius, 0.25f));
					break;
				case "5":
					newEffect = spellEffects [5].Spawn (transform.parent, new Vector3 (-0.86f*shrinkRadius, -.5f*shrinkRadius, 0.25f));
					break;
				case "6":
					newEffect = spellEffects [6].Spawn (transform.parent, new Vector3 (-0.86f*shrinkRadius, 0.5f*shrinkRadius, 0.25f));
					break;
				}
				//if we successfully triggered a spell, add it to our cast
				if (newEffect != null) {
					castingEffects [castingIndex] = newEffect;
					castingIndex ++;
				}

				//if this is 4 spells, trigger the magic!
				if (castingSpell.Length == spellLength) {
					//DO SOMETHING!!!!

					string orderedCast = new string (castingSpell.OrderBy(c => c).ToArray());
					Debug.Log (castingSpell+" -> "+orderedCast);
					lastCastTime = Time.time;
					GameObject newSpell = spellPrefab.Spawn(fireFrom.position);
					newSpell.GetComponent<SpellBehavior> ().Init (orderedCast, castingEffects, GetComponent<TargetFinder>().currentTarget);

					//reset our spell
					resetSpell ();
				}

			}
		}
	}

	void resetSpell(){
		castingSpell = "";
		Array.Clear (castingEffects, 0, castingEffects.Length);
		castingIndex = 0;
	}
	
	void OnDrawGizmos()
	{	
		Gizmos.DrawRay(transform.position, dir);
	}
}

