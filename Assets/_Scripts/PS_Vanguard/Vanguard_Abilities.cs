﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Vanguard_Abilities : NetworkBehaviour 
{
	private bool buttonPressed;
	public Animator swordAttackController;
	private bool teleporting;
	public GameObject vanguardShieldProjectile;
	private bool shieldProjectileShooting;
	private PlayerMovement playerMovement;
	private bool animationPlaying;
	public float shieldDamage;
	public float attackSpeed;
	public Sword sword;
	public float swordDamage;
	[SyncVar]
	public bool freezeUpgrade;
	[SyncVar]
	public float freezeDuration;


	void Start()
	{
		PlayerMovement playerMovement = GetComponent<PlayerMovement>();
		shieldDamage = 100f;
		swordDamage = 25f;
		attackSpeed = 0.9f;
		swordAttackController.speed = attackSpeed;
	}

	[Command]
	public void Cmd_FreezeUpgrade(bool freezeUpg, float freezeDur)
	{
		freezeUpgrade = freezeUpg;
		freezeDuration = freezeDur;
	}

	void Update () 
	{
		if(!isLocalPlayer) return;

		if(Input.GetMouseButton(0))
		{
			Cmd_SwordAttack(swordDamage, AnimatorIsPlaying());
		}
		else if(Input.GetMouseButton(1) && !shieldProjectileShooting)
		{
			Cmd_ShootProjectile(GetMouseDirection(), shieldDamage);
			StartCoroutine("ProjectileCooldown");
		} else if(Input.GetMouseButton(2) && !teleporting)
		{
			StartCoroutine("Teleport", GetMouseDirection());
		}
	}

	[Command]
	public void Cmd_ChangeAttackSpeed(float newSpeed)
	{
		attackSpeed = newSpeed;
		swordAttackController.speed = attackSpeed;
	}

	[Command]
	void Cmd_SwordAttack(float damage, bool attack)
	{
		if(!attack)
		{
			sword.damage = damage;
			sword.FreezeUpgrade(freezeUpgrade, freezeDuration);
			swordAttackController.SetTrigger("Attack");
			Rpc_SwordAttack();
		}
	}

	[ClientRpc]
	void Rpc_SwordAttack()
	{
		swordAttackController.SetTrigger("Attack");
	}

	public bool AnimatorIsPlaying()
	{
		return swordAttackController.GetCurrentAnimatorStateInfo(0).IsName("VanguardDefaultAttack");
	}
 

	IEnumerator ProjectileCooldown()
	{
		shieldProjectileShooting = true;
		yield return new WaitForSeconds(1f);
		shieldProjectileShooting = false;
	}

	[Command]
	public void Cmd_ShootProjectile(Vector3 dir, float dmg)
	{
		GameObject p = Instantiate (vanguardShieldProjectile, transform.position, transform.rotation);
		p.GetComponent<Projectile> ().SetProjectileProperties(gameObject, 0, dmg, freezeUpgrade, freezeDuration);
		Rigidbody2D r = p.GetComponent<Rigidbody2D> ();
		r.velocity = dir * 7;
		NetworkServer.Spawn (p);
		Destroy (p, 4f);
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
