using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	/*-----------------

	This script is for managing the full grid. It will create the right amount of cells and have functions to call.



	------------------*/
	public GameObject manager;

	public int gridHeight;						//The height of the grid,
	public int gridWidth;						//The width of the grid,
	public float gridDiff = 1.1f;				//The difference between the cells.
	public GameObject gridPrefab;				//Prefab to instanciate.
	public List<GameObject> gridGameobjects;				//The list of the all the cells.
	public int[,] grid;

	void Start () {								//Called when script starts.
		for (int h = 0; h < gridHeight; h++) {
			for (int w = 0; w < gridWidth; w++) {
				var gridPlace = Instantiate(gridPrefab, new Vector3(gridDiff*h,0,gridDiff*w), this.transform.rotation, this.transform);
				gridGameobjects.Add (gridPlace);
				gridPlace.name = "Cell| Top: " + w + " Left: " + h;
				Cell comp = gridPlace.GetComponent<Cell> ();
				comp.coordinates.top = w;
				comp.coordinates.left = h;
			}
		}
	}
}