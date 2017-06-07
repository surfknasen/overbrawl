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
	public Text[] upgradesText;
	private PlayerMovement playerMovement;
	private LevelHandler levelHandler;
	private Dictionary<string, int> tiers = new Dictionary<string, int>()
	{
		{"IncreaseMoveSpeed", 1},
		{"IncreaseDamage", 1} // not exactly a tier, but the value the damage gets multiplied with
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
		Random.State currentState = Random.state; 
        Random.InitState((int)Time.deltaTime * 100); 
        Random.state = currentState; 

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
			print("RandomNumber " + randomNumber);
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
			case 0:  // INCREASE MOVEVEMENT SPEED
			switch(tiers["IncreaseMoveSpeed"]) 
			{	 										
				case 1:
					buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);	
					upgradesText[buttonIndex].text = "Speed : Tier" + tiers["IncreaseMoveSpeed"];
				break;										// check if the required level is met, if so then this upgrade can be selected
				case 2:
					if(levelHandler.currentLevel >= 5)
					{
						buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);
						upgradesText[buttonIndex].text = "Speed : Tier" + tiers["IncreaseMoveSpeed"];
					} else
					{
						SelectUpgrades(Random.Range(0,4), buttonIndex);
					}
				break;
				case 3:
					if(levelHandler.currentLevel >= 10)
					{
						buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);
						upgradesText[buttonIndex].text = "Speed : Tier" + tiers["IncreaseMoveSpeed"];
					}
					else
					{
						SelectUpgrades(Random.Range(0,4), buttonIndex);						
					}
				break;
			}
			break;
			case 1: // INCREASE DAMAGE
				buttons[buttonIndex].onClick.AddListener(UG_IncreasedDamage);
				upgradesText[buttonIndex].text = "Damage : Tier " + tiers["IncreaseDamage"];
			break;
			case 2: // ATTACK TRANSFER
			buttons[buttonIndex].onClick.AddListener(UG_AttackTransfer);
			break;
			case 3: // FREEZE
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
		upgradeCanvas.SetActive(false);
		tiers["IncreaseMoveSpeed"] += 1;
		print(tiers["IncreaseMoveSpeed"]);
		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}
	}

	void UG_IncreasedDamage() // Extra damage
	{
		switch(activeClass)
		{
			case "Vanguard":
				Vanguard_Abilities vanguard = GetComponent<Vanguard_Abilities>();					// shield projectile damage
				Sword sword = GetComponentInChildren<Sword>(); 										// sword damage
				vanguard.shieldDamage += vanguard.shieldDamage / 100 * tiers["IncreaseDamage"];
				sword.damage += sword.damage / 100 * tiers["IncreaseDamage"];
				print("Increased sword damage to " + sword.damage);		
				print("Increased projectile damage to " + vanguard.shieldDamage);		
			break;
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = GetComponent<Gunslinger_Abilities>();
				gunslinger.damage += gunslinger.damage / 100 * tiers["IncreaseDamage"];
				print("Increased damage to " + gunslinger.damage);		
			break;
		}
		upgradeCanvas.SetActive(false);
		tiers["IncreaseDamage"]++; // not exactly a tier
		
		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}
	}

	void UG_AttackTransfer() // Transfer an attack's damage to another attack		
	{
		print("Transfered attack damage");
		upgradeCanvas.SetActive(false);
	}

	void UG_Freeze() // Hits slow down target	
	{
		print("Bullets now slow targets down");
		upgradeCanvas.SetActive(false);
	}

	
}
