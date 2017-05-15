using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAddCamera : NetworkBehaviour 
{
	public virtual void OnServerConnect(NetworkConnection conn)
	{
		if (isLocalPlayer) {
			return;
		}

		Camera.main.enabled = false;	
	}
	

}
