using UnityEngine;
using System.Collections;

public class SpellBehavior : MonoBehaviour {
	spellData spelldata;
	GameObject[] spellEffects;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * 15f);
	}

	public void Init(spellData dataIn, GameObject[] effectsIn){
		//get our data so we can figure out what kind of spell this is
		spelldata = dataIn;
		//put the effects on this object
		spellEffects = effectsIn;
		for (int i = 0; i < spellEffects.Length; i++) {
			spellEffects[i].transform.parent = transform;
		}

	}

	public void Init(GameObject[] effectsIn){
		//put the effects on this object
		spellEffects = effectsIn;
		for (int i = 0; i < spellEffects.Length; i++) {
			spellEffects[i].transform.parent = transform;
		}
		
	}
}
