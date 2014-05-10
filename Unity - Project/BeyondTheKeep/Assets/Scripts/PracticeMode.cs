using UnityEngine;
using System.Collections;

public class PracticeMode : MonoBehaviour {
	private GameController gameController;
	private SpawnController spawnTower;
	public int currentLevel;
	public bool changeLevel, gameOver;
	private int delay;

	public GameObject attackTower;
	public GameObject boostTower;
	private Quaternion towerRotation;
	private Vector3 towerPosition;


	public GameObject spawners;
	private Quaternion spawnRotation;
	private Vector3 spawnPosition;

	public GameObject tut1, tut2, tut3, tut4, tut5, tut6, tut7;
	public GameObject glowyBits;
	private Quaternion tutRotation;
	private Vector3 tutPosition;

	public int killCount;
	
	public Texture tutL01Health, tutL01Cash, tutL01Spawn, tutL01Goal;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}


		GameObject spawnTowerObject = GameObject.FindWithTag ("SpawnTower");
		if (spawnTowerObject != null) {
			spawnTower = spawnTowerObject.GetComponent<SpawnController>();
		}
		tutRotation = Quaternion.identity;
		tutPosition = new Vector3(0,0,0);
		delay = 0;
		gameOver = false;
		changeLevel = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (changeLevel) {
			nextLevel();
			changeLevel = false;
			if (currentLevel == 1) {
				Instantiate(tut1, tutPosition, tutRotation);
			}
			if (currentLevel == 2) {
				Instantiate(tut2, tutPosition, tutRotation);
			}
			if (currentLevel == 3) {
				Instantiate(tut3, tutPosition, tutRotation);
			}
			if (currentLevel == 4) {
				Instantiate(tut4, tutPosition, tutRotation);
			}
			if (currentLevel == 5) {
				Instantiate(tut5, tutPosition, tutRotation);
				Instantiate(glowyBits, tutPosition, tutRotation);
			}
			if (currentLevel == 6) {
				Instantiate(tut6, tutPosition, tutRotation);
				Instantiate(glowyBits, tutPosition, tutRotation);
			} 
			if (currentLevel == 7) {
				Instantiate(tut7, tutPosition, tutRotation);
				//Instantiate(glowyBits, tutPosition, tutRotation);
			}
		}


		checkLevelStatus (currentLevel);


	}
	void nextLevel(){
		currentLevel++;
		gameController.paused = gameController.togglePause ();

		if( currentLevel == 0) { Debug.Log ("Error: Current Level Invalid: " + currentLevel + " level"); }
		if (currentLevel == 1) {
			gameController.team2Cash = 15;
			gameController.team1Health = 100000;
			gameController.team2Health = 2;
			spawnTower.change = true;
		} else if ( currentLevel == 2 ) {
			gameController.team2Cash = 15;
			gameController.team1Health = 10000;
			gameController.team2Health = 2;
			spawnTower.change = true;
			spawnTower.minionType = 2;
			spawnTower.minionCount = 1;
		} else if ( currentLevel == 3 ) {
			gameController.team2Cash = 30;
			gameController.team1Health = 100000;
			gameController.team2Health = 4;
			spawnTower.change = true;
			spawnTower.minionType = 2;
			spawnTower.minionCount = 2;
		} else if ( currentLevel == 4 ) {
			gameController.team2Cash = 60;
			gameController.team1Health = 100000;
			gameController.team2Health = 6;
			spawnTower.change = true;
			spawnTower.minionType = 3;
			spawnTower.minionCount = 3;
			spawnTower.minionLevel = 2;
		} else if ( currentLevel == 5 ) {
			gameController.team2Cash = 50;
			gameController.team1Health = 100000;
			gameController.team2Health = 10;
			spawnTower.change = true;
			spawnTower.minionType = 1;
			spawnTower.minionCount = 3;
			spawnTower.minionLevel = 1;

			towerPosition.x = -2.69f;
			towerPosition.y = 0;
			towerPosition.z = -2;
			towerRotation = Quaternion.identity;
			Instantiate(attackTower, towerPosition, towerRotation);
			towerPosition.z = 2;
			Instantiate(attackTower, towerPosition, towerRotation);

			spawnPosition.x = 2.69f;
			spawnPosition.y = 0;
			spawnPosition.z = -2;
			spawnRotation = Quaternion.identity;
			Instantiate(spawners, spawnPosition, spawnRotation);
			spawnPosition.z = 2;
			Instantiate(spawners,spawnPosition, spawnRotation);

			towerPosition.x = 14.18f;
			towerPosition.z = 0;
			Instantiate (spawnTower, towerPosition, towerRotation);



		}else if ( currentLevel == 6 ) {
			gameController.team2Cash = 25;
			gameController.team1Health = 100000;
			gameController.team2Health = 10;
			spawnTower.change = true;
			spawnTower.minionType = 2;
			spawnTower.minionCount = 3;
			spawnTower.minionLevel = 1;

			towerPosition.x = -10.96f;
			towerPosition.z = -2.28f;
			Instantiate ( boostTower, towerPosition, towerRotation );
			towerPosition.z = 2.28f;
			Instantiate ( boostTower, towerPosition, towerRotation );
		} 

		else if ( currentLevel == 7 ) {
			gameController.team2Cash = 60;
			gameController.team1Health = 100000;
			gameController.team2Health = 10;

		}

	}
	void checkLevelStatus( int level ) {
		if (level == 1) {
			if( gameController.team2Health <= 0){
				changeLevel = true;
			}
		} else if (level == 2) {
			if( gameController.team2Health <= 0){
				changeLevel = true;

			} else if ( gameController.team2Cash == 0){
				gameController.team2Cash += 15;
			}
		} else if (level == 3) {
			if( gameController.team2Health <= 0){
				changeLevel = true;
			} else if ( gameController.team2Cash == 0){
				gameController.team2Cash += 30;
			}
		} else if (level == 4 ){
			if( gameController.team2Health <= 0){
				changeLevel = true;
			} else if( gameController.team2Cash > 60){
				gameController.team2Cash = 60;
			} else if ( gameController.team2Cash <= 30){
				gameController.team2Cash = 60;
				spawnTower.change = true;
				spawnTower.minionCount = 3;
				spawnTower.minionLevel = 3;
			}
		}
		else if(level == 5){
			if( gameController.team2Health <= 0){
				changeLevel = true;
			} else if ( gameController.team2Cash == 0 ){
				gameController.team2Cash += 50;
			}
		}
		else if (level == 6 ){
			if( killCount >= 12){
				changeLevel = true;
			} else if ( gameController.team2Cash == 0 ){
				gameController.team2Cash += 25;
			}

		} else if (level == 7 ){

			
		}
	}
}
