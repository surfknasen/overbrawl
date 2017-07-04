using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour 
{
	public float moveSpeed;

	void Start()
	{
		Camera_Movement cameraMovement = Camera.main.GetComponent<Camera_Movement> ();
		cameraMovement.CameraStart (gameObject);
	}

	void FixedUpdate () 
	{
		MovePlayer ();
		RotatePlayer (GetMouseDirection());
	}

	void MovePlayer()
	{
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		float y = Input.GetAxis ("Vertical") * Time.deltaTime * moveSpeed;
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
