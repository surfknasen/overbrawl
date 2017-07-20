using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostileChest : NetworkBehaviour {

	public Sprite chestOpen;
	public Sprite chestClosed;
	private bool attack = true;
	private Vector3 direction;
	public SpriteRenderer sprite;

	void Update()
	{
		if(attack)
		{
			sprite.sprite = chestOpen;
		} else
		{
			sprite.sprite = chestClosed;
		}
	}

	void FixedUpdate()
	{
		if(attack)
		{
 			transform.position += direction * Time.deltaTime * 50;
		}
	}

	public IEnumerator BecomeHostile(GameObject target)
	{
		attack = true;
		direction = (target.transform.position - transform.position).normalized;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.Rotate (0, 0, 90);
		yield return new WaitForSeconds(.75f);
		attack = false;
		yield return new WaitForSeconds(.5f);
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
				col.gameObject.GetComponent<Health>().TakeDamage(50);
			} else
			{
				return;
			}
			
		}
	}
}
