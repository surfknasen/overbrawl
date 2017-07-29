using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NewUpgrades : NetworkBehaviour 
{
	// button array
	//loop through button array and add a random upgrade
	// if the upgrade already exists
	public Sprite S_IncreaseMoveSpeed, S_IncreaseDamage, S_IncreaseAttackSpeed, S_IncreaseHealth, S_IncreaseHealthRegen,
	S_Freeze, S_LifeSteal, S_Poison;
	public Button[] buttons;
	public Image[] upgradeIcons;
	public Text[] upgradeText;

	[HideInInspector]
	public string activeClass;	

	private List<int> selectedUpgrades = new List<int>();
	private LevelHandler levelHandler;
	private PlayerMovement playerMovement;
	private UpgradeCanvasHandler upgradeHandler;

	/*private Dictionary<string, int> tiers = new Dictionary<string, int>() //-------// Tiers to limit some upgrades
	{
		{"IncreaseMoveSpeed", 1},
		{"IncreaseDamage", 1},
		{"IncreaseAttackSpeed", 1},
		{"IncreaseHealth", 1},
		{"IncreaseHealthRegen", 1},
		{"Freeze", 1},
		{"LifeSteal", 1},
		{"Poison", 1}
	};
	*/

	void Start()
	{
		levelHandler = GetComponentInParent<LevelHandler>();
		playerMovement = GetComponentInParent<PlayerMovement>();
		upgradeHandler = GetComponentInParent<UpgradeCanvasHandler>();
		SelectUpgrades();
	}

	
	public void SelectUpgrades()
	{

		for(int i = 0; i < 4; i++) // four upgrade buttons
		{
			int rand = Random.Range(0, 8); // 8 different upgrades so far

			if(!selectedUpgrades.Contains(rand))
			{
				selectedUpgrades.Add(rand);

				switch(selectedUpgrades[i])
				{
					case 0:	// DONE -- WORKS SINGLEPLAYER // ??? MULTIPLAYER
						switch(upgradeHandler.tiers["IncreaseMoveSpeed"])
						{
							case 1:
								buttons[i].onClick.AddListener(UG_IncreaseMoveSpeed);
								upgradeIcons[i].sprite = S_IncreaseMoveSpeed;
								upgradeText[i].text = "Increase movement speed";
							break;
							case 2:
								if(levelHandler.currentLevel >= 5)
								{
									buttons[i].onClick.AddListener(UG_IncreaseMoveSpeed);
									upgradeIcons[i].sprite = S_IncreaseMoveSpeed;
									upgradeText[i].text = "Increase movement speed";
								}
								else
								{
									selectedUpgrades.Remove(rand); 
									i--;
								}
							break;
							case 3:
								if(levelHandler.currentLevel >= 10)
								{
									buttons[i].onClick.AddListener(UG_IncreaseMoveSpeed);
									upgradeIcons[i].sprite = S_IncreaseMoveSpeed;
									upgradeText[i].text = "Increase movement speed";
								}
								else
								{
									selectedUpgrades.Remove(rand);
									i--;
								}
							break;
						}
					break;
					case 1: // DONE -- WORKS SINGLEPLAYER // ??? MULTIPLAYER
						buttons[i].onClick.AddListener(UG_IncreaseDamage);
						upgradeIcons[i].sprite = S_IncreaseDamage;
						upgradeText[i].text = "Increase damage";
					break;
					case 2: // DONE -- WORKS SINGLEPLAYER // ??? MULTIPLAYER
						switch(upgradeHandler.tiers["IncreaseAttackSpeed"])
						{
							case 1:
								buttons[i].onClick.AddListener(UG_IncreaseAttackSpeed);
								upgradeIcons[i].sprite = S_IncreaseAttackSpeed;
								upgradeText[i].text = "Increase attack speed";
							break;
							case 2:
								if(levelHandler.currentLevel >= 5)
								{
									buttons[i].onClick.AddListener(UG_IncreaseAttackSpeed);
									upgradeIcons[i].sprite = S_IncreaseAttackSpeed;
									upgradeText[i].text = "Increase attack speed";
								}
								else
								{
									selectedUpgrades.Remove(rand); 
									i--;
								}
							break;
							case 3:
								if(levelHandler.currentLevel >= 10)
								{
									buttons[i].onClick.AddListener(UG_IncreaseAttackSpeed);
									upgradeIcons[i].sprite = S_IncreaseAttackSpeed;
									upgradeText[i].text = "Increase attack speed";
								}
								else
								{
									selectedUpgrades.Remove(rand);
									i--;
								}
							break;
						}
					break;
					case 3: // DONE -- WORKS SINGLEPLAYER // ??? MULTIPLAYER
						buttons[i].onClick.AddListener(UG_IncreaseHealth);
						upgradeIcons[i].sprite = S_IncreaseHealth;
						upgradeText[i].text = "Increase health";
					break;
					case 4: // DONE -- WORKS SINGLEPLAYER // ??? MULTIPLAYER
						switch(upgradeHandler.tiers["IncreaseHealthRegen"])
						{
							case 1:
								buttons[i].onClick.AddListener(UG_IncreaseHealthRegen);
								upgradeIcons[i].sprite = S_IncreaseHealthRegen;
								upgradeText[i].text = "Increase health regen";
							break;
							case 2:
								if(levelHandler.currentLevel >= 5)
								{
									buttons[i].onClick.AddListener(UG_IncreaseHealthRegen);
									upgradeIcons[i].sprite = S_IncreaseHealthRegen;
									upgradeText[i].text = "Increase health regen";
								}
								else
								{
									selectedUpgrades.Remove(rand); 
									i--;
								}
							break;
							case 3:
								if(levelHandler.currentLevel >= 10)
								{
									buttons[i].onClick.AddListener(UG_IncreaseHealthRegen);
									upgradeIcons[i].sprite = S_IncreaseHealthRegen;
									upgradeText[i].text = "Increase health regen";
								}
								else
								{
									selectedUpgrades.Remove(rand);
									i--;
								}
							break;
						}
					break;
					case 5: // TODO
						switch(upgradeHandler.tiers["Freeze"])
						{
							case 1:
								buttons[i].onClick.AddListener(UG_Freeze);
								upgradeIcons[i].sprite = S_Freeze;
								upgradeText[i].text = "Freeze";
							break;
							case 2:
								if(levelHandler.currentLevel >= 5)
								{
									buttons[i].onClick.AddListener(UG_Freeze);
									upgradeIcons[i].sprite = S_Freeze;
									upgradeText[i].text = "Freeze";
								}
								else
								{
									selectedUpgrades.Remove(rand); 
									i--;
								}
							break;
							case 3:
								if(levelHandler.currentLevel >= 10)
								{
									buttons[i].onClick.AddListener(UG_Freeze);
									upgradeIcons[i].sprite = S_Freeze;
									upgradeText[i].text = "Freeze";
								}
								else
								{
									selectedUpgrades.Remove(rand);
									i--;
								}
							break;
						}
					break;
					case 6:	// DONE --- WORKS SINGLEPLAYER // ??? MULTIPLAYER
						switch(upgradeHandler.tiers["LifeSteal"])
						{
							case 1:
								buttons[i].onClick.AddListener(UG_LifeSteal);
								upgradeIcons[i].sprite = S_LifeSteal;
								upgradeText[i].text = "Increase life steal";
							break;
							case 2:
								if(levelHandler.currentLevel >= 5)
								{
									buttons[i].onClick.AddListener(UG_LifeSteal);
									upgradeIcons[i].sprite = S_LifeSteal;
									upgradeText[i].text = "Increase life steal";
								}
								else
								{
									selectedUpgrades.Remove(rand); 
									i--;
								}
							break;
							case 3:
								if(levelHandler.currentLevel >= 10)
								{
									buttons[i].onClick.AddListener(UG_LifeSteal);
									upgradeIcons[i].sprite = S_LifeSteal;
									upgradeText[i].text = "Increase life steal";
								}
								else
								{
									selectedUpgrades.Remove(rand);
									i--;
								}
							break;
						}
					break;
					case 7: // TODO
						switch(upgradeHandler.tiers["Poison"])
						{
							case 1:
								upgradeIcons[i].sprite = S_Poison;
								upgradeText[i].text = "Poison";
							break;
							case 2:
								if(levelHandler.currentLevel >= 5)
								{
									upgradeIcons[i].sprite = S_Poison;
									upgradeText[i].text = "Poison";
								}
								else
								{
									selectedUpgrades.Remove(rand); 
									i--;
								}
							break;
							case 3:
								if(levelHandler.currentLevel >= 10)
								{
									upgradeIcons[i].sprite = S_Poison;
									upgradeText[i].text = "Poison";
								}
								else
								{
									selectedUpgrades.Remove(rand);
									i--;
								}
							break;
						}
					break;
				}

			} else
			{
				i--;
			}
		}

		selectedUpgrades.Clear();
	}

	void UG_IncreaseMoveSpeed()
	{
		switch(upgradeHandler.tiers["IncreaseMoveSpeed"])
		{
			case 1:
				playerMovement.moveSpeed += playerMovement.moveSpeed / 100 * 5;
			break;
			case 2:
				playerMovement.moveSpeed += playerMovement.moveSpeed / 100 * 10;
			break;
			case 3:
				playerMovement.moveSpeed += playerMovement.moveSpeed / 100 * 15;
			break;
		}

		print("Increased movement speed, tier " + upgradeHandler.tiers["IncreaseMoveSpeed"]);		// ----------------------------------- PRINT

		upgradeHandler.tiers["IncreaseMoveSpeed"]++;
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}

	void UG_IncreaseDamage()
	{
		switch(activeClass)
		{
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = GetComponentInParent<Gunslinger_Abilities>();
				gunslinger.damage += gunslinger.damage / 25 * upgradeHandler.tiers["IncreaseDamage"];
			break;
			case "Vanguard":
				Vanguard_Abilities vanguard = GetComponentInParent<Vanguard_Abilities>(); 
				vanguard.swordDamage += vanguard.swordDamage / 25 * upgradeHandler.tiers["IncreaseDamage"];
				vanguard.shieldDamage += vanguard.shieldDamage / 25 * upgradeHandler.tiers["IncreaseDamage"];
			break;
		}

			print("Increased damage, tier " + upgradeHandler.tiers["IncreaseDamage"]); // ----------------------------------- PRINT

		upgradeHandler.tiers["IncreaseDamage"]++;
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}

	void UG_IncreaseAttackSpeed()
	{
		switch(activeClass)
		{
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = GetComponentInParent<Gunslinger_Abilities>();

				switch(upgradeHandler.tiers["IncreaseAttackSpeed"])
				{
					case 1:
						gunslinger.Cmd_ChangeAttackSpeed(gunslinger.attackSpeed - gunslinger.attackSpeed * 0.20f);
						print(gunslinger.attackSpeed);
					break;
					case 2:
						gunslinger.Cmd_ChangeAttackSpeed(gunslinger.attackSpeed - gunslinger.attackSpeed * 0.25f);
					break;
					case 3:
						gunslinger.Cmd_ChangeAttackSpeed(gunslinger.attackSpeed - gunslinger.attackSpeed * 0.25f);
					break;
				}
			break;
			case "Vanguard":
				Vanguard_Abilities vanguard = GetComponentInParent<Vanguard_Abilities>();

				switch(upgradeHandler.tiers["IncreaseAttackSpeed"])
				{
					case 1:
						vanguard.Cmd_ChangeAttackSpeed(vanguard.attackSpeed - vanguard.attackSpeed * 0.20f);
					break;
					case 2:
						vanguard.Cmd_ChangeAttackSpeed(vanguard.attackSpeed - vanguard.attackSpeed * 0.25f);
					break;
					case 3:
						vanguard.Cmd_ChangeAttackSpeed(vanguard.attackSpeed - vanguard.attackSpeed * 0.30f);
					break;
				}
			break;
		}

		print("IncreaseAttackSpeed tier " + upgradeHandler.tiers["IncreaseAttackSpeed"]);  // ----------------------------------- PRINT
		upgradeHandler.tiers["IncreaseAttackSpeed"]++;
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}

	void UG_IncreaseHealth()
	{
		Health health = GetComponentInParent<Health>();
		health.Cmd_ChangeMaxHealth(health.maxHealth + health.maxHealth * upgradeHandler.tiers["IncreaseHealth"] / 2);

		print("Health tier " + upgradeHandler.tiers["IncreaseHealth"]); // ----------------------------------- PRINT
		upgradeHandler.tiers["IncreaseHealth"]++;
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}

	void UG_IncreaseHealthRegen()
	{
		Health health = GetComponentInParent<Health>();
		switch(upgradeHandler.tiers["IncreaseHealthRegen"])
		{
			case 1:
				health.Cmd_SetRegenerateProperties(50, health.regenDelay);
			break;
			case 2:
				health.Cmd_SetRegenerateProperties(100, health.regenDelay);
			break;
			case 3:
				health.Cmd_SetRegenerateProperties(150, health.regenDelay);
			break;
		}

		upgradeHandler.tiers["IncreaseHealthRegen"]++;
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}

	void UG_Freeze() // DOES NOT WORK ON THE SWORD FOR VANGUARD
	{
		switch(activeClass)
		{
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = GetComponentInParent<Gunslinger_Abilities>();

				switch(upgradeHandler.tiers["Freeze"])
				{
					case 1:
						gunslinger.Cmd_FreezeUpgrade(true, 1);
					break;
					case 2:
						gunslinger.Cmd_FreezeUpgrade(true, 2);
					break;
					case 3:
						gunslinger.Cmd_FreezeUpgrade(true, 3);
					break;
				}
			break;
			case "Vanguard":
				Vanguard_Abilities vanguard = GetComponentInParent<Vanguard_Abilities>();
				Sword sword = transform.parent.GetComponentInChildren<Sword>();

				switch(upgradeHandler.tiers["Freeze"])
				{
					case 1:
						vanguard.Cmd_FreezeUpgrade(true, 1);
						sword.Cmd_FreezeUpgrade(true, 1);
					break;
					case 2:
						vanguard.Cmd_FreezeUpgrade(true, 2);
						sword.Cmd_FreezeUpgrade(true, 2);
					break;
					case 3:
						vanguard.Cmd_FreezeUpgrade(true, 3);
						sword.Cmd_FreezeUpgrade(true, 3);
					break;
				}
			break;
		}

		upgradeHandler.tiers["Freeze"]++;
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}

	void UG_LifeSteal()
	{
		switch(activeClass)
		{
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = GetComponentInParent<Gunslinger_Abilities>();

				switch(upgradeHandler.tiers["LifeSteal"])
				{
					case 1:
						gunslinger.Cmd_SetLifeSteal(2);
					break;
					case 2:
						gunslinger.Cmd_SetLifeSteal(6);
					break;
					case 3:
						gunslinger.Cmd_SetLifeSteal(9);
					break;
				}
			break;
			case "Vanguard":
				Sword sword = transform.parent.GetComponentInChildren<Sword>();

				switch(upgradeHandler.tiers["LifeSteal"])
				{
					case 1:
						sword.Cmd_SetLifesteal(5);
					break;
					case 2:
						sword.Cmd_SetLifesteal(15);
					break;
					case 3:
						sword.Cmd_SetLifesteal(25);
					break;
				}
			break;
		}

		upgradeHandler.tiers["LifeSteal"]++;
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}

	void UG_Poison()
	{	
		GetComponentInParent<UpgradeCanvasHandler>().RemoveLatestUpgradeCanvas(gameObject);
		Destroy(gameObject);
	}


	/*
	 * UPGRADES THAT ARE ONLY CONSIDERED
	 */
	void UG_Invisibility() // needs its own script
	{

	}

	void UG_ExtraDamageEveryXHit() // needs its own script
	{

	}

	void UG_PassThroughWalls()
	{

	}

	void UG_PickupRange()
	{

	}

	void UG_IncreasedDamageOnResource()
	{

	}

	void UG_IncreaseRangedAttackRange()
	{

	}	


}
