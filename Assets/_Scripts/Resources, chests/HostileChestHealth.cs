using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HostileChestHealth : NetworkBehaviour {

	public Slider healthBar;
	[SyncVar (hook = "OnChangeMaxHealth")] 
	public float maxHealth;
	[SyncVar (hook = "OnChangeCurrentHealth")] 
	public float currentHealth;

	
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
		healthBar.value = maxHealth;
	}

	public void TakeDamage(float amount)
	{
		if (!isServer) return;

		currentHealth -= amount;
		print((maxHealth * amount) / 400);
		if (currentHealth <= 0) 
		{
			currentHealth = 0;
			Destroy (gameObject);
		}
	}

	void OnChangeCurrentHealth(float health)
	{
		healthBar.value = health;
	}

	void OnChangeMaxHealth(float health)
	{
		healthBar.maxValue = maxHealth;
	}


}


