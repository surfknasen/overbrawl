﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LevelHandler : NetworkBehaviour
{

	public int balance;
	private int requiredExp;
	private int currentLevel;
	private Text balanceText;
	private Upgrades upgrades;
	Collider2D[] hitColliders;
	private Health health;
	private GameObject player;
	public string activeClass;
	private Slider expSlider;
	private Text levelText;
	private int upgradeBaseInt;

	// for the randomized upgrades
	

	void Start()
	{
		player = gameObject;
		health = player.GetComponent<Health>();
		expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		balanceText = GameObject.Find("CurrencyText").GetComponent<Text>();
		currentLevel = 1;
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
				hitColliders[i].transform.Translate(dir * Time.deltaTime * 5);
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
			LevelUp();
		}
	}

	[Command]
	void Cmd_DestroyPickedupCurrency(GameObject currencyObj)
	{
		Destroy(currencyObj);
	}

	void LevelUp()
	{
		requiredExp = (currentLevel * currentLevel + currentLevel + 3) * 20;
		
		if(balance >= requiredExp)
		{
			UpgradeStats();
			currentLevel++;
			levelText.text = "LEVEL " + currentLevel.ToString();
			GetComponent<Upgrades>().ChooseRandomUpgrades();
			expSlider.value = 0;
			balance = 0;
		} else
		{
			UpdateExpSlider();
		}
	}

	void UpdateExpSlider()
	{
		expSlider.value = (float)balance / requiredExp * 100;
	}

	void UpgradeStats()
	{
		IncreaseHealth(health.maxHealth / 5);
		IncreaseAttackDamage(requiredExp / 50);
	}

	public void IncreaseHealth(int amount)
	{
		health.maxHealth += amount;
		health.healthBar.maxValue += amount;
	}

	public void IncreaseAttackDamage(int amount)
	{
		if(balance >= 0)
		{
			switch(activeClass)
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
			
		}
	}

	// randomized upgrades

	
}
