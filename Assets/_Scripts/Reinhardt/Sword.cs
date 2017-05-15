using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D otherCol)
	{
		Health health = otherCol.gameObject.GetComponent<Health>();
		if(health == null) return;

		if(gameObject.transform.parent.GetComponent<Animation>().isPlaying) 
		{
			print(gameObject.transform.parent);
			health.TakeDamage(15);
		}

	}
}
