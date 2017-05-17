using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CurrencyHandler : NetworkBehaviour
{

	public int balance;
	private Text balanceText;
	Collider2D[] hitColliders;

	void Start()
	{
		balanceText = GameObject.Find("CurrencyText").GetComponent<Text>();
	}

	void Update()
	{
		Pickup();
	}

	void Pickup()
	{
		hitColliders = Physics2D.OverlapCircleAll(transform.position, 2);

		for(int i = 0; i < hitColliders.Length; i++)
		{
			if(hitColliders[i].gameObject.CompareTag("Currency"))
			{
				hitColliders[i].isTrigger = true;
				Vector3 dir = transform.position - hitColliders[i].transform.position;
				hitColliders[i].transform.Translate(dir * Time.deltaTime * 7);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		if(!isLocalPlayer) return;

		if(otherCol.gameObject.CompareTag("Currency"))
		{
			balance++;
			balanceText.text = balance.ToString();
			Cmd_DestroyPickedupCurrency(otherCol.gameObject);
		}
	}

	[Command]
	void Cmd_DestroyPickedupCurrency(GameObject currencyObj)
	{
		Destroy(currencyObj);
	}
}
