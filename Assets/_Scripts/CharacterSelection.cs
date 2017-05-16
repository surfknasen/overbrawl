﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterSelection : NetworkBehaviour {

	[SerializeField]
	private GameObject menuCanvas;
	private Camera_Movement cameraMovement;

	[SerializeField]
	private GameObject tracerPrefab;
	private SetupLocalPlayer_Gunslinger tracerSetup;

	void Start()
	{
		cameraMovement = Camera.main.GetComponent<Camera_Movement> ();
	}

	public void SelectedTracer()
	{
		menuCanvas.SetActive (false);
	}

}
