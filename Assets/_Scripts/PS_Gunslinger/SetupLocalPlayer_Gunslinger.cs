using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer_Gunslinger : NetworkBehaviour {

	public Image fillColor;
	public GameObject body;
	public Sprite thisSprite;
	public NetworkAnimator networkAnimatorGunslinger;
	private string activeClass = "Gunslinger";

	public void Start () // Setup the local player
	{
		if (isLocalPlayer) 
		{
			Cmd_SetupSprite();
			networkAnimatorGunslinger.SetParameterAutoSend(0,true);
			GetComponent<PlayerMovement> ().enabled = true;
			GetComponent<Gunslinger_Abilities> ().enabled = true;
			GetComponent<Health>().enabled = true;

			Health health = GetComponent<Health>();
			health.Cmd_ChangeMaxHealth(150);
			health.Cmd_ChangeCurrentHealth(150);
			health.Cmd_SetRegenerateProperties(10, 5);

			GetComponent<LevelHandler>().enabled = true; // ADDED RECENTLY 
			GetComponent<UpgradeCanvasHandler>().enabled = true; // ADDED RECENTLY
			GetComponent<LevelHandler>().activeClass = this.activeClass;
			GetComponent<UpgradeCanvasHandler>().activeClass = this.activeClass;
			fillColor.color = new Color32(0,255, 0, 255); // green
		}
	}

	public override void PreStartClient()
	{
		networkAnimatorGunslinger.SetParameterAutoSend(0,true);
	}

	/* 
	public override void OnStartClient() // so that the health text and health bar is correct for all clients and server
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++)
		{
			SetupLocalPlayer_Gunslinger gunslinger = players[i].GetComponent<SetupLocalPlayer_Gunslinger>();
			if(gunslinger != null)
			{
				Health health = players[i].GetComponent<Health>();
				health.healthText.text = 150 + " / " + 150;
				health.healthBar.maxValue = 150;
				health.healthBar.value = 150;
			} else
			{
				SetupLocalPlayer_Vanguard vanguard = players[i].GetComponent<SetupLocalPlayer_Vanguard>();
				if(vanguard != null)
				{
					Health health = players[i].GetComponent<Health>();
					health.healthText.text = 200 + " / " + 200;
					health.healthBar.maxValue = 200;
					health.healthBar.value = 200;
				}
				
			}
		}
	}
	*/
	

	void Update()
	{
		if(!isLocalPlayer) return;
		Cmd_SetupSprite();	
	}

	[Command]
	void Cmd_SetupSprite()
	{
		Rpc_SetupSprite();
	}

	[ClientRpc]
	void Rpc_SetupSprite()
	{
		body.SetActive(true);
		gameObject.GetComponent<SpriteRenderer>().sprite = thisSprite;
	}
}
