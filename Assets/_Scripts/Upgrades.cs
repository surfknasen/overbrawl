using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Upgrades : NetworkBehaviour 
{
	public GameObject upgradeShopCanvas;
	private GameObject player;
	private Health health;
	private LevelHandler currencyHandler;
	public string activeClass;

	void Start()
	{
		StartCoroutine("LateStart");
	}

	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.1f);
		player = GameObject.FindGameObjectWithTag("Player");
		health = player.GetComponent<Health>();
		currencyHandler = player.GetComponent<LevelHandler>();
	}

	
}
