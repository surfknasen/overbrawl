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
	public Text healthText;
	[SyncVar]
	public float regenValue;	
	[SyncVar]
	public int regenDelay;	
	private bool alreadyPosioned;

	void Start()
	{
		StartCoroutine("RegenerateHealthDelay");
	}

	[Command]
	public void Cmd_SetRegenerateProperties(float regenValue, int regenDelay)
	{
		this.regenValue = regenValue;
		this.regenDelay = regenDelay;
	}

	public IEnumerator RegenerateHealthDelay()
	{
		while(true)
		{	
			yield return new WaitForSeconds(regenDelay);
			RegenerateHealth();
		}
							
	}

	void RegenerateHealth()
	{
		if(!isLocalPlayer) return;		
		
		if(currentHealth < maxHealth){
			Cmd_ChangeCurrentHealth(currentHealth + regenValue);
			if(currentHealth > maxHealth)
			{
				Cmd_ChangeCurrentHealth(currentHealth = maxHealth);
			}
		} else
		{
			return;
		}
	}

	public void TakeDamage(float amount, GameObject attacker)
	{
		if (!isServer) return;
		currentHealth -= amount;

		if (currentHealth <= 0) 
		{
			attacker.GetComponent<LevelHandler>().Cmd_AddToBalance(600);
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
		maxHealth = (int)health;
		currentHealth = maxHealth;
	}	

	[Command]
	public void Cmd_ChangeCurrentHealth(float health)
	{
		currentHealth = (int)health;
		if(currentHealth > maxHealth) currentHealth = maxHealth;
	}

	void OnChangeCurrentHealth(float health)
	{
		currentHealth = (int)health;
		healthBar.value = currentHealth;
		healthText.text = currentHealth + " / " + maxHealth;
	}

	void OnChangeMaxHealth(float health)
	{
		maxHealth = (int)health;
		healthBar.maxValue = maxHealth;
		healthText.text = currentHealth + " / " + maxHealth;
	}

	public IEnumerator PoisonTarget(GameObject other, float poisonAmount)
	{
		if(alreadyPosioned) yield break;
		alreadyPosioned = true;
		for(int i = 0; i < 6; i++)
		{
			currentHealth -= poisonAmount;
			yield return new WaitForSeconds(1);		
		}
		alreadyPosioned = false;
		
	}

}
