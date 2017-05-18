using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Upgrades : NetworkBehaviour 
{
	public GameObject upgradeShopCanvas;
	public GameObject player;
	private Health health;
	private CurrencyHandler currencyHandler;
//	public string activeClass;

	void Start()
	{
		StartCoroutine("LateStart");
	}

	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.1f);
		health = player.GetComponent<Health>();
		currencyHandler = player.GetComponent<CurrencyHandler>();

	}



	void Update () 
	{
		// show upgrade menu on escape button
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			upgradeShopCanvas.SetActive(true);
		}
	}


	public void Upgrade_IncreaseHealth(int amount)
	{
		if(currencyHandler.balance >= 40)
		{
			health.maxHealth += amount;
			health.healthBar.maxValue += amount;
		}
	}

	public void Upgrade_Attack(int amount)
	{
		if(currencyHandler.balance >= 0)
		{
		/*	switch(activeClass)
			{
				case "Vanguard":
				print(activeClass);
				player.GetComponent<Vanguard_Abilities>().shieldProjectileDamage += amount;
				player.GetComponentInChildren<Sword>().swordDamage += amount;
				break;
				case "Gunslinger":
				player.GetComponent<Gunslinger_Abilities>().bulletDamage += amount;
				break;
			}
			*/
		}
	}
}
