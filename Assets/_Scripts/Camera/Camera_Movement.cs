using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Camera_Movement : NetworkBehaviour {

	private Vector3 offset;
	private Vector3 velocity = Vector3.zero;
	private GameObject player;

	public void CameraStart(GameObject p) // the local player pass itself into this method 
	{
		player = p;
		offset = transform.position - player.transform.position;
	}
	
	void FixedUpdate () // late update causes jitter in the movement
	{
		if (player != null) {
			transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, 0.1f);
		//	transform.position = player.transform.position + offset;
		}
	}


}
