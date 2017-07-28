using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCanvasHandler : MonoBehaviour {

	public GameObject upgradeCanvasObject;
	public string activeClass;
	public List<GameObject> upgradeCanvases = new List<GameObject>();
	
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
