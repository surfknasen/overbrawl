using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Upgrades : NetworkBehaviour 
{
	public GameObject upgradeCanvas;
	private Health health;
	[HideInInspector]
	public string activeClass;
	public Button[] buttons;
	private PlayerMovement playerMovement;
	private LevelHandler levelHandler;
	private Dictionary<string, int> tiers = new Dictionary<string, int>()
	{
		{"IncreaseMoveSpeed", 1}
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
		levelHandler = GetComponent<LevelHandler>();
	}

	public void ChooseRandomUpgrades()
	{
		RandomNumber();
	}

	void RandomNumber()
	{
		List<int> generatedNumbers = new List<int>();
		generatedNumbers.Add(-1); // need four slots for when I check if a slot already contains random number
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1);

		// generate 4 unique random numbers between a certain range
		int i = 0;
		while(i < 4)
		{
			print("Inside while loop");
			int randomNumber = (int)Random.Range(0,4);
			if(!generatedNumbers.Contains(randomNumber))
			{
				generatedNumbers[i] = randomNumber;	
				SelectUpgrades(randomNumber, i);
				i++;
			}
		}
		upgradeCanvas.SetActive(true);
		generatedNumbers.Clear();
	}

	void SelectUpgrades(int randomNumber, int buttonIndex) // This selects the upgrade with the random number generated above
	{
		switch(randomNumber)
		{
			case 0:  // ERROR: THE GIVEN KEY WAS NOT PRESENT IN THE DICTIONARY
			switch(tiers["IncreaseMoveSpeed"])
			{											// check if the required level is met, if so then this upgrade can be selected
				case 2:
					if(levelHandler.currentLevel >= 5)
					{
						SelectUpgrades(Random.Range(0,4), buttonIndex);
						return;
					}
				break;
				case 3:
					if(levelHandler.currentLevel >= 10)
					{
						SelectUpgrades(Random.Range(0,4), buttonIndex);
						return;
					}
				break;
				default:
				break;
			}
			buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);
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

	void UG_IncreaseMoveSpeed() // Increased movement speed 	
	{
		switch(tiers["IncreaseMoveSpeed"])
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
		tiers["IncreaseMoveSpeed"] += 1;
		print(tiers["IncreaseMoveSpeed"]);
	}

	void UG_IncreasedDamage() // Extra damage
	{
		print("Increased damage");
	//	upgradeCanvas.SetActive(false);
	}

	void UG_AttackTransfer() // Transfer an attack's damage to another attack		
	{
		print("Transfered attack damage");
		//upgradeCanvas.SetActive(false);
	}

	void UG_Freeze() // Hits slow down target	
	{
		print("Bullets now slow targets down");
	//	upgradeCanvas.SetActive(false);
	}

	
}
