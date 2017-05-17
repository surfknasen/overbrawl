﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IAttack 
{
	private Vanguard_Abilities vanguardAbilities;

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

    int IAttack.getDamage()
    {
        return 20;
    }
    bool IAttack.isActive()
    {
        return vanguardAbilities.AnimatorIsPlaying();
    }
}