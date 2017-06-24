using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour { // TODO: ADD ATTACK INTERFACE

	public Slider healthBar;
	public float maxHealth;
	[SyncVar (hook = "OnChangeHealth")] 
	public float currentHealth;
	public int regenDelay = 15;
	private float regenValue;

	

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.maxValue = currentHealth;
		healthBar.value = currentHealth;
		regenValue = maxHealth / 10;

		StartCoroutine("RegenerateHealth");;
	}


	IEnumerator RegenerateHealth()
	{
		if(currentHealth < maxHealth){
			yield return new WaitForSeconds(regenDelay);					
			currentHealth += regenValue;
			if(currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}
		} else
		{
			yield return null;
		}
		StartCoroutine("RegenerateHealth");				
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

	public void AddDamage(float amount)
	{
		if(!isServer)
			return;

		if(currentHealth < maxHealth)
		{
			currentHealth += amount;
		}	
	}

	void OnChangeHealth(float health)
	{
		healthBar.value = health;
		print(healthBar.value);
	}

}
