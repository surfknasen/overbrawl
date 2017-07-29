using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour, Interface_Attack
{
	public ParticleSystem hitParticle;	
	private bool freeze;	
	private float freezeDuration;
	private GameObject owner;
	private float damage;
	private float lifeSteal;

	public void SetProjectileProperties(GameObject obj, float lS, float dmg, bool freezeUpgrade, float freezeDur)
	{
		owner = obj;
		lifeSteal = lS;
		damage = dmg;
		freeze = freezeUpgrade;
		freezeDuration = freezeDur;
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
					
					if(freeze)
					{			
						PlayerMovement otherPlayerMovement = other.gameObject.GetComponent<PlayerMovement>();	
						otherPlayerMovement.StartCoroutine(otherPlayerMovement.FreezePlayer(other.gameObject, freezeDuration));		
					}
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

/*	IEnumerator FreezePlayer(GameObject playerToFreeze)
	{
		PlayerMovement otherPlayerMovement = playerToFreeze.GetComponent<PlayerMovement>();
		otherPlayerMovement.Cmd_ChangeMoveSpeed(otherPlayerMovement.moveSpeed - otherPlayerMovement.moveSpeed * 0.30f);
		float revertSpeed = otherPlayerMovement.moveSpeed * 0.30f;
		
		yield return new WaitForSeconds(freezeDuration);

		if(otherPlayerMovement != null)
		{
			otherPlayerMovement.Cmd_ChangeMoveSpeed(otherPlayerMovement.moveSpeed + revertSpeed);
		}
	}
*/
    public float getDamage()
    {
        return damage;
    }

    public bool isActive()
    {
        return true;
    }
}
