using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	/*-----------------

	This script is for managing the player's inventory. You can add and delete items for building and buying.
	This script needs to be placed on a solo gameobject.


	------------------*/

	public GameObject invSlot;							//Prefab for the inventory slots.
	public GameObject invParent;						//Parent to instaciate to.
	public List<inventorySlot> inventory;				//A list with all inventory items.
	public inventoryItems[] items;						//An array with set IDs to reference sprites.

	public BuildingManager buildmanager;

	void Start(){
		addItem (0);
		addItem (0);
		addItem (1);
		addItem (1);
	}

	public void addItem(int id){								//Script for adding items.
		int index = getIndex (id);
		if (index != 789) {
			//Add item to exicting inventory slot.
			inventory[index].amount++;
			var invtext = inventory [index].slot.transform.Find ("invText").GetComponent<Text> ();
			invtext.text = "Amount: "+(inventory[index].amount + 1).ToString ();
		} else {
			//Add item and new inventory slot.
			inventorySlot invItem = new inventorySlot(id, id.ToString (), 0);
			invItem.slot = Instantiate (invSlot, invParent.transform);
			invItem.slot.transform.Find ("invText").GetComponent<Text> ().text = "Amount: "+(0 + 1).ToString ();
			invItem.slot.transform.Find ("invImg").GetComponent<Image> ().sprite = items [id].image;
			invItem.slot.GetComponent<Button>().onClick.AddListener(delegate{buildmanager.startBuilding(id);});
			inventory.Add (invItem);
		}
	}
	public void delItem(int id){								//Script for removing items.
		int delindex = getIndex (id);
		if (delindex == 789) {
			return;
		}
		inventory [delindex].amount--;
		if (inventory [delindex].amount < 0) {
			Destroy (inventory [delindex].slot);
			inventory.RemoveAt (delindex);
		} else {
			var invtext = inventory [delindex].slot.transform.Find ("invText").GetComponent<Text> ();
			invtext.text = "Amount: "+(inventory[delindex].amount + 1).ToString ();
		}
	}

	int getIndex(int id){								//Script for getting the index of a object in a list
		if (inventory != null) {
			for (int i = 0; i < inventory.Count; i++) {
				if (inventory [i].id == id) {
					return i;
				}
			}
		} 
		return 789;										//Returning 789 will function as a false return. This will safe time.
	}

}
[System.Serializable]
public class inventorySlot{
	public int id;	
	public string name;
	public int amount;
	public GameObject slot;     
	public inventorySlot(int aid, string aname, int aamount)
	{
		id = aid;
		name = aname;
		amount = aamount;
	}
}
[System.Serializable]
public class inventoryItems{
	public string name;
	public Sprite image;
}