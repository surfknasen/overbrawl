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
	private float lifeSteal;

	public void SetProjectileProperties(GameObject obj, float lS)
	{
		owner = obj;
		lifeSteal = lS;
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
					owner.GetComponent<Health>().AddDamage(lifeSteal);
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
