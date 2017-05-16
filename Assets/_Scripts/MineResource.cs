using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineResource : MonoBehaviour {

	[SerializeField]
	private Slider healthBar;

	void Start()
	{
		healthBar.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D otherCol)
	{
		IAttack iAttack = otherCol.gameObject.GetComponent<IAttack>();
		if(iAttack != null)
		{
			if(iAttack.isActive())
			{
				TakeDamage(iAttack.getDamage());
			}
				
		} 
		
	}
	
	void TakeDamage (int dmg) 
	{
		healthBar.gameObject.SetActive (true);
		healthBar.value -= dmg;
		if (healthBar.value > 0) return;
		Destroy (gameObject);
	}
}
