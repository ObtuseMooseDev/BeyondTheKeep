using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject ClickedSound;
	public int newGameWait;
	public bool attacked = false;
	private GameController gameController;
	private PracticeMode practiceMode;
	private GameSettings gameSettings;
	public int health, attack;
	private SpriteRenderer myRenderer;
	public Sprite pressed;
	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
			practiceMode = gameControllerObject.GetComponent<PracticeMode>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

		GameObject gameSettingsObject = GameObject.FindWithTag ("GameSettings");
		if (gameSettingsObject != null) {
			gameSettings = gameSettingsObject.GetComponent<GameSettings>();
			Debug.Log (gameSettings.stuff);
		}

	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Touch" && ( tag == "Pause" || tag == "GameController" ) ) {
			//gameController.triggerPause = true;
			//Time.timeScale = 0.000000001f;
			//Destroy(other.gameObject);
			return;
		} else if(other.tag == "Touch" && tag == "Resume" ){
			gameController.paused = gameController.togglePause();
			//Time.timeScale = 1f;
			gameController.menuUp = false;
			//gameController.paused = false; 
			Destroy(transform.parent.gameObject);
			return;
		} else if(other.tag == "Touch" && tag == "Restart" ){
			//Time.timeScale = 1f;
			gameController.triggerPause = true;
			gameController.menuUp = false;
			//gameController.paused = false;
			Application.LoadLevel (Application.loadedLevel);
			Destroy(transform.parent.gameObject);
			return;
		} else if(other.tag == "Touch" && tag == "Quit" ){
			Time.timeScale = 1f;
			gameController.paused = false;
			Application.LoadLevel (0);
			return;
		} else if(other.tag == "Touch" && tag == "Start" ){
			Instantiate (ClickedSound, transform.position, transform.rotation);
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				myRenderer = renderer;
			}
			myRenderer.sprite = pressed;
			Application.LoadLevel (1);
		} else if(other.tag == "Touch" && tag == "Store" ){
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				myRenderer = renderer;
			}
			myRenderer.sprite = pressed;
			Debug.Log (gameSettings.unlockAll);
			Application.LoadLevel (2);
		} else if(other.tag == "Touch" && tag == "Customize" ){
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				myRenderer = renderer;
			}
			myRenderer.sprite = pressed;
			//Application.LoadLevel (3);
		} else if(other.tag == "Touch" && tag == "Practice" ){
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				myRenderer = renderer;
			}
			myRenderer.sprite = pressed;
			Application.LoadLevel (3);
		} else if(other.tag == "Touch" && (tag == "MinionTowers" || tag == "MenuStyles" || tag == "Backgrounds" ) ){
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				if(renderer.gameObject.tag == tag){
					myRenderer = renderer;
				}
			}
			myRenderer.sprite = pressed;
			gameObject.GetComponentInChildren<ChangeDisplay>().change = true;
		} else if(other.tag == "Touch" && ( tag == "P1Wins" || tag == "P2Wins" ) ){
			//WaitForSeconds (newGameWait);
			Application.LoadLevel (Application.loadedLevel);
		} else if ( other.gameObject.tag == "P1Wins" || other.gameObject.tag == "P2Wins" || tag == "P1Wins" || tag == "P2Wins" ){
			return;
		}
		if ((tag == "Team1Minion" && other.tag == "Team2Minion") || (tag == "Team2Minion" && other.tag == "Team1Minion")) {
						Instantiate (explosion, transform.position, transform.rotation);
						if (attacked == false) {
								
								if (gameObject.GetComponent<MinionController> ().minionType == 1 && other.gameObject.GetComponent<MinionController> ().minionType == 2) {
										other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (2 * attack);
										health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack / 2);
								} else if (gameObject.GetComponent<MinionController> ().minionType == 2 && other.gameObject.GetComponent<MinionController> ().minionType == 1) {
										other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (attack / 2);
										health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack * 2);
								} else if (gameObject.GetComponent<MinionController> ().minionType == 3 && other.gameObject.GetComponent<MinionController> ().minionType == 1) {
										other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (2 * attack);
										health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack / 2);
								} else if (gameObject.GetComponent<MinionController> ().minionType == 1 && other.gameObject.GetComponent<MinionController> ().minionType == 3) {
										other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (attack / 2);
										health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack * 2);
								} else if (gameObject.GetComponent<MinionController> ().minionType == 2 && other.gameObject.GetComponent<MinionController> ().minionType == 3) {
										other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (2 * attack);
										health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack / 2);
								} else if (gameObject.GetComponent<MinionController> ().minionType == 3 && other.gameObject.GetComponent<MinionController> ().minionType == 2) {
										other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (attack / 2);
										health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack * 2);
								} else if (gameObject.GetComponent<MinionController> ().minionType == other.gameObject.GetComponent<MinionController> ().minionType){
										other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (attack);
										health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack);
								} else if ( gameObject.GetComponent<MinionController> ().minionType == 4 ){
									other.gameObject.GetComponent<MinionController>().poisoned = true;
									other.gameObject.GetComponent<DestroyByContact> ().health -= attack;
									health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack);
								} else if ( other.gameObject.GetComponent<MinionController> ().minionType == 4 ){
									GetComponent<MinionController>().poisoned = true;
									health = health - (other.gameObject.GetComponent<DestroyByContact> ().attack);
									other.gameObject.GetComponent<DestroyByContact> ().health = other.gameObject.GetComponent<DestroyByContact> ().health - (attack);
								}

								other.gameObject.GetComponent<DestroyByContact> ().attacked = true;

								if (other.GetComponent<DestroyByContact> ().health <= 0) {
										if(other.gameObject.GetComponent<MinionController>().team == 1 && !gameController.isPractice){
											gameController.team1Cash = gameController.team1Cash + 5;
								} else if (other.gameObject.GetComponent<MinionController>().team == 2 && !gameController.isPractice){
											gameController.team2Cash = gameController.team2Cash +  5;
										}
										Destroy (other.gameObject);
					           }
								if (health <= 0) {
										if(gameObject.GetComponent<MinionController>().team == 1 && !gameController.isPractice){
											gameController.team1Cash = gameController.team1Cash + 5;
										} else if (gameObject.GetComponent<MinionController>().team == 2 && !gameController.isPractice){
											gameController.team2Cash = gameController.team2Cash + 5;
										}
										Destroy (gameObject);
								}
						}
						attacked = false;
						return;
		
				}
		if ( other.tag == "Boundary" || other.tag == "Player" || other.tag == "SpawnTower" || other.tag == "BoostTower" ) {
			return;
		}

		if (other.tag == "PlayerBase" && tag != "Bolt") {
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			if(tag == "Team1Minion"){ gameController.team2Health = gameController.team2Health - attack; }
			else if (tag == "Team2Minion"){ gameController.team1Health = gameController.team1Health - attack; }
			Destroy(gameObject);
		}

		if (other.tag == "Bolt") {
			Debug.Log ("Bolt");
			health--;
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy(other.gameObject);
			if(health <= 0){
				if( gameController.practiceMode ){
					if( practiceMode.currentLevel == 6 ){
						practiceMode.killCount++;
					}
				}

				if (gameObject.GetComponent<MinionController>().team == 1){
					gameController.team1Cash = gameController.team1Cash +  5;
				} else if (gameObject.GetComponent<MinionController>().team == 2){
					gameController.team2Cash = gameController.team2Cash +  5;
				}
				Destroy (gameObject);
				Instantiate (explosion, other.transform.position, other.transform.rotation);
			}
		}
	}
}
