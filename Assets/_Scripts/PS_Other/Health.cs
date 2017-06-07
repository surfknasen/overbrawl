using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public Slider healthBar;
	public float maxHealth;
	[SyncVar (hook = "OnChangeHealth")] 
	private float currentHealth;

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.maxValue = currentHealth;
		healthBar.value = currentHealth;
	}

	public void TakeDamage(float amount)
	{
		if (!isServer)
			return;

		currentHealth -= amount;
		print(currentHealth);
		if (currentHealth <= 0) 
		{
			currentHealth = 0;
			Destroy (gameObject);
		}
		
	}

	void OnChangeHealth(float health)
	{
		healthBar.value = health;
		print(healthBar.value);
	}

}
