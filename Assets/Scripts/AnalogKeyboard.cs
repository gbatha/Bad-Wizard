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
	public spell[] spells;


	//every time we trigger a spell it's added to this string. once this string is 4 we cast the spell
	string castingSpell = "";

	void Start ()
	{

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
		
//		// -x
//		if( Input.GetKeyDown(KeyCode.F) )
//			newDir.x -= 1f;
//		if( Input.GetKeyDown(KeyCode.D) )
//			newDir.x -= 1f;
//		if( Input.GetKeyDown(KeyCode.S) )
//			newDir.x -= 1f;
//		if( Input.GetKeyDown(KeyCode.A) )
//			newDir.x -= 1f;
//			
//		// +x
//		if( Input.GetKeyDown(KeyCode.H) )
//			newDir.x += 1f;
//		if( Input.GetKeyDown(KeyCode.J) )
//			newDir.x += 1f;
//		if( Input.GetKeyDown(KeyCode.K) )
//			newDir.x += 1f;
//		if( Input.GetKeyDown(KeyCode.L) )
//			newDir.x += 1f;
//		
//		// +z
//		if( Input.GetKeyDown(KeyCode.T) )
//			newDir.z += 1f;
//		if( Input.GetKeyDown(KeyCode.Y) )
//			newDir.z += 1f;
//			
//		// -z
//		if( Input.GetKeyDown(KeyCode.V) )
//			newDir.z -= 1f;
//		if( Input.GetKeyDown(KeyCode.B) )
//			newDir.z -= 1f;
//			
//		// -x, +z
//		if( Input.GetKeyDown(KeyCode.R) )
//		{
//			newDir.x -= 1f; newDir.z += 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.E) )
//		{
//			newDir.x -= 1f; newDir.z += 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.W) )
//		{
//			newDir.x -= 1f; newDir.z += 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.Q) )
//		{
//			newDir.x -= 1f; newDir.z += 1f;
//		}
//		
//		// -x, -z
//		if( Input.GetKeyDown(KeyCode.V) )
//		{
//			newDir.x -= 1f; newDir.z -= 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.C) )
//		{
//			newDir.x -= 1f; newDir.z -= 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.X) )
//		{
//			newDir.x -= 1f; newDir.z -= 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.Z) )
//		{
//			newDir.x -= 1f; newDir.z -= 1f;
//		}
//		
//		// +x, -z
//		if( Input.GetKeyDown(KeyCode.N) )
//		{
//			newDir.x += 1f; newDir.z -= 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.M) )
//		{
//			newDir.x += 1f; newDir.z -= 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.Comma) )
//		{
//			newDir.x += 1f; newDir.z -= 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.Period) )
//		{
//			newDir.x += 1f; newDir.z -= 1f;
//		}
//		
//		// +x, +z
//		if( Input.GetKeyDown(KeyCode.U) )
//		{
//			newDir.x += 1f; newDir.z += 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.I) )
//		{
//			newDir.x += 1f; newDir.z += 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.O) )
//		{
//			newDir.x += 1f; newDir.z += 1f;
//		}
//		if( Input.GetKeyDown(KeyCode.P) )
//		{
//			newDir.x += 1f; newDir.z += 1f;
//		}

		if (keysDown) {
			dir = newDir;
			transform.localPosition = new Vector3 (dir.x, dir.y, 0f);
			float ang = Vector3.Angle(new Vector3(0f, 1f, 0f), dir);
			float dist = Vector3.Distance(Vector3.zero, dir);
//			gameObject.GetComponent<Renderer>().material.color 
//			Debug.Log(ang+"   "+dist);

			//CONDITIONS
			if(dist < 0.5f){
				//SPELL 0
				gameObject.GetComponent<Renderer>().material.color = Color.red;
				spellEffects[0].SetActive(true);
				triggerCast("0");
			}else if(ang < 90f && dir.x >= 0f){
				//SPELL 1
				gameObject.GetComponent<Renderer>().material.color = Color.blue;
				spellEffects[1].SetActive(true);
				triggerCast("1");
			}else if(ang <= 90f && dir.x < 0f){
				//SPELL 4
				gameObject.GetComponent<Renderer>().material.color = Color.green;
				spellEffects[4].SetActive(true);
				triggerCast("4");
			}else if(ang <= 180f && dir.x >= 0f){
				//SPELL 2
				gameObject.GetComponent<Renderer>().material.color = Color.yellow;
				spellEffects[2].SetActive(true);
				triggerCast("2");
			}else{
				//SPELL 3
				gameObject.GetComponent<Renderer>().material.color = Color.magenta;
				spellEffects[3].SetActive(true);
				triggerCast("3");
			}
		}

		
		//Debug.Log(dir);
	}

	void triggerCast(string strIn){
		//make sure we're not appending this spell twice
		if (castingSpell.LastIndexOf (strIn) == -1 || castingSpell.LastIndexOf (strIn) != castingSpell.Length - 1) {
			castingSpell += strIn;
			//if this is 4 spells, trigger the magic!
			if(castingSpell.Length == 4){
				//DO SOMETHING!!!!
				Debug.Log(castingSpell);

				//reset our spell
				resetSpell();
			}

		}
	}

	void resetSpell(){
		for(int i = 0; i < spellEffects.Length; i++)
			spellEffects[i].SetActive(false);
		castingSpell = "";
	}
	
	void OnDrawGizmos()
	{	
		Gizmos.DrawRay(transform.position, dir);
	}
}

[System.Serializable]
public struct spell {
	public string spellcombo;
	public spellType type;
	public GameObject turnsInto;
	public GameObject effect;
	public AudioClip sound;
}

public enum spellType { TurnInto, Effect };