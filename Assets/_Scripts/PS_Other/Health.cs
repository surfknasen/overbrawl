using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public Slider healthBar;
	public int maxHealth;
	[SyncVar (hook = "OnChangeHealth")] 
	private int currentHealth;

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.maxValue = currentHealth;
		healthBar.value = currentHealth;
	}

	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

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
