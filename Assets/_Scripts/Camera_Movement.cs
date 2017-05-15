using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Camera_Movement : NetworkBehaviour {

	private Vector3 offset;
	private GameObject player;

	public void CameraStart(GameObject p) // the local player pass itself into this method 
	{
		player = p;
		offset = transform.position - player.transform.position;
	}
	
	void LateUpdate () 
	{
		if (player != null) {
			transform.position = player.transform.position + offset;
		}
	}


}
