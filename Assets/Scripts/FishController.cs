using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

	[Header("Fish Stats")]
	public string fishName;
	public int fishHealth;
	public int age;
	public bool hostile;
	public float hunger = 100;
	public float happiness = 100;
	public waterType waterType;
	public int fishType;

	[Header("Movement Settings")]
	private float speed;
	public float maxSpeed;
	public float minSpeed;

	public float rotationSpeed;
	Vector3 averageHeading;
	Vector3 averagePosition;

	public bool turning = false;
	public Vector3 goPos;

	public GridManager gridManager;
	public GameManager gameManager;

	public bool doingAction;
	public int actionToDo;

	private int refreshRate;
	private float timeOver;

	private float secondClock;

	public Transform currentPointToSwim;

	void Start () {
		hunger = 100;
		fishHealth = 5;
		speed = Random.Range (minSpeed, maxSpeed);
		gridManager = GameObject.Find ("GridManager").GetComponent<GridManager>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		refreshRate = Random.Range (10, 31);
		timeOver = refreshRate;
	}
	

	void Update () {
		//Quick Test
		if (waterType != gameManager.waterType) {
			Destroy (this.gameObject);
		}

		//The Inside clock of the fish
		timeOver -= Time.deltaTime;
		if(timeOver <= 0){

			//Fish Points Generator
			gameManager.currency = gameManager.currency + (int)(happiness/7) + 1;

			//Food Timer
			if (Random.Range (0, 2) == 0) {
				hunger--;
			}

			if (gameManager.foodInTank > 0 && hunger < 100) {
				gameManager.foodInTank--;
				hunger++;
			}

			if (happiness <= 5) {
				fishHealth--;
			}

			if (happiness > 5) {
				fishHealth = 5;
			}

			if (fishHealth == 0) {
				Destroy (this.gameObject);
			}

			refreshRate = Random.Range (10, 31);
			timeOver = refreshRate;
		}

		//Second Clock for checkingMovement
		secondClock -= Time.deltaTime;
		if(secondClock <= 0){
			if (!doingAction) {
				actionToDo = Random.Range (0, 1000);
			}


			//Hapiness includes food, plants in tank and water quality
			happiness = ((0 + hunger) / 100) * gameManager.tankHapinessBonus;
			if (happiness > 100) {
				happiness = 100;
			}

			secondClock = 0.5f;
		}

		if(transform.position.x > gridManager.gridHeight - 1.5f || transform.position.x < 0.5f || transform.position.z > gridManager.gridWidth - 1.5f || transform.position.z < 0.5f) {
		//if (Vector3.Distance (transform.position, Vector3.zero) >= gridManager.gridWidth) {
			turning = true;
		} else {
			turning = false;
		}

		if (turning) {
			GenNewPos ();
			MoveToNewPos ();
		} else {
			//Has a 1 in 10 change to execute MoveToNewPos function
			if (!doingAction) {
				if (actionToDo < 200) {
					GenNewPos ();
					MoveToNewPos ();
				}
			}

			//Executes and action when one of these numbers
			if (actionToDo > 99 && actionToDo < 120) {
				doingAction = true;
				var obj = GameObject.Find ("SwimPoint1");
				if (obj != null) {
					currentPointToSwim = obj.transform;
				}
				if (currentPointToSwim != null) {
					goPos = new Vector3 (currentPointToSwim.position.x, currentPointToSwim.position.y, currentPointToSwim.position.z);
					MoveToNewPos ();
				} else {
					actionToDo = Random.Range (0, 1000);
					doingAction = false;
				}
			}
			if (actionToDo == 1000) {
				goPos = new Vector3 (currentPointToSwim.position.x, currentPointToSwim.position.y, currentPointToSwim.position.z);
				MoveToNewPos ();
			}

		}

		//Moves the fish forward
		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	//If the fish swims to the first point it wil swim to the second point and will follow his own movement again
	void OnTriggerEnter(Collider other){
		if (other.tag == "Spot" && doingAction && actionToDo > 99 && actionToDo < 120) {
			currentPointToSwim = currentPointToSwim.GetChild(0);
			actionToDo = 1000;
		}
		if (other.tag == "Spot2" && doingAction) {
			actionToDo = 0;
			doingAction = false;
			currentPointToSwim = null;
		}
	}

	//Generates a new position for the fish to move to
	void MoveToNewPos() {
		Vector3 direction = goPos - transform.position;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);

	}

	void GenNewPos() {
		goPos = new Vector3 (Random.Range (0.1f, gridManager.gridHeight), Random.Range (0.5f, 4), Random.Range (0.1f, gridManager.gridWidth));
	}
}