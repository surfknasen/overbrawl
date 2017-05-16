using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour, Attack
{
	private GameObject player;

	[HideInInspector]
	public int projectileDmg;

	public void ProjectileOwner(GameObject g) // make this a gameobject instead and return the player
	{
		player = g;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
	 	if(col.gameObject != player && !col.transform.IsChildOf(player.transform) && !col.gameObject.CompareTag("Enemy"))
		{
			Destroy(gameObject);
			Health health = col.gameObject.GetComponent<Health> ();
			if(health != null)
			{
				health.TakeDamage (projectileDmg);
			}
		}
	}

    public int getDamage()
    {
        return projectileDmg;
    }

    public bool isActive()
    {
        return true;
    }
}
