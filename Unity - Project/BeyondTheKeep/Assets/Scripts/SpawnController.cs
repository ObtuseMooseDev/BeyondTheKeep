using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	public GameObject waterLightMinion, fireLightMinion, earthLightMinion, poisonMinion;
	public GameObject waterMedMinion, fireMedMinion, earthMedMinion;
	public GameObject waterHeavyMinion, fireHeavyMinion, earthHeavyMinion;
	public GameObject minion;
	private Vector3 spawnValues;


	public bool change = false;

	public int minionType; // 1 = Blue, 2 = Red, 3 = Green //
	public int minionCount; // 1 - 3 //
	public int minionLevel; // 1, 2, 3 // 

	public int team;

	public GameController gameController;
	public PracticeMode practiceMode;

	public float spawnWait;
	public float waveWait;
	public float startWait;

	// Use this for initialization
	void Start () {
		//minion = null;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
			if( gameController.isPractice){
				practiceMode = gameControllerObject.GetComponent<PracticeMode>();
			}
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		minion = null;
		spawnValues.z = rigidbody.position.z; 
		if (transform.position.x < 0) { 
			team = 2;
		} else { team = 1; }
		if (team == 1)
			spawnValues.x = rigidbody.position.x - 2;
		else if(team == 2){
			spawnValues.x = rigidbody.position.x + 2;
		}
		StartCoroutine ("SpawnWaves");

	}
	
	// Update is called once per frame
	void Update () {
		if( gameController.isPractice ){
			if (practiceMode.currentLevel == 6 && team == 1) {
				Destroy(gameObject);
			}

		}
		if (minionLevel == 0 && change == true) {
			StopCoroutine("SpawnWaves");
			change = false;
		}
		else if( minionLevel == 1) {
			if (minionType == 1 && change == true) {
				minion = waterLightMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			} else if ( minionType == 2 && change == true ){
				minion = fireLightMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			} else if ( minionType == 3 && change == true ){
				minion = earthLightMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			} else if ( minionType == 4 && change == true ){
				minion = poisonMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			}
		} else if( minionLevel == 2) {
			if (minionType == 1 && change == true) {
				minion = waterMedMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			} else if ( minionType == 2 && change == true ){
				minion = fireMedMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			} else if ( minionType == 3 && change == true ){
				minion = earthMedMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			}
		} else if( minionLevel == 3) {
			if (minionType == 1 && change == true) {
				minion = waterHeavyMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			} else if ( minionType == 2 && change == true ){
				minion = fireHeavyMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			} else if ( minionType == 3 && change == true ){
				minion = earthHeavyMinion;
				StopCoroutine("SpawnWaves");
				StartCoroutine ("SpawnWaves");
				change = false;
			}
		}

	}

	IEnumerator SpawnWaves(){
		if (minion != null) {  
						yield return new WaitForSeconds (startWait);
						while (true) {
								for (int i = 0; i < minionCount; i++) {
										Vector3 spawnPosition = new Vector3 (spawnValues.x, spawnValues.y, spawnValues.z);
										Quaternion spawnRotation = Quaternion.identity;
										//Quaternion towerRotation = Quaternion.identity;
										if (team == 1)
												minion.tag = "Team1Minion";
										else if (team == 2)
												minion.tag = "Team2Minion";
										else
												Debug.Log ("No Team Select");

										if (team == 2) {
												spawnRotation.Set (0, -180, 0, 0);
										} else {
												spawnRotation.Set (0, 0, 0, 0);
										}
										minion.GetComponent<MinionController> ().minionType = minionType;
										minion.GetComponent<MinionController> ().minionLevel = minionLevel;
										//minion.GetComponent<Mover>().speed = minion.GetComponent<Mover>().speed - minionLevel;
										Instantiate (minion, spawnPosition, spawnRotation);
					
										yield return new WaitForSeconds (spawnWait);
								}
								yield return new WaitForSeconds (waveWait);
						}
				}
	}

}
