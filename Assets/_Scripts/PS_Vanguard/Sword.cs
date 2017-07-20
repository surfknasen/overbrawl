using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sword : NetworkBehaviour, Interface_Attack 
{
	private Vanguard_Abilities vanguardAbilities;
	[SyncVar]
	public float damage;
	public float lifeSteal;

	void Start()
	{
		vanguardAbilities = GetComponentInParent<Vanguard_Abilities>();
		//damage = 25;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.CompareTag("HostileChest"))
		{
				HostileChestHealth chestHealth = other.gameObject.GetComponent<HostileChestHealth>();
				chestHealth.TakeDamage(damage);
				GetComponentInParent<Health>().Cmd_AddHealth(lifeSteal);
				return;
		}

		Health health = other.gameObject.GetComponent<Health>();
		if(health != null)
		{
			if(vanguardAbilities.AnimatorIsPlaying()) 
			{
				health.TakeDamage(damage);
				GetComponentInParent<Health>().Cmd_AddHealth(lifeSteal);
			}
		}
	}

    float Interface_Attack.getDamage()
    {
        return damage;
    }
    bool Interface_Attack.isActive()
    {
        return vanguardAbilities.AnimatorIsPlaying();
    }
}
