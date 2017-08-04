using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShockwaveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) 
	{
		if(!GetComponent<Animation>().isPlaying) return;

		if(other.gameObject.CompareTag("Player") && other.gameObject != this.gameObject) // add hostile chest here
		{
			Health playerHealth = other.GetComponent<Health>();
			playerHealth.TakeDamage(GetComponentInParent<Vanguard_Abilities>().swordDamage * 2, gameObject);
		} else if(other.gameObject.CompareTag("Resource"))
		{
			MineResource resourceHealth = other.GetComponent<MineResource>();
			resourceHealth.TakeDamage(GetComponentInParent<Vanguard_Abilities>().swordDamage * 4);
		} 
	}
}
