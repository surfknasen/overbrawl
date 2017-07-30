using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gunslinger_Abilities : NetworkBehaviour 
{
	[SerializeField]
	private GameObject bullet;
	private bool shootingBullet;
	private bool teleporting;
	private bool mouseOverPlayer;
	[SerializeField]
	private GameObject[] bulletSpawnPositions;
	private int bulletsFired;
	public Animator gunslingerGunsController;
	public float damage;
	public float attackSpeed;	
	[SyncVar]
	public float lifeSteal;
	[SyncVar]
	public bool freezeUpgrade;
	[SyncVar]
	public float freezeDuration;
	[SyncVar]
	public bool poisonUpgrade;
	[SyncVar]
	public float poisonAmount;
	
	void Start()
	{
		damage = 10;
		attackSpeed = 0.2f;
		gunslingerGunsController.speed = attackSpeed * 2;
	}

	[Command]
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

		if (Input.GetMouseButton (0) && !mouseOverPlayer) 
		{
			if(!shootingBullet)
			{
				StartCoroutine ("ShootBulletNumerator");
				Cmd_ShootBullet (GetMouseDirection(), damage);
				Cmd_GunAnimation();	
			}

		} else if (Input.GetMouseButton (1) && !teleporting) 
		{
			StartCoroutine (Teleport(GetMouseDirection()));
		}
	}
	

	IEnumerator ShootBulletNumerator()
	{
		shootingBullet = true;
		yield return new WaitForSeconds (attackSpeed);
		shootingBullet = false;
	}

	[Command]
	public void Cmd_ShootBullet(Vector3 dir, float dmg)
	{
		for(int i = 0; i < 2; i++)
		{
			GameObject b = Instantiate (bullet, bulletSpawnPositions[i].transform.position, bullet.transform.rotation);	
			b.GetComponent<Projectile>().SetProjectileProperties(gameObject, lifeSteal, dmg, freezeUpgrade, freezeDuration, poisonUpgrade, poisonAmount);		
			Rigidbody2D r = b.GetComponent<Rigidbody2D> ();
			r.velocity = dir * 30;
			NetworkServer.Spawn (b);
			Destroy (b, 1f);

		}
	}

	[Command]
	public void Cmd_ChangeAttackSpeed(float newSpeed)
	{
		attackSpeed = newSpeed;
		gunslingerGunsController.speed = attackSpeed * 2;
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

	IEnumerator Teleport(Vector3 dir)
	{
		teleporting = true;

		for (int i = 0; i < 4; i++) 
		{
			transform.position += dir * 2;
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);			
			yield return new WaitForSeconds (0.01f);
		}
		yield return new WaitForSeconds(2f);
		teleporting = false;
	}

	Vector3 GetMouseDirection()
	{
		Vector3 mousePosition = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 direction = (Input.mousePosition - mousePosition).normalized;

		return direction;
	}

}
