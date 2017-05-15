using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Reinhardt_Abilities : NetworkBehaviour 
{
	private bool buttonPressed;
	public Animator swordAttackAnim;
	private bool teleporting;
	public GameObject reinhardtProjectile;
	private bool projectileShooting;

	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			Cmd_SwordAttack();
		}
		else if(Input.GetMouseButton(1) && !projectileShooting)
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
		swordAttackAnim.SetTrigger("Attack");
		swordAttackAnim.ResetTrigger("Attack");
	}
 
	 public bool AnimatorIsPlaying()
	 {
         return swordAttackAnim.GetCurrentAnimatorStateInfo(0).length >
                swordAttackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
     }
	
	IEnumerator ProjectileCooldown()
	{
		projectileShooting = true;
		yield return new WaitForSeconds(6f);
		projectileShooting = false;
	}

	[Command]
	public void Cmd_ShootProjectile(Vector3 dir)
	{
		GameObject p = Instantiate (reinhardtProjectile, transform.position, transform.rotation);
		Rigidbody2D r = p.GetComponent<Rigidbody2D> ();
		r.velocity = dir * 5;
		p.GetComponent<Projectile> ().ProjectileOwner (gameObject);
		p.GetComponent<Projectile> ().projectileDmg = 30;
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
