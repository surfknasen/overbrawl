using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour 
{
	[SyncVar]
	public float moveSpeed;
	bool alreadyFrozen;

	void Start()
	{
		Camera_Movement cameraMovement = Camera.main.GetComponent<Camera_Movement> ();
		cameraMovement.CameraStart (gameObject);
	}

	[Command]
	public void Cmd_ChangeMoveSpeed(float newValue)
	{
		moveSpeed = newValue;
	}

	public IEnumerator FreezePlayer(GameObject playerToFreeze, float freezeDuration)
	{
		if(alreadyFrozen) yield break;
		alreadyFrozen = true;

		float revertSpeed = moveSpeed;
		moveSpeed = moveSpeed - moveSpeed * 0.30f;
		yield return new WaitForSeconds(freezeDuration);
		moveSpeed = revertSpeed;
		
		alreadyFrozen = false;
	}


	void FixedUpdate () 
	{
		MovePlayer ();
		RotatePlayer (GetMouseDirection());
	}

	
	void MovePlayer()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis ("Vertical");
		transform.position += new Vector3 (x, y, 0) * moveSpeed * Time.deltaTime;
	}

	void RotatePlayer(Vector3 dir) // -------------- LEARN MORE ABOUT THIS I HAVE HONESTLY NO IDEA WHAT IS GOING ON BUT IT WORKS FOR NOW 
	{
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.Rotate (0, 0, -90);
	}

	Vector3 GetMouseDirection()
	{
		Vector3 mousePosition = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 direction = (Input.mousePosition - mousePosition).normalized;
		return direction;
	}
		
}
