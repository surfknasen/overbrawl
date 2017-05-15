using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineResource : MonoBehaviour {

	[SerializeField]
	private Slider healthBar;
	private int dmg;

	void Start()
	{
		healthBar.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		switch(otherCol.gameObject.tag)
		{
			case "Sword":
				if(otherCol.gameObject.transform.parent.GetComponent<Animation>().isPlaying)
				{
					dmg = 25;
					ResourceHealth();
				}
			break;
			case "TracerProjectile":
				dmg = 20;
				ResourceHealth();
			break;
			case "ReinhardtProjectile":
				dmg = 80;
				ResourceHealth();
			break;
		}
		
	}
	
	void ResourceHealth () 
	{
		healthBar.gameObject.SetActive (true);
		healthBar.value -= dmg;
		if (healthBar.value > 0) return;
		Destroy (gameObject);
	}
}
