using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Chest : NetworkBehaviour {

	public Color hostileColor;	
	public GameObject chestText;
	public GameObject hostileChest;
	public Sprite chestOpen;
	private string playerClass;
	SpriteRenderer sprite;
	private bool opened;
	private bool tint;

	void Start () 
	{
		chestText.SetActive(false);
		sprite = GetComponent<SpriteRenderer>();
	}
	
	void OnTriggerStay2D (Collider2D col) 
	{
		if(opened) return;

		if(col.gameObject.CompareTag("Player"))
		{
			if(!col.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) return;

			chestText.SetActive(true);					
			
			if(Input.GetKeyDown(KeyCode.E))
			{
				chestText.SetActive(false);
				int num = Random.Range(0, 100);

				if(num < 20)
				{
					StartCoroutine("SpawnHostileChest", col.gameObject);
				}
				else
				{
					GetComponent<SpriteRenderer>().sprite = chestOpen;		
					GivePlayerExp(col.gameObject);			
			//		DecidePickupType(col.gameObject);
					Destroy(gameObject, 3);
				}
				opened = true;
			}
		}
	}	

	void Update()
	{
		if(tint)
		{
			sprite.color = Color.Lerp(sprite.color, hostileColor, Time.deltaTime);
		}
	}


	IEnumerator SpawnHostileChest(GameObject obj)
	{
		tint = true;
		yield return new WaitForSeconds(1f);
		obj.GetComponent<SpawnHostileChest>().Cmd_SpawnHostileChest(gameObject);
	}

	void GivePlayerExp(GameObject player)
	{
		for(int i = 0; i < 4; i++)
		{
			player.GetComponent<LevelHandler>().balance += 5000;
			player.GetComponent<LevelHandler>().LevelUp();
		}	
	}


	void DecidePickupType(GameObject player)
	{
		// 50% statboost
		// 40% powerup
		// 10% ultimate attack
		Random.InitState(System.Environment.TickCount);
		int num = Random.Range(0, 100);
		if(num <= 50) // statboost
		{
			StatBoost(player);
			print("Statboost");
		}else if(num > 50) // powerup
		{
//			Powerup(player);
			print("powerup");
		}else if(num >= 80) // ultimate attack
		{
	//		UltimateAttack(player);
			print("Ultimate attack");
		}
	}

	void StatBoost(GameObject player)
	{
		// decide stat: //
		// attack
		// health
		// attack speed
		// movement speed
		// etc

		int num = Random.Range(0, 100);		
		Health health = player.GetComponent<Health>();
		PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

		playerClass = player.GetComponent<LevelHandler>().activeClass;

		switch(playerClass)
		{
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = player.GetComponent<Gunslinger_Abilities>();

				if(num < 25) // main attack damage +50%, duration: 30 sec ////////// WORKS
				{
					gunslinger.damage += gunslinger.damage / 2;
					StartCoroutine(DisableStatBoost(30, "AttackDamage", player, gunslinger.damage / 2));
					print("main attack damage +50%, duration: 30 sec");
				}
				else if(num < 50) // health +100%, duration 45 sec ////////// WORKS
				{
					health.Cmd_ChangeMaxHealth(health.maxHealth * 2);
					health.Cmd_ChangeCurrentHealth(health.maxHealth * 2);
					StartCoroutine(DisableStatBoost(45, "Health", player, health.maxHealth));
					print("health +100%, duration 45 sec");
				} else if(num < 75) // attackspeed 33%, duration 30 sec ////// SHOULD WORK
				{
					gunslinger.Cmd_ChangeAttackSpeed(gunslinger.attackSpeed + gunslinger.attackSpeed / 3);
					StartCoroutine(DisableStatBoost(30, "AttackSpeed", player, gunslinger.attackSpeed / 3));
					print("attackspeed 33%, duration 30 sec");
				} else if(num < 100) // movespeed 33%, duration 30 sec ////////// WORKS
				{
					playerMovement.moveSpeed += playerMovement.moveSpeed / 3;
					StartCoroutine(DisableStatBoost(30, "MoveSpeed", player, playerMovement.moveSpeed / 3));
					print("movespeed 33%, duration 45 sec");
				} 
			break;
			case "Vanguard":
				Vanguard_Abilities vanguardAbilities = player.GetComponent<Vanguard_Abilities>();

				if(num < 25) // main attack damage +50%, duration: 30 sec ////////// WORKS
				{
					vanguardAbilities.swordDamage += vanguardAbilities.swordDamage / 2;
					StartCoroutine(DisableStatBoost(30, "AttackDamage", player, vanguardAbilities.swordDamage / 2));
					print("main attack damage +50%, duration: 30 sec");
				}
				else if(num < 50) // health +100%, duration 45 sec ////////// WORKS
				{
					health.Cmd_ChangeMaxHealth(health.maxHealth * 2);
					health.Cmd_ChangeCurrentHealth(health.maxHealth * 2);
					StartCoroutine(DisableStatBoost(45, "Health", player, health.maxHealth));
					print("health +100%, duration 45 sec");
				} else if(num < 75) // attackspeed 33%, duration 30 sec ////////// WORKS
				{
					vanguardAbilities.Cmd_ChangeAttackSpeed(vanguardAbilities.attackSpeed + vanguardAbilities.attackSpeed / 3);
					StartCoroutine(DisableStatBoost(30, "AttackSpeed", player, vanguardAbilities.attackSpeed / 3));
					print("attackspeed 33%, duration 30 sec");
				} else if(num < 100) // movespeed 33%, duration 30 sec ////////// WORKS
				{
					playerMovement.moveSpeed += playerMovement.moveSpeed / 3;
					StartCoroutine(DisableStatBoost(30, "MoveSpeed", player, playerMovement.moveSpeed / 3));
					print("movespeed 33%, duration 45 sec");
				} 
			break;
		}
	}

	IEnumerator DisableStatBoost(int duration, string stat, GameObject player, float amount)
	{
		yield return new WaitForSeconds(duration);
		
		Health health = player.GetComponent<Health>();
		PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

		switch(playerClass)
		{
			case "Gunslinger":
				Gunslinger_Abilities gunslinger = player.GetComponent<Gunslinger_Abilities>();

				switch(stat)
				{
					case "AttackDamage":
						gunslinger.damage -= gunslinger.damage / 2;
						print("Disabled AttackDamage");
					break;
					case "Health":
						health.Cmd_ChangeMaxHealth(health.maxHealth - amount);
						print("Disabled Health");
					break;
					case "AttackSpeed":
						gunslinger.Cmd_ChangeAttackSpeed(gunslinger.attackSpeed - amount);
						print("Disabled AttackSpeed");
					break;
					case "MoveSpeed":
						playerMovement.moveSpeed -= amount;
						print("Disabled MoveSpeed");
					break;
				}
			break;
			case "Vanguard":

				Vanguard_Abilities vanguard = player.GetComponent<Vanguard_Abilities>();
			//	Sword sword = player.GetComponent<Sword>();

				switch(stat)
					{
						case "AttackDamage":
							vanguard.swordDamage -= amount;
							print("Disabled AttackDamage");
						break;
						case "Health":
							health.Cmd_ChangeMaxHealth(health.maxHealth - amount);
							print("Disabled Health");
						break;
						case "AttackSpeed":
							vanguard.Cmd_ChangeAttackSpeed(vanguard.attackSpeed - amount);
							print("Disabled AttackSpeed");
						break;
						case "MoveSpeed":
							playerMovement.moveSpeed -= amount;
							print("Disabled MoveSpeed");
						break;
					}
			break;
		}
	}

	/*void Powerup(GameObject player)
	{
		// decide powerup
		// 5 different powerups:
		// immortality
		// --
		// --
		// --
		// --

		int num = Random.Range(0, 100);

		if(num < 50)
		{	
			player.AddComponent<Powerup_Immortal>();
		} else if (num < 100)
		{
			
		}
	}

	void UltimateAttack(GameObject player)
	{
		// give player its ultimate attack
	}
*/
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			chestText.SetActive(false);
		}
	}
}
