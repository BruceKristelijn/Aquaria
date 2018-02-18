using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

	/*-----------------

	This script is for managing every individual cell in the grid. It will make effects appear and can contain stats about the grid.



	------------------*/
	public GameObject manager;				//Reference the main game manager. This is to reference all other Components and scripts.

	public GameObject 	hoverCanvas;		//Canvas to activate on hover.
	public coordinate	coordinates;		//Int to remember where the cell is, coordinates

	public bool 		taken;				//Bool if the cell is taken.
	public GameObject 	childObject;		//The object places on the cell if it is taken.
	public int 			childId;
	public List<Cell>	cells;				//Other taken cells.

	public GameObject Bubbleparticle;		//The prefab to instanciate while building.
	public Material     palette;			//Save the color pallet as a material.
	BuildingManager 	buildingManager;	//The building manager pure for reference.

	void Start(){
		buildingManager = GameObject.Find ("GridManager").GetComponent<BuildingManager>();
	}

	public void OnMouseOver(){				//Called when mouse is on the cell.
		hoverCanvas.SetActive(true);
		buildingManager.setLocation (this.gameObject, taken);
	}
	public void OnMouseExit(){				//Called when mouse is on out of the cell.
		hoverCanvas.SetActive(false);
		setChildColor ();
	}
	public IEnumerator Bubbles(){			//Function to display a bubble effect, makes the aquarium feel more alive.
		while (taken) {						//Starts a while loop while the cell has an object on itself.
			int i = Random.Range (1, 20);	//RNG a number between 1 and 20.
			if (i == 19) {					//If the number is 19 it will create a bubble effect.
				//Instansiate the bubble particle and define it to a variable called obj.
				var obj = Instantiate (Bubbleparticle, this.transform.position, this.transform.rotation, this.transform);
				Destroy (obj, 3.0f);		//Destroy OBJ after 3 seconds.
			}
			//Wait one second between running again.
			yield return new WaitForSeconds(1);
		}
	}
	void setChildColor(){					//A function to make sure the child's color is set to the original.
		if (childObject != null) {			//Do nothing if the child object isn't set.
			var comps = childObject.GetComponentsInChildren<MeshRenderer> ();
			//Loop trough the array of mesh renderers.
			for (int i = 0; i < comps.Length; i++) {
				comps [i].material = palette;//Set each material to match the original.
			}
		}
	}
}
//Define a costum class called coordinate. 
[System.Serializable] public class coordinate{
	public int top;
	public int left;
}
