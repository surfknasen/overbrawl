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
	public int regenDelay = 5;
	public Text healthText;
	public float regenValue;
	
	public IEnumerator RegenerateHealth()
	{
		if(!isServer) yield return null;
		if(regenValue == 0) StopCoroutine("RegenerateHealth");

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

	[Command]
	public void Cmd_AddHealth(float amount)
	{
		if(!isServer) return;

		if(currentHealth < maxHealth)
		{
			currentHealth += amount;
		}	
	}

	[Command]
	public void Cmd_ChangeMaxHealth(float health)
	{
		maxHealth = health;
	}	

	[Command]
	public void Cmd_ChangeCurrentHealth(float health)
	{
		currentHealth = health;
	}

	void OnChangeCurrentHealth(float health)
	{
		currentHealth = health;
		healthBar.value = currentHealth;
		healthText.text = currentHealth + " / " + maxHealth;
	}

	void OnChangeMaxHealth(float health)
	{
		maxHealth = health;
		healthBar.maxValue = maxHealth;
		healthText.text = currentHealth + " / " + maxHealth;
	}

}
