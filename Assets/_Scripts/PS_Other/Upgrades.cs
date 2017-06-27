using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Upgrades : NetworkBehaviour 					// TODO: CHANGE VALUES (LVLs, Multiply percentage)
{
	public GameObject upgradeCanvas;
	private Health health;
	[HideInInspector]
	public string activeClass;
	public Button[] buttons;
	public Text[] upgradesText;
	private PlayerMovement playerMovement;
	private LevelHandler levelHandler;		
				
	private Dictionary<string, int> tiers = new Dictionary<string, int>() //-------// Tiers to limit some upgrades
	{
		{"IncreaseMoveSpeed", 1},
		{"IncreaseDamage", 1},
		{"IncreaseAttackSpeed", 1},
		{"IncreaseHealth", 1},
		{"IncreaseHealthRegen", 1},
		{"Freeze", 1},
		{"LifeSteal", 1},
		{"Bleed", 1}
	};

	void Start()
	{
		StartCoroutine("LateStart"); // ---------------- // TO GIVE TIME FOR THE PLAYER TO SPAWN
	}

	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.1f);
		playerMovement = GetComponent<PlayerMovement>();
		health = GetComponent<Health>();
		levelHandler = GetComponent<LevelHandler>();
	}

	public void ChooseRandomUpgrades() //---------------// THIS IS RUN WHEN YOU LEVEL UP //---------------//
	{
		RandomNumber();
	}

	void RandomNumber() //---------------// Generates a set of random ints to choose upgrades //---------------//
	{
		Random.State currentState = Random.state; 
        Random.InitState((int)Time.deltaTime * 100); 
        Random.state = currentState; 

		List<int> generatedNumbers = new List<int>();
		generatedNumbers.Add(-1); //---------------// need a bunch of slots for when I check if a slot already contains random number
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1);
		generatedNumbers.Add(-1); //---------------//


		//---------------// Here I generate 4 unique random numbers between a certain range
		int i = 0;
		int buttonIndex = 0;

		while(i < 8) //---------------// 8 is the amount of upgrades I have
		{
			print("Inside while loop");
			int randomNumber = (int)Random.Range(0,8);
			print("RandomNumber " + randomNumber);
			if(!generatedNumbers.Contains(randomNumber)) 
			{
				generatedNumbers[i] = randomNumber;	
				SelectUpgrades(randomNumber, buttonIndex);
				buttonIndex++;				
				i++;
				if(buttonIndex == 4) //---------------// Amount of buttons for the upgrades, break if all upgrades have been picked
				{
					break;
				}
			}
		}
		upgradeCanvas.SetActive(true);
		generatedNumbers.Clear();
	}

	void SelectUpgrades(int randomNumber, int buttonIndex) //---------------// Selects the upgrade with the random numbers
	{
		switch(randomNumber) 
		{
			case 0:  //---------------// INCREASES MOVEVEMENT SPEED //---------------// LVL LIMIT //---------------//
				switch(tiers["IncreaseMoveSpeed"]) 
				{	 										
					case 1:
						buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);	
						upgradesText[buttonIndex].text = "Speed : Tier" + tiers["IncreaseMoveSpeed"];
					break;									
					case 2:
						if(levelHandler.currentLevel >= 5)
						{
							buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);
							upgradesText[buttonIndex].text = "Speed : Tier" + tiers["IncreaseMoveSpeed"];
						} else
						{
							SelectUpgrades(Random.Range(0,8), buttonIndex);
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
							SelectUpgrades(Random.Range(0,8), buttonIndex);						
						}
					break;
				}
			break;
			case 1: //---------------// INCREASE DAMAGE //---------------// 
				buttons[buttonIndex].onClick.AddListener(UG_IncreasedDamage);
				upgradesText[buttonIndex].text = "Damage";
			break;
			case 2: //---------------// INCREASED ATTACK SPEED //---------------// LVL LIMIT //---------------//
				switch(tiers["IncreaseMoveSpeed"]) 
				{	 										
					case 1:
						buttons[buttonIndex].onClick.AddListener(UG_IncreaseAttackSpeed);	
						upgradesText[buttonIndex].text = "Attack Speed : Tier" + tiers["IncreaseAttackSpeed"];
					break;									
					case 2:
						if(levelHandler.currentLevel >= 10)
						{
							buttons[buttonIndex].onClick.AddListener(UG_IncreaseAttackSpeed);
							upgradesText[buttonIndex].text = "Attack Speed : Tier" + tiers["IncreaseAttackSpeed"];
						} else
						{
							SelectUpgrades(Random.Range(0,8), buttonIndex);
						}
					break;
					case 3:
						if(levelHandler.currentLevel >= 15)
						{
							buttons[buttonIndex].onClick.AddListener(UG_IncreaseAttackSpeed);
							upgradesText[buttonIndex].text = "Attack Speed : Tier" + tiers["IncreaseAttackSpeed"];
						}
						else
						{
							SelectUpgrades(Random.Range(0,8), buttonIndex);						
						}
					break;
				}
			break;
			case 3: //---------------// INCREASED HEALTH //---------------//
				buttons[buttonIndex].onClick.AddListener(UG_IncreaseHealth);
				upgradesText[buttonIndex].text = "Health";
			break;
			case 4: //---------------// INCREASED HEALTH REGEN //---------------// LVL LIMIT //---------------//
				switch(tiers["IncreaseHealthRegen"]) 
				{	 										
					case 1:
						buttons[buttonIndex].onClick.AddListener(UG_IncreaseHealthRegen);	
						upgradesText[buttonIndex].text = "Health Regen : Tier" + tiers["IncreaseHealthRegen"];
					break;									
					case 2:
						if(levelHandler.currentLevel >= 5)
						{
							buttons[buttonIndex].onClick.AddListener(UG_IncreaseHealthRegen);
							upgradesText[buttonIndex].text = "Health Regen : Tier" + tiers["IncreaseHealthRegen"];
						} else
						{
							SelectUpgrades(Random.Range(0,8), buttonIndex);
						}
					break;
					case 3:
						if(levelHandler.currentLevel >= 10)
						{
							buttons[buttonIndex].onClick.AddListener(UG_IncreaseHealthRegen);
							upgradesText[buttonIndex].text = "Health Regen : Tier" + tiers["IncreaseHealthRegen"];
						}
						else
						{
							SelectUpgrades(Random.Range(0,8), buttonIndex);						
						}
					break;
				}
			break;
			case 5: //---------------// FREEZE //---------------// LVL LIMIT //---------------//
				buttons[buttonIndex].onClick.AddListener(UG_Freeze);
				upgradesText[buttonIndex].text = "Freeze : Tier" + tiers["Freeze"];
			break;
			case 6: //---------------// LIFE STEAL //---------------// LVL LIMIT //---------------//
				switch(tiers["LifeSteal"]) 
				{	 										
					case 1:
						buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);	
						upgradesText[buttonIndex].text = "Life Steal : Tier" + tiers["LifeSteal"];
					break;									
					case 2:
						if(levelHandler.currentLevel >= 5)
						{
							buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);
							upgradesText[buttonIndex].text = "Life Steal : Tier" + tiers["LifeSteal"];
						} else
						{
							SelectUpgrades(Random.Range(0,8), buttonIndex);
						}
					break;
					case 3:
						if(levelHandler.currentLevel >= 10)
						{
							buttons[buttonIndex].onClick.AddListener(UG_IncreaseMoveSpeed);
							upgradesText[buttonIndex].text = "Life Steal : Tier" + tiers["LifeSteal"];
						}
						else
						{
							SelectUpgrades(Random.Range(0,8), buttonIndex);						
						}
					break;
				}
			break;
			case 7: //---------------// BLEED //---------------// LVL LIMIT //---------------//
				buttons[buttonIndex].onClick.AddListener(UG_Bleed);
				upgradesText[buttonIndex].text = "Bleed : Tier" + tiers["Bleed"];
			break;
		}
	}

	void UG_IncreaseMoveSpeed() 
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
		}
		print("Increased movespeed");
		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}	
		upgradeCanvas.SetActive(false);
		tiers["IncreaseMoveSpeed"]++;
	}

	void UG_IncreasedDamage() 
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

		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}
		upgradeCanvas.SetActive(false);
		tiers["IncreaseDamage"]++; // not exactly a tier
		
	}

	void UG_IncreaseAttackSpeed() 
	{
		Vanguard_Abilities vanguard = GetComponent<Vanguard_Abilities>();	
		Gunslinger_Abilities gunslinger = GetComponent<Gunslinger_Abilities>();

		switch(activeClass)
		{
			case "Vanguard":
				switch(tiers["IncreaseAttackSpeed"])
				{
					case 1:
						vanguard.attackSpeed += vanguard.attackSpeed / 100 * 20;					// TEMPORARY VALUE // MODIFY //
						print("Increased vanguard attack speed " + vanguard.attackSpeed);
					break;
					case 2:
						vanguard.attackSpeed += vanguard.attackSpeed / 100 * 25;					// TEMPORARY VALUE // MODIFY //
						print("Increased vanguard attack speed " + vanguard.attackSpeed);
					break;
					case 3:
						vanguard.attackSpeed += vanguard.attackSpeed / 100 * 30;					// TEMPORARY VALUE // MODIFY //
						print("Increased vanguard attack speed " + vanguard.attackSpeed);
					break;
				}
			break;
			case "Gunslinger":
				switch(tiers["IncreaseAttackSpeed"])
				{
					case 1:
						gunslinger.attackSpeed -= gunslinger.attackSpeed / 100 * 10;					// TEMPORARY VALUE // MODIFY //
						print("Increased gunslinger attack speed " + gunslinger.attackSpeed);
					break;
					case 2:
						gunslinger.attackSpeed -= gunslinger.attackSpeed / 100 * 15;					// TEMPORARY VALUE // MODIFY //
						print("Increased gunslinger attack speed " + gunslinger.attackSpeed);
					break;
					case 3:
						gunslinger.attackSpeed -= gunslinger.attackSpeed / 100 * 20;					// TEMPORARY VALUE // MODIFY //
						print("Increased gunslinger attack speed " + gunslinger.attackSpeed);
					break;
				}
			break;
		}

		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}
		upgradeCanvas.SetActive(false);
		tiers["IncreaseAttackSpeed"]++;

	}

	void UG_IncreaseHealth() 
	{
		Health health = GetComponent<Health>();
		health.maxHealth += 50 * tiers["IncreaseHealth"];

		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}
		upgradeCanvas.SetActive(false);
		tiers["IncreaseAttackSpeed"]++;

	}

	void UG_IncreaseHealthRegen()
	{
		Health health = GetComponent<Health>();
		switch(tiers["IncreaseHealthRegen"])
		{
			case 1:
				health.regenDelay = 12;
				print(health.regenDelay);
			break;
			case 2:
				health.regenDelay = 8;
			break;
			case 3:
				health.regenDelay = 4;
			break;
		}

		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}
		upgradeCanvas.SetActive(false);
		tiers["IncreaseHealthRegen"]++;
	}

	void UG_Freeze() // TODO
	{
		print("Bullets now slow targets down");
		upgradeCanvas.SetActive(false);
	}

	void UG_LifeSteal()
	{
		Sword vanguard = GetComponentInChildren<Sword>();
		Gunslinger_Abilities gunslinger = GetComponent<Gunslinger_Abilities>();

		switch(activeClass)
		{
			case "Vanguard":
				switch(tiers["LifeSteal"])
				{
					case 1:
						vanguard.lifeSteal += vanguard.damage / 100 * 10;
					break;
					case 2:
						vanguard.lifeSteal += vanguard.damage / 100 * 20;
					break;
					case 3:
						vanguard.lifeSteal += vanguard.damage / 100 * 30;
					break;
				}
			break;
			case "Gunslinger":
				switch(tiers["LifeSteal"])
				{
					case 1:
						gunslinger.lifeSteal += gunslinger.damage / 100 * 10;
					break;
					case 2:
						gunslinger.lifeSteal += gunslinger.damage / 100 * 20;
					break;
					case 3:
						gunslinger.lifeSteal += gunslinger.damage / 100 * 30;
					break;
				}
			break;
		}

		for(int i = 0; i < upgradesText.Length; i++)
		{
			upgradesText[i].text = null;
		}

		upgradeCanvas.SetActive(false);
		tiers["LifeSteal"]++;

		
	}

	void UG_Bleed() // TODO
	{
	}
	
}
