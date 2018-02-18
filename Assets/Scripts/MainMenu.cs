using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*---------------------

	This script is here to manage the main menu. It will redict to the scene wich actually plays the game and makes the music player behave.

---------------------*/
public class MainMenu : MonoBehaviour {

	public GameObject musicPlayer;								//Define the musicplayer.

	void Start(){												//Called when script starts.
		PlayMusic ();											//Start the PlayMusic Script.
	}
	void PlayMusic(){											//Define PlayMusic function. This is used to play music.
		var obj = GameObject.Find ("MUSICPLAYER_DONTDESTROY");	//Find a gameobject called "MUSICPLAYER_DONTDESTROY" and define it to a variable called OBJ.
		if (obj) {												//Check if the OBJ is set.
			Destroy (obj);										//Destroy the OBJ.
		}
		DontDestroyOnLoad(musicPlayer);							//Make it so the music player doesn't get destoryed while loading.
		musicPlayer.name = "MUSICPLAYER_DONTDESTROY";			//Reset the name for easy finding.
	}
	public void Quit(){											//Define the Quit function for button presses.
		Application.Quit ();									//Quit the application.
	}
	public void Startgame(){									//Define the Startgame function for button presses.
		SceneManager.LoadScene (1);								//Load the main game scene.
	}
}
