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

	void Start()
	{
		damage = 10;
	}

	public void SetProjectileProperties(GameObject obj, float lS)
	{
		owner = obj;
		lifeSteal = lS;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(owner != null){
			if(other.gameObject != owner && !other.transform.IsChildOf(owner.transform) && !other.gameObject.CompareTag("Currency")) 
			{
				Health health = other.gameObject.GetComponent<Health> ();
				if(health != null) 
				{
					health.TakeDamage (damage);
					owner.GetComponent<Health>().AddDamage(lifeSteal);
				}
				Destroy(gameObject);
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
