using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Chest : NetworkBehaviour {

	Collider2D[] hitColliders;
	public GameObject chestText;
	float lerpTime;
	Vector3 direction;
	bool attack;

	void Start () 
	{
		chestText.SetActive(false);
	}
	
	void OnTriggerStay2D (Collider2D col) 
	{
		if(col.gameObject.CompareTag("Player"))
		{
			chestText.SetActive(true);			

			if(Input.GetKeyDown(KeyCode.E))
			{
				StartCoroutine("BecomeHostile", col.gameObject);
				return;
			}
		}
	}

	void FixedUpdate()
	{
		if(attack)
		{
 			transform.position += direction * Time.deltaTime * 60;
		}
	}

	IEnumerator BecomeHostile(GameObject target)
	{
		attack = true;
		direction = (target.transform.position - transform.position).normalized;
		yield return new WaitForSeconds(.5f);
		attack = false;
		yield return new WaitForSeconds(1f);
		StartCoroutine("BecomeHostile", target);
	}	

	void ShowTextOnClient()
	{
		chestText.SetActive(true);		
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			chestText.SetActive(false);
		}
	}
}
