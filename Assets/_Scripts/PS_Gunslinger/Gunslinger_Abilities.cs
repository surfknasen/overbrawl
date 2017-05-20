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
	public int bulletDamage;

	void Start()
	{
		bulletDamage = 10;
	}

	void Update () 
	{
		if (Input.GetMouseButton (0) && !mouseOverPlayer) 
		{
			if(!shootingBullet)
			{
				StartCoroutine ("ShootBulletNumerator");
				Cmd_ShootBullet (GetMouseDirection());
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
		yield return new WaitForSeconds (0.2f);
		shootingBullet = false;
	}

	[Command]
	public void Cmd_ShootBullet(Vector3 dir)
	{
		for(int i = 0; i < 2; i++)
		{
			GameObject b = Instantiate (bullet, bulletSpawnPositions[i].transform.position, bullet.transform.rotation);	
			Rigidbody2D r = b.GetComponent<Rigidbody2D> ();
			r.velocity = dir * 30;
			NetworkServer.Spawn (b);
			b.GetComponent<Projectile>().SetProjectileOwner(gameObject);
			b.GetComponent<Projectile> ().damage = bulletDamage;
			Destroy (b, 0.7f);
		}
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
