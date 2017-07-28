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
	bool open;

	void Start()
	{
		StartCoroutine("OpenClose");
	}

	void FixedUpdate()
	{
		if(attack)
		{
 			transform.position += direction * Time.deltaTime * 50;
		}
	}

	public IEnumerator OpenClose()
	{
		if(attack == true)
		{
			sprite.sprite = chestOpen;			
		}
		yield return new WaitForSeconds(.1f);
		sprite.sprite = chestClosed;
		yield return new WaitForSeconds(.1f);
		StartCoroutine("OpenClose");
	}
	
	public IEnumerator BecomeHostile(GameObject target)
	{
		attack = true;
		direction = (target.transform.position - transform.position).normalized;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.Rotate (0, 0, 90);
		yield return new WaitForSeconds(.75f);
		if(target == null) Destroy(gameObject);						
		attack = false;
		yield return new WaitForSeconds(.5f);
		StartCoroutine("BecomeHostile", target);
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			if(attack == true)
			{
				col.gameObject.GetComponent<Health>().TakeDamage(15);
				attack = false;
			} else
			{
				return;
			}
			
		}
	}
}
