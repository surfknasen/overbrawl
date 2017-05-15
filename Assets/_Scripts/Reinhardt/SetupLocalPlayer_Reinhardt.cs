using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer_Reinhardt : NetworkBehaviour {

	public Image fillColor;
	public GameObject body;
	public Sprite thisSprite;

	public void Start () 
	{
		if (isLocalPlayer) 
		{
			Cmd_SetupSprite();
			GetComponent<PlayerMovement> ().enabled = true;
			GetComponent<Reinhardt_Abilities> ().enabled = true;
			GetComponent<Health> ().enabled = true;
			fillColor.color = new Color32(0,255, 0, 255); // green
		}

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
