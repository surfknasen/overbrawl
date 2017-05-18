using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, Interface_Attack 
{
	private Vanguard_Abilities vanguardAbilities;
	public int swordDamage = 20;

	void Start()
	{
		vanguardAbilities = GetComponentInParent<Vanguard_Abilities>();
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		Health health = otherCol.gameObject.GetComponent<Health>();
		if(health != null)
		{
			if(vanguardAbilities.AnimatorIsPlaying()) 
			{
				health.TakeDamage(15);
			}
		}
	}

    int Interface_Attack.getDamage()
    {
        return swordDamage;
    }
    bool Interface_Attack.isActive()
    {
        return vanguardAbilities.AnimatorIsPlaying();
    }
}
