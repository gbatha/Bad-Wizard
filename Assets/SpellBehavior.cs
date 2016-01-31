using UnityEngine;
using System.Collections;

public class SpellBehavior : MonoBehaviour {
	spellData spelldata;
	GameObject[] spellEffects;

	[SerializeField]
	SpellManager spellmanager;
	
	Vector3 moveVector;
	
	GameObject target;

	// Use this for initialization
	void Awake () {
		spellmanager = GameObject.Find ("SpellManager").GetComponent<SpellManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position,target.transform.position,Color.white,0.1f);
		moveVector += (target.transform.position - transform.position).normalized/5f;
		transform.position += moveVector; //new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * 15f);
	}

	public void Init(string castCode, GameObject[] effectsIn, GameObject targetIn = null){
		//get our data from the spell manager by the cast code so we can figure out what kind of spell this is
		spelldata = spellmanager.getSpell(castCode);
		//put the effects on this object
		spellEffects = effectsIn;
		for (int i = 0; i < spellEffects.Length; i++) {
			spellEffects[i].transform.parent = transform;
		}
		target = targetIn;

	}

	public void Init(GameObject[] effectsIn){
		//put the effects on this object
		spellEffects = effectsIn;
		for (int i = 0; i < spellEffects.Length; i++) {
			spellEffects[i].transform.parent = transform;
		}
		
	}
}
