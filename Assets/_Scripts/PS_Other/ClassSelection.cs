﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ClassSelection : NetworkBehaviour 
{
	 
	string selectedClassName;
	GameObject[] player;
	bool localPlayerFound;
	public GameObject classCanvas;

	public void SelectClass (string className) 
	{
		selectedClassName = className;
		classCanvas.SetActive(false);
	}

	void Update()
	{
		if(localPlayerFound) return;
		player = GameObject.FindGameObjectsWithTag("Player");
		if(player.Length > 0)
		{
			GetLocalPlayer();
		}
	}

	void GetLocalPlayer()
	{
		for(int i = 0; i < player.Length; i++)
		{
			if(player[i].GetComponent<NetworkIdentity>().isLocalPlayer)
			{
				ConfigurePlayer(player[i]);
				localPlayerFound = true;
				return;
			}
		}
	}

	void ConfigurePlayer(GameObject p)
	{
		switch(selectedClassName)
		{
			case "Tracer":
			p.GetComponent<SetupLocalPlayer_Gunslinger>().enabled = true;
			p.GetComponent<Gunslinger_Abilities>().enabled = true;
			break;
			case "Reinhardt":
			p.GetComponent<SetupLocalPlayer_Vanguard>().enabled = true;
			p.GetComponent<Vanguard_Abilities>().enabled = true;
			break;
		}
	}
	
}

// Player selects a class
// When you host or join the correct scripts and sprite will be added on the player