using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpawnPowerupChest : NetworkBehaviour {

	public Text broadcastText;
	public GameObject chest;
	private Animation broadcastAnim;
	private bool spawnedRecently = true;
	private int attemptSpawnDelay = 60;
	GameObject player;

	// every x seconds two random numbers are generated
	// if both are equal, broadcast a message that a powerup has been spawned
	// spawn and assign properties to the powerup


	// Notes:
	// - The powerup is a creature that gives you its powers
	// - The powerup will try to escape (difficulty varies)

	void Start () 
	{
		broadcastAnim = broadcastText.gameObject.GetComponent<Animation>();
		if(!isServer) return;
		StartCoroutine("SpawnOrNot");
	}

	IEnumerator SpawnOrNot () 
	{
		yield return new WaitForSeconds (attemptSpawnDelay);
		Random.InitState(System.Environment.TickCount);
		int num = Random.Range(0, 100);

		if(num < 25 && !spawnedRecently)
		{
			//DecidePowerup();
			SpawnChest();
			spawnedRecently = true;
		} else
		{
			spawnedRecently = false;
		}

		StartCoroutine("SpawnOrNot");
	}

	void SpawnChest()
	{
		Vector3 vector = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(vector, 40);		

		for(int i = 0; i < hitColliders.Length; i++)
		{
			if(hitColliders[i].gameObject.CompareTag("Player"))
			{
				SpawnChest();
				return;
			}
		}

		/*GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++)
		{
			if(players[i].GetComponent<NetworkIdentity>().isLocalPlayer)
			{
				player = players[i];
				break;
			}
		}*/

		GameObject obj = Instantiate(chest, vector, transform.rotation);
		NetworkServer.Spawn(obj);
		//Rpc_Broadcast();		

	}

/*	void WhereToSpawn()
	{
		GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
		int num = Random.Range(0, resources.Length);
		resources[num].GetComponent<MineResource>().specialDrop = true;
	}
	*/

	[ClientRpc]
	void Rpc_Broadcast()
	{
		broadcastText.text = "A power-up has spawned!";
		broadcastAnim.Play();		
	}


/* 
	// THE BELOW SHOULD BE DONE WHEN PICKED UP
	void DecidePowerup()
	{
		// ~4-6 different powerups
		int num = Random.Range(0, 100);

		if(num < 50) 
		{
			AssignProperties("Shield");
		} else 
		{
			AssignProperties("Ghost");
		}
	}



	void AssignProperties(string powerup)
	{
		// transform values etc
		GameObject powerupObj;
		Vector3 vector;

		switch(powerup)
		{
			case "Shield":
				vector = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
				powerupObj = Instantiate(powerupObject, vector, transform.rotation);
				powerupObj.GetComponent<SpriteRenderer>().color = Color.green; 
				powerupObj.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
			break;
			case "Ghost":
				vector = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
				powerupObj = Instantiate(powerupObject, vector, transform.rotation);
				powerupObj.GetComponent<SpriteRenderer>().color = Color.red; 
				powerupObj.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
			break;
		}
	}
	*/
}
