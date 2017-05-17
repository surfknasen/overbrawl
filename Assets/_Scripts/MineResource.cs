using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MineResource : NetworkBehaviour {

	[SerializeField]
	private Slider healthBar;
	[SyncVar(hook = "OnChangeHealth")]
	private int currentHealth;
	
	void Start()
	{
		healthBar.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		IAttack iAttack = otherCol.gameObject.GetComponent<IAttack>();
		if(iAttack != null)
		{
			if(iAttack.isActive())
			{
				TakeDamage(iAttack.getDamage());
			}
		} 
	}
	
	void TakeDamage (int dmg) 
	{
		healthBar.gameObject.SetActive (true);
		healthBar.value -= dmg;
		currentHealth = (int)healthBar.value;

		if (healthBar.value > 0) return;

		Destroy (gameObject);
	}

	void OnChangeHealth(int health)
	{
		healthBar.value = health;
	}
}


