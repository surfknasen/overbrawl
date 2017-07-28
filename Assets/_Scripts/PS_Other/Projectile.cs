using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour, Interface_Attack
{
	private GameObject owner;
	public ParticleSystem hitParticle;	
	[HideInInspector]
	private float damage;
	private float lifeSteal;

	public void SetProjectileProperties(GameObject obj, float lS, float dmg)
	{
		owner = obj;
		lifeSteal = lS;
		damage = dmg;
	}

	[Command]
	void Cmd_SpawnParticleSystem()
	{
		ParticleSystem particle = Instantiate(hitParticle, transform.position, transform.rotation);
		Destroy(particle, 1);

		Rpc_SpawnParticleSystem();
	}

	[ClientRpc]
	void Rpc_SpawnParticleSystem()
	{
		ParticleSystem particle = Instantiate(hitParticle, transform.position, transform.rotation);
		Destroy(particle, 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(owner != null){
			Health ownerHealth = owner.GetComponent<Health>();
			if(other.gameObject.CompareTag("HostileChest"))
			{
				Cmd_SpawnParticleSystem();

				HostileChestHealth chestHealth = other.gameObject.GetComponent<HostileChestHealth>();
				chestHealth.TakeDamage(damage);
				ownerHealth.Cmd_ChangeCurrentHealth(ownerHealth.currentHealth + lifeSteal);
				Destroy(gameObject);															
			}
			else if(other.gameObject != owner && !other.transform.IsChildOf(owner.transform) && !other.gameObject.CompareTag("Currency")) 
			{
				Health otherHealth = other.gameObject.GetComponent<Health> ();
				
				if(otherHealth != null) 
				{
					otherHealth.TakeDamage (damage);
					Cmd_SpawnParticleSystem();					
					ownerHealth.Cmd_ChangeCurrentHealth(ownerHealth.currentHealth + lifeSteal);
				} 

				if(other.isTrigger == false)
				{
					Destroy(gameObject);	
					if(other.gameObject.CompareTag("Resource"))
					{
						ownerHealth.Cmd_ChangeCurrentHealth(ownerHealth.currentHealth + lifeSteal);
						Cmd_SpawnParticleSystem();
					}														
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
