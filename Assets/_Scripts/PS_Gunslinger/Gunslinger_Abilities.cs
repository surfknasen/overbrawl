using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gunslinger_Abilities : NetworkBehaviour 
{
	public Animator gunslingerGunsController;
	[HideInInspector]
	public float damage;
	[HideInInspector]
	public float attackSpeed;	
	[SyncVar] [HideInInspector]
	public float lifeSteal;
	[SyncVar] [HideInInspector]
	public bool freezeUpgrade;
	[SyncVar] [HideInInspector]
	public float freezeDuration;
	[SyncVar] [HideInInspector]
	public bool poisonUpgrade;
	[SyncVar] [HideInInspector]
	public float poisonAmount;
	[HideInInspector]
	public float attackRange;
	[HideInInspector]
	public GameObject bullet;
	public GameObject laserBullet;
	private bool shootingBullet;
	private bool speedBoostActive;
	private bool mouseOverPlayer;
	private bool laserBeamReady;
	private bool ultimateAttackActive;
	private bool ultimateAttackReady;
	[SerializeField]
	private GameObject[] bulletSpawnPositions;
	private int bulletsFired;
	

	
	void Start()
	{
		damage = 8;
		attackSpeed = 0.2f;
		attackRange = 1f;
		gunslingerGunsController.speed = attackSpeed * 4;
		ultimateAttackReady = true;
		laserBeamReady = true;
	}

	[Command] // THESE COMMANDS DO NOT HAVE TO BE SET, THEY CAN BE PASSED IN TO THE SHOOT COMMAND
	public void Cmd_SetLifeSteal(int amount)
	{
		lifeSteal = amount;
	}

	[Command]
	public void Cmd_FreezeUpgrade(bool freezeUpg, float freezeDur)
	{
		freezeUpgrade = freezeUpg;
		freezeDuration = freezeDur;
	}

	[Command]
	public void Cmd_PoisonUpgrade(bool poisonUpg, float _poisonAmount)
	{
		poisonUpgrade = poisonUpg;
		poisonAmount = _poisonAmount;
	}

	void Update () 
	{
		if(!isLocalPlayer) return;

		if (Input.GetMouseButton (0) && !mouseOverPlayer && !ultimateAttackActive) 
		{
			if(!shootingBullet)
			{
				StartCoroutine ("ShootBulletNumerator");
				Cmd_ShootBullet (GetMouseDirection(), damage, attackRange);
				Cmd_GunAnimation();	
			}

		} else if (Input.GetMouseButton (2) && !speedBoostActive) 
		{
			StartCoroutine("SpeedBoost");
		} else if(Input.GetMouseButton(1) && laserBeamReady)
		{
			StartCoroutine("LaserBeam");
		}else if(Input.GetKeyDown(KeyCode.R) && ultimateAttackReady)
		{
			StartCoroutine("InitiateUltimateAttack");
			StartCoroutine("UltimateAttackCooldown");			
		}
	}

	IEnumerator SpeedBoost()
	{
		speedBoostActive = true;
		PlayerMovement playerMovement = GetComponent<PlayerMovement>();
		playerMovement.Cmd_ChangeMoveSpeed(playerMovement.moveSpeed + 5);
		yield return new WaitForSeconds(3);
		playerMovement.Cmd_ChangeMoveSpeed(playerMovement.moveSpeed - 5);
		speedBoostActive = false;
	}

	IEnumerator UltimateAttackCooldown()
	{
		ultimateAttackReady = false;
		yield return new WaitForSeconds(90);
		ultimateAttackReady = true;
	}
	
	IEnumerator InitiateUltimateAttack()
	{
		ultimateAttackActive = true;
		for(int i = 0; i < 100; i++)
		{
			Cmd_ShootBullet(GetMouseDirection(), damage, attackRange);
			Cmd_GunAnimation();
			yield return new WaitForSeconds(0.05f);
		}
		ultimateAttackActive = false;
	}

	IEnumerator LaserBeam()
	{
		laserBeamReady = false;
		Cmd_LaserBeam(GetMouseDirection(), damage * 8);
		Cmd_GunAnimation();
		yield return new WaitForSeconds(10);
		laserBeamReady = true;
	}

	[Command]
	public void Cmd_LaserBeam(Vector3 dir, float dmg)
	{
		for(int i = 0; i < 2; i++)
		{
			GameObject b = Instantiate (laserBullet, bulletSpawnPositions[i].transform.position + -transform.up, bullet.transform.rotation);	
			b.GetComponent<Projectile>().SetProjectileProperties(gameObject, lifeSteal, dmg, freezeUpgrade, freezeDuration, poisonUpgrade, poisonAmount);		
			Rigidbody2D r = b.GetComponent<Rigidbody2D> ();
			r.velocity = dir * 80;
			NetworkServer.Spawn (b);
			Destroy (b, .75f);
		}
	}

	IEnumerator ShootBulletNumerator()
	{
		shootingBullet = true;
		yield return new WaitForSeconds (attackSpeed);
		shootingBullet = false;
	}

	[Command]
	public void Cmd_ShootBullet(Vector3 dir, float dmg, float _attackRange)
	{
		for(int i = 0; i < 2; i++)
		{
			GameObject b = Instantiate (bullet, bulletSpawnPositions[i].transform.position, bullet.transform.rotation);	
			b.GetComponent<Projectile>().SetProjectileProperties(gameObject, lifeSteal, dmg, freezeUpgrade, freezeDuration, poisonUpgrade, poisonAmount);		
			Rigidbody2D r = b.GetComponent<Rigidbody2D> ();
			r.velocity = dir * 30;
			NetworkServer.Spawn (b);
			Destroy (b, _attackRange);
		}
	}

	[Command]
	public void Cmd_ChangeAttackSpeed(float newSpeed)
	{
		attackSpeed = newSpeed;
		gunslingerGunsController.speed = attackSpeed * 4;
	}


	[Command]
	void Cmd_GunAnimation()
	{
		if(!AnimatorIsPlaying()) 
		{
			Rpc_GunAnimation();
		}
	}

	[ClientRpc]
	void Rpc_GunAnimation()
	{
		gunslingerGunsController.SetTrigger("Shoot");
	}

	 public bool AnimatorIsPlaying()
	 {
         return gunslingerGunsController.GetCurrentAnimatorStateInfo(0).length >
                gunslingerGunsController.GetCurrentAnimatorStateInfo(0).normalizedTime;
     }
	void OnMouseEnter()
	{
		mouseOverPlayer = true;
	}

	void OnMouseExit()
	{
		mouseOverPlayer = false;
	}

/*	IEnumerator Teleport(Vector3 dir)
	{
		moveSpeedBoostActive = true;

		for (int i = 0; i < 4; i++) 
		{
			transform.position += dir * 2;
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);			
			yield return new WaitForSeconds (0.01f);
		}
		yield return new WaitForSeconds(2f);
		moveSpeedBoostActive = false;
	}
	*/

	Vector3 GetMouseDirection()
	{
		Vector3 mousePosition = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 direction = (Input.mousePosition - mousePosition).normalized;

		return direction;
	}

}
