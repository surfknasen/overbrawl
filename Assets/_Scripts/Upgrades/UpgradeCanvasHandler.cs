using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCanvasHandler : MonoBehaviour {

	public GameObject upgradeCanvasObject;
	public string activeClass;
	public List<GameObject> upgradeCanvases = new List<GameObject>();

	public Dictionary<string, int> tiers = new Dictionary<string, int>() //-------// Tiers to limit some upgrades
	{
		{"IncreaseMoveSpeed", 1},
		{"IncreaseDamage", 1},
		{"IncreaseAttackSpeed", 1},
		{"IncreaseHealth", 1},
		{"IncreaseHealthRegen", 1},
		{"Freeze", 1},
		{"LifeSteal", 1},
		{"Poison", 1},
		{"IncreaseRange", 1},
		{"CriticalHit", 1}
	};
	
	public void CreateUpgradeCanvas () 
	{
		if(upgradeCanvases.Count > 0 )
		{
			GameObject newUpgradeCanvas = Instantiate(upgradeCanvasObject, transform.parent = transform);
			upgradeCanvases.Add(newUpgradeCanvas);			
			newUpgradeCanvas.SetActive(false);
			newUpgradeCanvas.GetComponent<NewUpgrades>().activeClass = activeClass;
		} else
		{
			GameObject newUpgradeCanvas = Instantiate(upgradeCanvasObject, transform.parent = transform);
			upgradeCanvases.Add(newUpgradeCanvas);			
			newUpgradeCanvas.SetActive(true);
			newUpgradeCanvas.GetComponent<NewUpgrades>().activeClass = activeClass;
		}
	}

	public void RemoveLatestUpgradeCanvas(GameObject toRemove)
	{
		upgradeCanvases.Remove(toRemove);
		if(upgradeCanvases.Count == 0) return;
		upgradeCanvases[upgradeCanvases.Count-1].SetActive(true);

	}
}
