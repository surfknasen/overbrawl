using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnResources : NetworkBehaviour {

	private Vector2 spawnPosition;
	[SerializeField]
	private GameObject resourceObject;

	void Start () 
	{
		if(!isServer) return;
		for (int i = 0; i < 100; i++) {
			SpawningResources ();
		}
		StartCoroutine ("SpawningResourcesNumerator");
	}

	IEnumerator SpawningResourcesNumerator()
	{
		SpawningResources ();
		yield return new WaitForSeconds (1f);
		StartCoroutine ("SpawningResourcesNumerator");
	}

	void SpawningResources()
	{
		spawnPosition = new Vector2 (Random.Range(-100,100), Random.Range(-100, 100));
		GameObject g = Instantiate (resourceObject);
		g.transform.position = spawnPosition;

		Rigidbody2D r = g.GetComponent<Rigidbody2D> ();
		Vector2 randomDirection = new Vector2 (Random.value, Random.value);
		transform.Rotate (randomDirection);
		NetworkServer.Spawn (g);
	}

}
