using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineResource : MonoBehaviour {

	[SerializeField]
	private Slider healthBar;

	void Start()
	{
		healthBar.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		var attack = otherCol.gameObject.GetComponent<Attack>();
		if(attack != null)
		{
			if(attack.isActive())
			{
				Damage(attack.getDamage());
			}
				
		} 
		
	}
	
	void Damage (int dmg) 
	{
		healthBar.gameObject.SetActive (true);
		healthBar.value -= dmg;
		if (healthBar.value > 0) return;
		Destroy (gameObject);
	}
}
