using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour, Interface_Attack
{
	private GameObject owner;
	[HideInInspector]
	public float damage;

	public void SetProjectileOwner(GameObject obj)
	{
		owner = obj;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(owner != null){
			if(other.gameObject != owner && !other.transform.IsChildOf(owner.transform) && !other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Currency")) // FIX
			{
				Destroy(gameObject);
				Health health = other.gameObject.GetComponent<Health> ();
				if(health != null) 
				{
					health.TakeDamage (damage);
				}
			}
		} 
	 	
	}

    public float getDamage()
    {
        return damage;
    }

    public bool isActive()
    {
        return true;
    }
}
