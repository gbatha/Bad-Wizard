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
}


[System.Serializable]
public struct spellData {
	public string spellcombo;
	public spellType type;
	public GameObject theObject;
	public AudioClip sound;
}

public enum spellType { Projectile, TurnInto, Effect, Gravity };
//SPEED
//scale
//amount of the effect (fire, poison)
//spin
//determined by how quickly you cast and how many keys you hit