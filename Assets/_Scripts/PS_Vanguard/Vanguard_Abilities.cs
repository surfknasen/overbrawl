using System.Collections;
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
	private bool animationPlaying;

	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			Cmd_SwordAttack();
		}
		else if(Input.GetMouseButton(1) && !shieldProjectileShooting)
		{
			Cmd_ShootProjectile(GetMouseDirection());
			StartCoroutine("ProjectileCooldown");
		} else if(Input.GetMouseButton(2) && !teleporting)
		{
			StartCoroutine("Teleport", GetMouseDirection());
		}
	}

	[Command]
	void Cmd_SwordAttack()
	{
		if(!AnimatorIsPlaying())
		{
			Rpc_SwordAttack();
		}
	}

	[ClientRpc]
	void Rpc_SwordAttack()
	{
		swordAttackController.SetTrigger("Attack");
	//	StartCoroutine("AnimationPlayingNumerator");
	}

/*	IEnumerator AnimationPlayingNumerator()
	{
		yield return new WaitForSeconds(0.1f);
		animationPlaying = false;
	}
*/
	public bool AnimatorIsPlaying()
	{
		return swordAttackController.GetCurrentAnimatorStateInfo(0).IsName("VanguardDefaultAttack");
	}
 

	IEnumerator ProjectileCooldown()
	{
		shieldProjectileShooting = true;
		yield return new WaitForSeconds(6f);
		shieldProjectileShooting = false;
	}

	[Command]
	public void Cmd_ShootProjectile(Vector3 dir)
	{
		GameObject p = Instantiate (vanguardShieldProjectile, transform.position, transform.rotation);
		Rigidbody2D r = p.GetComponent<Rigidbody2D> ();
		r.velocity = dir * 5;
		p.GetComponent<Projectile> ().SetProjectileOwner(gameObject);
		p.GetComponent<Projectile> ().damage = 40;
		NetworkServer.Spawn (p);
		Destroy (p, 4f);
	}

	IEnumerator Teleport(Vector3 dir)
	{
		teleporting = true;

		for (int i = 0; i < 4; i++) 
		{
			transform.position += dir * 2;
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
