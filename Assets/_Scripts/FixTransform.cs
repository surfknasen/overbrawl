using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FixTransform : NetworkBehaviour 
{
	private static Quaternion rotation;
	private static Vector3 pos;

	void Start()
	{
		rotation = transform.rotation;
		pos = transform.localPosition;
	}

	void LateUpdate()
	{
		transform.rotation = rotation;
		transform.localPosition = pos;
	}


}
