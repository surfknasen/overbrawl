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
		maxHealth = (int)healthBar.value;
		currentHealth = maxHealth;
	}
/*  ------------------------------------------------------------ THIS DOES NOT WORK -- ADD PROJECTILE OWNER
	void OnTriggerEnter2D(Collider2D otherCol)
	{
		Attack attack = otherCol.gameObject.GetComponent<Attack>();
		if(attack != null)
		{
			if(attack.isActive())
			{
				TakeDamage(attack.getDamage());
			}
		} 
	}
*/
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
