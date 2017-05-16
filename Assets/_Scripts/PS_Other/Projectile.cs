using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour, IAttack
{
	private GameObject owner;
	[HideInInspector]
	public int damage;

	public void SetProjectileOwner(GameObject obj)
	{
		owner = obj;
	}

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
