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

	public void Start () 
	{
		if (isLocalPlayer) 
		{
			Cmd_SetupSprite();
			networkAnimatorGunslinger.SetParameterAutoSend(0,true);
			GetComponent<PlayerMovement> ().enabled = true;
			GetComponent<Gunslinger_Abilities> ().enabled = true;
			GetComponent<Health> ().enabled = true;
			GetComponent<Health>().maxHealth = 150;
			GetComponent<LevelHandler>().activeClass = this.activeClass;
			fillColor.color = new Color32(0,255, 0, 255); // green
		}

	}

	public override void PreStartClient()
	{
		networkAnimatorGunslinger.SetParameterAutoSend(0,true);
	}
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
