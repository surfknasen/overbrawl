using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnHostileChest : NetworkBehaviour {

	public GameObject hostileChest;

	void Start () {
		
	}
	
	// Update is called once per frame
	[Command]
	public void Cmd_SpawnHostileChest(GameObject obj)
	{
		GameObject g = Instantiate(hostileChest, obj.transform.position, obj.transform.rotation);
		g.GetComponent<HostileChest>().StartCoroutine("BecomeHostile", gameObject);	
		Destroy(obj);
		Rpc_SpawnHostileChest(obj);
	}

	[ClientRpc]
	void Rpc_SpawnHostileChest(GameObject obj)
	{
		GameObject g = Instantiate(hostileChest, obj.transform.position, obj.transform.rotation);
		g.GetComponent<HostileChest>().StartCoroutine("BecomeHostile", gameObject);	
	}
}
