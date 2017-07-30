using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LevelHandler : NetworkBehaviour
{

	[SyncVar (hook = "OnChangeBalance")]
	public int balance;
	public int currentLevel;	
	private int requiredExp;
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

	[Command]
	public void Cmd_AddToBalance(int amount)
	{
		balance += amount;
	}

	void OnChangeBalance(int bal)
	{
		balance = bal;
		LevelUp();
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

	public void LevelUp()
	{
		if(!isLocalPlayer) return;
		requiredExp = currentLevel * 100;

		if(balance >= requiredExp)
		{
			UpgradeStats();
			currentLevel++;
			levelText.text = "LEVEL " + currentLevel.ToString();
			GetComponent<UpgradeCanvasHandler>().CreateUpgradeCanvas();
			expSlider.value = 0;
			balance = balance - requiredExp;
			LevelUp();
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
		IncreaseHealth(health.maxHealth / 10);
		IncreaseAttackDamage();
	}

	public void IncreaseHealth(float amount)
	{
		health.Cmd_ChangeMaxHealth(health.maxHealth + amount);
	}


	public void IncreaseAttackDamage()
	{
		if(balance >= 0)
		{
			switch(activeClass)
			{
			case "Vanguard":
				Vanguard_Abilities vanguard = GetComponent<Vanguard_Abilities>();					
				vanguard.shieldDamage += vanguard.shieldDamage / 100 * 3;
				vanguard.swordDamage += vanguard.swordDamage / 100 * 3;
			break;
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = GetComponent<Gunslinger_Abilities>();
				gunslinger.damage += gunslinger.damage / 100 * 3;
			break;
			}
		}
	}


	
}
