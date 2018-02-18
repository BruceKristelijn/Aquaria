using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*---------------------

	This script is here to manage the in-game UI. It will display info about the fishes and how much hunger they have.

---------------------*/
public class UiManager : MonoBehaviour {

	public GameObject currentUI;																				//Define the CurrentUI
	public GameObject[] uiComponents;																			//Define UI screens like the inventory and shopscreen. 

    public Shop shop;																							//Define the shopmanager to have a fast way of buting objects.

	public GameManager gameManager;																				//Define the gamemanager.

	public void goToScreen(int id){																				//Define the goToScreen() Function to show a screen from uiComponents[The given int called 'id'].
		if (id == 99) {																							//Check if the id is a special value to close all screens.
			currentUI.SetActive (false);																		//Turn off the current UI.
			currentUI = null;																					//And set it to null.
			return;																								//Stop the function to prevent unwanted behaviour.
		}
		if(currentUI != null){																					//Check if the currentUI is defined.
		currentUI.SetActive (false);																			//if so turn it off.
		}
		if (currentUI == uiComponents [id]) {																	//Check if the currentUI is the same as the chosen screen.
			currentUI.SetActive (false);																		//If so turn off currentUI.
			currentUI = null;																					//Set currentUI to null.
		} else {
			if (currentUI != null) {																			//If currentUI isn't null;
				currentUI.SetActive (false);																	//Turn current UI off
			}
			uiComponents [id].SetActive (true);																	//Turn on chosen UI.
			currentUI = uiComponents [id];																		//Redifine currentUI to match the chosen screen.
		}
	}
	public void Destroy(){																						//Define Destroy, A function to start or stop destorying (Picking up items) items.
		BuildingManager comp = GameObject.Find ("GridManager").GetComponent<BuildingManager> ();				//Find the scene's BuildingManager and define it to the Var comp.
		comp.destroying = !comp.destroying;																		//Turn the bool destorying to the opposite.
	}

	public void Update()																						//Do something every frame (fps).
	{
		DisplayFish();																							//Call the DisplayFish() Function.
		DisplayCurrency();																						//Call the DisplayCurrency() Function.
		DisplayFood ();																							//Call the DisplayFood() Function.
		DisplayHapiness ();																						//Call the DisplayHapiness() Function.
    }
    
	void DisplayFish()																							//A function to display the amount of fishes in the tank.
    {
        var fishes = GameObject.FindGameObjectsWithTag("Fish");													//Assaign an array of gameObjects to the variable fishes.
        GameObject.Find("fishAmount").GetComponent<Text>().text = fishes.Length.ToString();						//Turn the array length in to a string to display in the UI.
    }
    
	void DisplayCurrency()																						//A function to display the amount of "Fish coins" a user has.
    {
		//Find the currency displayer and get the amount of currency taken from the gamemanager to display.
		GameObject.Find ("currencyAmount").GetComponentInChildren<Text> ().text = gameManager.currency.ToString ();
    }

	void DisplayFood()																							//A function to display the amount of food the fishes have left to eat.
	{
		//Display the amount of food left in the tank. The script finds a gameobject called foodAmount and changes its textcomponent to display the amount of food left.
		GameObject.Find ("foodAmount").GetComponentInChildren<Text> ().text = gameManager.foodInTank + " - " + gameManager.maxFoodInTank;
	}

	void DisplayHapiness()																						//A function to display how happy fishes are.
	{
		//The script finds a gameobject called hapinessAmount and changes its textcomponent to display how happy the fish are.
		GameObject.Find ("hapinessAmount").GetComponentInChildren<Text> ().text = gameManager.tankHapinessBonus.ToString();
	}
	public void QuitGame(){																						//A function to quit the game.
		SceneManager.LoadScene (0);																				//Move the user back to the MainManu with buildindex: 0.
	}
}
