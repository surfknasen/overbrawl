using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Powerup_Immortal : NetworkBehaviour {

	Health health;
	float lastMaxHealth;

	void Start () 
	{
		health = GetComponent<Health>();
		lastMaxHealth = health.maxHealth;
		health.Cmd_ChangeMaxHealth(999999);
		health.Cmd_ChangeCurrentHealth(999999);
		StartCoroutine("DeactivatePowerup");
	}

	IEnumerator DeactivatePowerup()
	{
		yield return new WaitForSeconds(20);
		health.Cmd_ChangeMaxHealth(lastMaxHealth);
		health.Cmd_ChangeCurrentHealth(lastMaxHealth);
	}
	
}
