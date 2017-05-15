using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	[SerializeField]
	private Slider healthBar;
	private const int maxHealth = 100;
	[SyncVar (hook = "OnChangeHealth")] 
	private int currentHealth = maxHealth;

	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

		print ("TakeDamage");
		currentHealth -= amount;
		if (currentHealth <= 0) 
		{
			currentHealth = 0;

			Destroy (gameObject);
		}
		
	}

	void OnChangeHealth(int health)
	{
		healthBar.value = health;
	}

}
