using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateAllClients : NetworkBehaviour {

	public override void OnStartLocalPlayer()
	{
		UpdateHealth();
	}

	void UpdateHealth()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++)
		{
			Health health = players[i].GetComponent<Health>();
			health.healthBar.maxValue = health.maxHealth;
			health.healthBar.value = health.maxHealth;
			health.healthText.text = health.currentHealth + " / " + health.maxHealth;
		}
	}
}
