using UnityEngine;
using System.Collections;
 
public class SpellBehavior : MonoBehaviour {
	spellData spelldata;
	GameObject[] spellEffects;
	Vector3 moveVector = Vector3.zero;
	GameObject target;

	// Use this for initialization
	void Start () {
	
	}  
	
	// Update is called once per frame
	void Update () {
		moveVector += (target.transform.position - transform.position).normalized;
		transform.position += moveVector;
	}

	public void Init(spellData dataIn, GameObject[] effectsIn, GameObject inTarget){
		//get our data so we can figure out what kind of spell this is
		spelldata = dataIn;
		//put the effects on this object
		spellEffects = effectsIn;
		for (int i = 0; i < spellEffects.Length; i++) {
			spellEffects[i].transform.parent = transform;
		}
		target = inTarget;
	}

	public void Init(GameObject[] effectsIn){
		//put the effects on this object
		spellEffects = effectsIn;
		for (int i = 0; i < spellEffects.Length; i++) {
			spellEffects[i].transform.parent = transform;
		}
		
	}
}
