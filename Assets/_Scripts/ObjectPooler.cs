using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectPooler : NetworkBehaviour 
{
	public static ObjectPooler SharedInstance;

	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	void Awake()
	{
		SharedInstance = this;
	}

	void Start()
	{
		if(!isServer) return;

		pooledObjects = new List<GameObject>();
		for (int i = 0; i < amountToPool; i++) {
  			GameObject obj = (GameObject)Instantiate(objectToPool);
			obj.SetActive(false);
 			pooledObjects.Add(obj);
			NetworkServer.Spawn(obj);	
		}
	}
	
	public GameObject GetPooledObject()
	{
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}

		return null;
	}
}
