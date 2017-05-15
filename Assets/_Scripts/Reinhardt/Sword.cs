using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour 
{
	public Reinhardt_Abilities reinhardtAbilities;

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		Health health = otherCol.gameObject.GetComponent<Health>();
		if(health != null)
		{
			if(reinhardtAbilities.AnimatorIsPlaying()) 
			{
				print(gameObject.transform.parent);
				health.TakeDamage(15);
			}
		}

		

	}
}
