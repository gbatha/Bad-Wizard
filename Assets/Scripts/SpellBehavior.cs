using UnityEngine;
using System.Collections;

public class SpellBehavior : MonoBehaviour {
	spellData spelldata;
	GameObject[] spellEffects;

	[SerializeField]
	SpellManager spellmanager;
	
	Vector3 moveVector;
	
	GameObject target;

	float spellSpeed = 15f;

	// Use this for initialization
	void Awake () {
		spellmanager = GameObject.Find ("SpellManager").GetComponent<SpellManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//if this isn't a projectile, move the spell itself towards the target
		if (spelldata.type != spellType.Projectile) {
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, Time.deltaTime * spellSpeed);
			Debug.DrawLine(transform.position,target.transform.position,Color.white,0.1f);
		}
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
		//if this is a projectile, fire it now!
		if (spelldata.type == spellType.Projectile) {
			fireProjectile();
		}
	}

	void doSpell(){}

	void fireProjectile(){
		//fire the object!
		if (spelldata.theObject != null) {
			GameObject projectile = Instantiate (spelldata.theObject);

			projectile.transform.position = transform.position;
			transform.parent = projectile.transform;
			projectile.GetComponent<Rigidbody> ().AddForce (Camera.main.transform.forward * spelldata.projectileForce);
			//play the sound if we have it
			if(spelldata.sound != null){
				//plays on awake
				Utils.AddAudio(gameObject, spelldata.sound, false, true, 1f);
			}
		} else {
			//fart out I guess?
		}
	}

	IEnumerator 
	
}
