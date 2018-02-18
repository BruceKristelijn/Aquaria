using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	/*-----------------

	This script is for managing the aquariums item placement. it will instanciate a object to show feedback and where to place it. It will check the size of the object and .
	This script needs to be placed on a solo gameobject or can be shared with gridmanager.


	------------------*/

	public GameObject buildObject;
	public GameObject buildPrefab;

	public Building[] buildPrefabs;
	public Building chosenBuilding;

	public Material red;
	public Material green;
	public Material regular;

	public bool building;
	public bool destroying;

	public Inventory inv;
	public int buildid;
	public GridManager gridman;


	public void startBuilding(int id){
		if (GameObject.Find ("invcanvas").active) {
			GameObject.Find ("invcanvas").SetActive (false);
		}
		stopBuilding ();
		buildid = id;
		destroying = false;
		chosenBuilding = buildPrefabs [id];
        if (!building)
        {
			var prefab = buildPrefabs[id].prefab;
            var Object = Instantiate(prefab, buildObject.transform);
            buildPrefab = Object;
            building = true;
            setColor(1);
        }
        return;
	}
	public void stopBuilding(){
		building = false;
        if(buildPrefab != null)
        {
            Destroy(buildPrefab);
            buildPrefab = null;
        }
        return;
	}
	void build(GameObject cell){
		setColor (2);
		buildPrefab.transform.SetParent (cell.transform);
		cell.GetComponent<Cell> ().taken 		= true;
		cell.GetComponent<Cell> ().StartCoroutine("Bubbles");
        cell.GetComponent<Cell> ().childObject 	= buildPrefab;
		cell.GetComponent<Cell> ().childId 		= buildid;
        buildPrefab = null;
        building = false;
		inv.delItem (buildid);
		setTaken (cell.GetComponent<Cell>(), cell.GetComponent<Cell> ().coordinates);
	}

	public void setLocation(GameObject cell, bool taken){
		var pos = cell.transform.position;


		if (building) {
			buildObject.transform.position = pos;
			if (taken) {
                setColor(1);
            } else {
				if (checkLocation (cell)) {
					if (Input.GetMouseButtonDown (0)) {
						build (cell);
						return;
					} else {
						setColor (0);
					}
				} else {
					setColor(1);
				}
            }
		} else if(destroying){
			if (taken) {
				if (Input.GetMouseButtonDown (0)) {
					Cell obj = cell.GetComponent<Cell> ();
					if (obj.cells.Count > 0) {						
						for (int i = 0; i < obj.cells.Count; i++) {
							obj.cells [i].taken = false;
						}
					} else {
						obj.taken = false;
					}
					Destroy (obj.childObject);
					obj.childObject = null;
					Inventory inv = GameObject.Find ("Inventory").GetComponent<Inventory> ();
					inv.addItem (obj.childId);
				} else {
					Cell obj = cell.GetComponent<Cell> ();
					var comps = obj.childObject.GetComponentsInChildren<MeshRenderer> ();
					for (int i = 0; i < comps.Length; i++) {
						comps [i].material = red;
					}
				}
			}
		} else {
			return;
		}
	}
	bool checkLocation(GameObject cell){
		coordinate cellcoords = cell.GetComponent<Cell> ().coordinates;

		bool width = true;
		bool height = true;

		for (int i = 0; i < chosenBuilding.height; i++) {
			for (int j = 0; j < chosenBuilding.width; j++) {
				if (!checkCell (cellcoords.top+i, cellcoords.left+j)) {
					width = false;
				}
			}
			if (!checkCell (cellcoords.top+i, cellcoords.left)) {
				height = false;
			}
		}
		if (width && height) {
			return true;
		} else {
			return false;
		}
	}
	bool checkCell(int top, int left){
		var cells = GameObject.FindGameObjectsWithTag ("Ground");

		for (int i = 0; i < cells.Length; i++) {
			coordinate coord = cells [i].GetComponent<Cell> ().coordinates;
			Cell cell = cells [i].GetComponent<Cell> ();

			if (coord.left == left && coord.top == top) {
				if (cell.taken) {			
					return false;
				} else {
					return true;
				}
			} else if(top-1 > gridman.gridHeight || left-1 > gridman.gridWidth){
				return false;
			}
			cell = null;
			coord = null;
		}
		return true;
	}
	void setTaken(Cell cell, coordinate coord){
		if (chosenBuilding.width > 1 || chosenBuilding.height > 1) {
			for (int h = 0; h < chosenBuilding.height; h++) {				
				for (int w = 0; w < chosenBuilding.width; w++) {
					//Debug.Log ("height:" + h + "width:" + w);
					int top = h+coord.top;
					Debug.Log ("Top:"+top+"Left:"+(w+coord.left));
					cell.cells.Add (setCell (top, w + coord.left).GetComponent<Cell>());
				}	
			}

		}
	}
	GameObject setCell(int top, int left){
		var cells = GameObject.FindGameObjectsWithTag ("Ground");
		for (int i = 0; i < cells.Length; i++) {
			var cell = cells [i].GetComponent<Cell> ().coordinates;
			if (cell.top == top && cell.left == left) {
				cells [i].GetComponent<Cell> ().taken = true;
				return cells [i];
			}
		}
		return null;
	}
	void setColor(int way){
		if (way == 0) {
			MeshRenderer[] comps = buildObject.GetComponentsInChildren<MeshRenderer> ();
			for (int i = 0; i < comps.Length; i++) {
				comps [i].material = green;
			}
		} else if (way == 1) {
			MeshRenderer[] comps = buildObject.GetComponentsInChildren<MeshRenderer> ();
			for (int i = 0; i < comps.Length; i++) {
				comps [i].material = red;
			}
		} else if(way == 2){
			MeshRenderer[] comps = buildObject.GetComponentsInChildren<MeshRenderer> ();
			for (int i = 0; i < comps.Length; i++) {
				comps [i].material = regular;
			}
		}
		return;
	}
	void Update(){
		if (Input.GetMouseButtonDown (1)) {
			building = false;
			stopBuilding();
			destroying = false;
		}
	}
}
[System.Serializable]
public class Building{
	public GameObject prefab;
	public int width = 1;
	public int height = 1;
}