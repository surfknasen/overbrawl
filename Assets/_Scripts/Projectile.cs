using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour, Attack
{
	public GameObject owner;

	[HideInInspector]
	public int damage;

	void OnTriggerEnter2D(Collider2D other)
	{
	 	if(other.gameObject != owner && !other.transform.IsChildOf(owner.transform) && !other.gameObject.CompareTag("Enemy"))
		{
			Destroy(gameObject);
			Health health = other.gameObject.GetComponent<Health> ();
			if(health != null) 
			{
				health.TakeDamage (damage);
			}
		}
	}

    public int getDamage()
    {
        return damage;
    }

    public bool isActive()
    {
        return true;
    }
}
