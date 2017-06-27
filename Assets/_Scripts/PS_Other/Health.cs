using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour { // TODO: ADD ATTACK INTERFACE

	public Slider healthBar;
	[SyncVar (hook = "OnChangeMaxHealth")] 
	public float maxHealth;
	[SyncVar (hook = "OnChangeCurrentHealth")] 
	public float currentHealth;
	public int regenDelay = 15;
	private float regenValue;

	
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
		healthBar.value = maxHealth;
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
		if (!isServer) return;

		currentHealth -= amount;
		if (currentHealth <= 0) 
		{
			currentHealth = 0;
			Destroy (gameObject);
		}
	}

	public void AddDamage(float amount)
	{
		if(!isServer) return;

		if(currentHealth < maxHealth)
		{
			currentHealth += amount;
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
