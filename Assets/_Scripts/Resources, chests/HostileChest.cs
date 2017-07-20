using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileChest : MonoBehaviour {

	bool attack = true;
	Vector3 direction;

	void FixedUpdate()
	{
		if(attack)
		{
 			transform.position += direction * Time.deltaTime * 60;
		}
	}

	public IEnumerator BecomeHostile(GameObject target)
	{
		attack = true;
		direction = (target.transform.position - transform.position).normalized;
		yield return new WaitForSeconds(.5f);
		attack = false;
		yield return new WaitForSeconds(1f);
		StartCoroutine("BecomeHostile", target);
	}	

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			if(attack == true)
			{
				attack = false;
				float playerHealth = col.gameObject.GetComponent<Health>().maxHealth * 0.10f;
				col.gameObject.GetComponent<Health>().TakeDamage(15);
			} else
			{
				return;
			}
			
		}
	}
}
