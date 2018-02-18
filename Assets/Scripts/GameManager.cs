using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public enum waterType {salt, normal, any };

public class GameManager : MonoBehaviour {

	public int maxFoodInTank;
	public int foodInTank;
	public int fishPoints;
    public int fishFood;
	public int tankHapinessBonus;
	private int tempHapiness;
    public waterType waterType;

	public int currency;

	public GameObject[] fishPrefab;

	public GridManager gridManager;

	public List<GameObject> plantsInAquarium;

	void Start () {
	}

	void Update () {
		var arr = GameObject.FindGameObjectsWithTag("Plant");														//Loops through the scene and checks for objects with the "Plant" Tag
		plantsInAquarium = arr.ToList();																			//Changes the array to an list for better usability

		tempHapiness = 0;																							//Resets the temp var for hapiness to 0

		for (int r = 0; r < arr.Length; r++) {																		//Starts a for loop
			tempHapiness = tempHapiness + plantsInAquarium[r].GetComponent<Plant>().hapinessBonus;					//Changes the temp hapiness to the hapiness of the plants combined
		}

		tankHapinessBonus = tempHapiness;																			//Applys the temp hapiness to the normal hapiness

	}

	public void SpawnFish(int FishID){																				//The function for spawning fish, !Requires and ID
		Instantiate (fishPrefab [FishID], new Vector3	(Random.Range (0.1f, gridManager.gridHeight),
														0.5f,Random.Range (0.1f, gridManager.gridWidth)),
														Quaternion.identity);
	}

	public void Feed(){
		foodInTank = 5;
	}

}
//hoi