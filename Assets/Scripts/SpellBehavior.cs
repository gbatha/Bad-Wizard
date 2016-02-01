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

	bool triggeredSpell = false;

	// Use this for initialization
	void Awake () {
		spellmanager = GameObject.Find ("SpellManager").GetComponent<SpellManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//if this isn't a projectile, move the spell itself towards the target
		if (spelldata.type != spellType.Projectile && !triggeredSpell && target != null) {
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, Time.deltaTime * spellSpeed);
			Debug.DrawLine(transform.position,target.transform.position,Color.white,0.1f);
			//once we've reached the target, do the spell!
			if(Vector3.Distance(transform.position, target.transform.position) < 0.1f && !triggeredSpell){
				triggeredSpell = true;
				doSpell();
			}
		}

		//if this spell doesn't have any data the particles kinda just fly off towards a random point lol...
		if (spelldata.theObject == null) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3(0f, 0f, 100f), Time.deltaTime * spellSpeed);
		}
	}

	public void Init(string castCode, GameObject[] effectsIn, GameObject targetIn, float speedCast, int numKeys){
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

	void doSpell(){
		if (spelldata.type == spellType.TurnInto) {
			//spawn an object of this new thing where the target is, then delete the target
			GameObject newThing = Instantiate (spelldata.theObject);
			newThing.transform.position = target.transform.position;
			transform.parent = newThing.transform;
			target.Recycle ();
		} else if (spelldata.type == spellType.Effect) {
			//spawn this object and parent it to the target
			GameObject newThing = Instantiate (spelldata.theObject);
			newThing.transform.parent = target.transform;
			newThing.transform.localPosition = new Vector3(0f,0f,0f);
		}
		//play a sound if we have one
		if(spelldata.sound != null){
			//plays on awake
			AudioSource sound = Utils.AddAudio(gameObject, spelldata.sound, false, false, 1f);
			sound.Play();
		}

		StartCoroutine (delayRecycle (2f));
	}

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
				AudioSource sound = Utils.AddAudio(gameObject, spelldata.sound, false, false, 1f);
				sound.Play();
			}
		} else {
			//fart out I guess?
		}
		triggeredSpell = true;
		StartCoroutine (delayRecycle (3f));
	}

	IEnumerator delayRecycle(float delay){
		yield return new WaitForSeconds (delay);
		gameObject.Recycle ();
	}
	
}
