using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishShop : MonoBehaviour {

	/*-------------------------
	 * 
	 Script for managing the purchaes of fishes.
	 
	 ------------------------*/
	
	public GameManager 	manager;
	public Transform 	shopPanel;
	public GameObject   shopPlate;
	public Fish[]		fishes; 

	void Start(){
		createPlates ();
	}

	void createPlates(){
		for (int i = 0; i < fishes.Length; i++) {
			var id = i;
			var fish = fishes [i];

			var plate = Instantiate (shopPlate, shopPanel);

			Image icon		= plate.transform.Find ("FishImage").GetComponent<Image> ();
			Text name 		= plate.transform.Find ("Name").GetComponent<Text> ();
			Text cost 		= plate.transform.Find ("Cost").GetComponent<Text> ();
			Button website 	= plate.transform.Find ("Website").GetComponent<Button> ();
			Button buy 	   	= plate.transform.Find ("Buy").GetComponent<Button> ();

			name.text = fish.name;
			cost.text = fish.cost.ToString();

			icon.sprite = fish.sprite;

			website.onClick.AddListener (delegate {
				Application.OpenURL (fish.url);
			});
			buy.onClick.AddListener (delegate {
				buyFish ((id));
			});
		}
	}

	bool buyFish(int id){
		var money = manager.currency; 
		var cost = fishes[id].cost;
		if(money >= cost){
			manager.currency -= cost;
			manager.SpawnFish (id);
			return true;
		} else {
			return false;
		}
	}
 

	
}
[System.Serializable]
public class Fish{
	public string 	name;
	public int 		cost;
	public Sprite 	sprite;
	public string 	url;
}