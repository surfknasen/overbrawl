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
	[SyncVar]
	public float lifeSteal;

	void Start()
	{
		vanguardAbilities = GetComponentInParent<Vanguard_Abilities>();
	}

	[Command]
	public void Cmd_SetLifesteal(int amount)
	{
		lifeSteal = amount;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		Health otherHealth = other.gameObject.GetComponent<Health>();

		if(other.gameObject.CompareTag("HostileChest"))
		{
				HostileChestHealth chestHealth = other.gameObject.GetComponent<HostileChestHealth>();
				chestHealth.TakeDamage(damage);
				otherHealth.Cmd_ChangeCurrentHealth(otherHealth.currentHealth + lifeSteal);
		}
		else if(otherHealth != null)
		{
			if(vanguardAbilities.AnimatorIsPlaying())
			{
				otherHealth.TakeDamage(damage);
				Health thisHealth = GetComponentInParent<Health>();
				thisHealth.Cmd_ChangeCurrentHealth(thisHealth.currentHealth + lifeSteal);
			}

		} else if(other.gameObject.CompareTag("Resource"))
		{
			if(vanguardAbilities.AnimatorIsPlaying())
			{
				Health thisHealth = GetComponentInParent<Health>();
				thisHealth.Cmd_ChangeCurrentHealth(thisHealth.currentHealth + lifeSteal);
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
