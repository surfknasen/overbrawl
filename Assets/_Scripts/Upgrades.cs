using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Upgrades : NetworkBehaviour 
{
	public GameObject upgradeCanvas;
	private Health health;
	private LevelHandler currencyHandler;
	[HideInInspector]
	public string activeClass;
	public Button[] buttons;
	private PlayerMovement playerMovement;
	private Dictionary<string, int> tiers = new Dictionary<string, int>()
	{
		{"IncreasedMoveSpeed", 1}
	};

	void Start()
	{
		StartCoroutine("LateStart");
	}

	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.1f);
		playerMovement = GetComponent<PlayerMovement>();
		health = GetComponent<Health>();
		currencyHandler = GetComponent<LevelHandler>();
	}

	public void ChooseRandomUpgrades()
	{
		RandomNumber();
	}

	void RandomNumber()
	{
		// generate 4 unique random numbers between a certain range
		for(int i = 0; i < 4; i++)
		{
			int randomNumber = (int)Random.Range(0,4);
			SelectUpgrades(randomNumber, i);
		}
		upgradeCanvas.SetActive(true);
	}

	void SelectUpgrades(int randomNumber, int buttonIndex)
	{
		switch(randomNumber)
		{
			case 0:
			buttons[buttonIndex].onClick.AddListener(UG_IncreasedMoveSpeed);
			break;
			case 1:
			buttons[buttonIndex].onClick.AddListener(UG_IncreasedDamage);
			break;
			case 2:
			buttons[buttonIndex].onClick.AddListener(UG_AttackTransfer);
			break;
			case 3:
			buttons[buttonIndex].onClick.AddListener(UG_Freeze);
			break;
		}
	}

	void UG_IncreasedMoveSpeed() // Increased movement speed 	
	{
		switch(tiers["IncreasedMoveSpeed"])
		{
			case 1:
			playerMovement.moveSpeed += playerMovement.moveSpeed / 100 * 5;
			print(playerMovement.moveSpeed);
			break;
			case 2:
			playerMovement.moveSpeed += playerMovement.moveSpeed / 100 * 10;
			print(playerMovement.moveSpeed);
			break;
			case 3:
			playerMovement.moveSpeed += playerMovement.moveSpeed / 100 * 15;
			print(playerMovement.moveSpeed);
			break;
			default:
			return;
		}
		print("Increased movespeed");
	//	upgradeCanvas.SetActive(false);
		tiers["IncreasedMoveSpeed"] += 1;
		print(tiers["IncreasedMoveSpeed"]);
	}

	void UG_IncreasedDamage() // Extra damage
	{
		print("Increased damage");
	//	upgradeCanvas.SetActive(false);
	}

	void UG_AttackTransfer() // Transfer an attack's damage to another attack		
	{
		print("Transfered attack damage");
	//	upgradeCanvas.SetActive(false);
	}

	void UG_Freeze() // Hits slow down target	
	{
		print("Bullets now slow targets down");
	//	upgradeCanvas.SetActive(false);
	}

	
}
