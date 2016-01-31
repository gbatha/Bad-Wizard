using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour {
	[SerializeField]
	public spellData[] spells;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//returns a spell by bast code
	//cast code MUST be in order from lowest to higest
	public spellData getSpell(string castCode){
		for (int i = 0; i < spells.Length; i++) {
			if(castCode == spells[i].spellcombo){
				return spells[i];
			}
		}
		//we didn't find it!!!
		return new spellData();
	}
}


[System.Serializable]
public struct spellData {
	public string spellcombo;
	public spellType type;
	public GameObject theObject;
	public AudioClip sound;
	public float projectileForce;
}

public enum spellType { Projectile, TurnInto, Effect, Gravity };
//SPEED
//scale
//amount of the effect (fire, poison)
//spin
//determined by how quickly you cast and how many keys you hit