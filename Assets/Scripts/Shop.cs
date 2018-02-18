using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
	/*-----------------

		This script is here to manage the the buying of items.

	 * --------------*/
	public GameManager gameManager;																				//Reference to the gamemanager

	public shopitem[] items;																					//An array filled with all the items to sell in the store.

	public GameObject shopPanel;																				//Reference to the shopPanel.
	public Transform shopParent;																				//Reference to the shopParent.
	public Inventory inv;																						//Reference to the inv parent.

	void Start(){																								//Called when the script starts.
		for (int i = 0; i < items.Length; i++) {
			var obj = Instantiate (shopPanel, shopParent);														//Instanciate object.
			Text itemCost = obj.transform.Find ("itemCost").GetComponent<Text> ();								
			Image itemImg = obj.transform.Find ("itemimg").GetComponent<Image> ();
			Button itemButton = obj.GetComponent<Button> ();
			int id = items [i].invid;
			int cost = items [i].cost;
			itemCost.text = items [i].cost.ToString()+",-";
			itemImg.sprite = items [i].image;
			//Set the onclick listener to buy an item.
			itemButton.onClick.AddListener(delegate{BuyItem(id, cost);});
		}
	}
	//A function for buying items. It suptrackts the cost from the currency and adds the item to your inventory.
	public void BuyItem(int id, int cost){
		if (cost > gameManager.currency) {

		} else {
			//Buy it.
			gameManager.currency -= cost;
			inv.addItem (id);
		}
	}
}
[System.Serializable]
public class shopitem{
	public string name;
	public Sprite image;
	public int invid;
	public int cost;
}