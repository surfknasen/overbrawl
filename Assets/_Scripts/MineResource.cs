using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MineResource : NetworkBehaviour {

	[SerializeField]
	private Slider healthBar;
	[SyncVar(hook = "OnChangeHealth")]
	private int currentHealth;
	public GameObject currency;
	
	void Start()
	{
		healthBar.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		IAttack iAttack = otherCol.gameObject.GetComponent<IAttack>();
		if(iAttack != null)
		{
			if(iAttack.isActive())
			{
				TakeDamage(iAttack.getDamage());
			}
		} 
	}
	
	void TakeDamage (int dmg) 
	{
		healthBar.gameObject.SetActive (true);
		healthBar.value -= dmg;
		currentHealth = (int)healthBar.value;

		if (healthBar.value > 0) return;

		DropCurrency();
		
	}

	void OnChangeHealth(int health)
	{
		healthBar.value = health;
	}

	void DropCurrency()
	{
		for(int i = 0; i < 3; i++) // comment the shit below
		{
			Vector3 spawnBox = transform.localScale;
			Vector3 position = new Vector3(Random.value * spawnBox.x, Random.value * spawnBox.y, Random.value * spawnBox.z);
			position = transform.TransformPoint(position-spawnBox/2);
			GameObject cur = Instantiate(currency, position,transform.rotation);
			NetworkServer.Spawn(cur);
		}
		Destroy(gameObject);
	}
}


