using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, Interface_Attack 
{
	private Vanguard_Abilities vanguardAbilities;
	public float damage;
	public float lifeSteal;

	void Start()
	{
		vanguardAbilities = GetComponentInParent<Vanguard_Abilities>();
		damage = 15;
	}

	void OnTriggerEnter2D(Collider2D otherCol) // bug
	{
		Health health = otherCol.gameObject.GetComponent<Health>();
		if(health != null)
		{
			if(vanguardAbilities.AnimatorIsPlaying()) 
			{
				health.TakeDamage(damage);
				GetComponentInParent<Health>().AddDamage(lifeSteal);
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
