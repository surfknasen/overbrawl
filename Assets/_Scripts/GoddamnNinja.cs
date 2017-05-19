/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidalSamurai : Character {
 
    public SuicidalSamurai(int moveSpeed) : base(moveSpeed)
    {
			this.abilities = new Dictionary<KeyCode, Func<Boolean>>();
			abilities.Add(KeyCode.Q, Sepuku);
    }

    private bool Sepuku()
    {
		// Goddamn sepuku
		theHealthClassThing.TakeDamage(99999);
		return true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
*/