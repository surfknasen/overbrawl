using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineResource : MonoBehaviour {

	[SerializeField]
	private Slider healthBar;
	public Reinhardt_Abilities reinhardtAbilities;

	void Start()
	{
		healthBar.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		
		switch(otherCol.gameObject.tag)
		{
			case "Sword":
			//	if(reinhardtAbilities.AnimatorIsPlaying())
			//	{
					print("Sword");
					ResourceHealth(25);
			//	}
					
			break;
			case "TracerProjectile":
				ResourceHealth(20);
			break;
			case "ReinhardtProjectile":
				ResourceHealth(80);
			break;
		}
		
	}
	
	void ResourceHealth (int dmg) 
	{
		healthBar.gameObject.SetActive (true);
		healthBar.value -= dmg;
		if (healthBar.value > 0) return;
		Destroy (gameObject);
	}
}
