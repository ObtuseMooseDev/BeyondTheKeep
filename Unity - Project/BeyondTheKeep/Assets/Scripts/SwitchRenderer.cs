using UnityEngine;
using System.Collections;

public class SwitchRenderer : MonoBehaviour {
	//public
	private Vector3 towerPosition;
	public GameObject attackTower, boostTower;
	public Renderer[] renderers;
	public GameController gameController;
	public int towerID, team; // id 1 = attack, id 2 = boost, id 3 = cancel

	public GameObject NoCash;
	void Start(){
		if (transform.position.x > 0) {
			team = 2;
		} else {
			team = 1;
		}
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	void update(){
		if (transform.parent.GetComponent<TowerTouchTrigger2> ().towerPlaced == true) {
			gameObject.renderer.enabled = false;
			gameObject.collider.enabled = false;
		}
	}
	void OnTriggerStay(Collider other){
		if (other.tag == "Touch" && transform.parent.GetComponent<TowerTouchTrigger2>().towerPlaced == false) {
				Destroy (other.gameObject);
				towerPosition = gameObject.transform.parent.transform.position;
				Quaternion towerRotation = Quaternion.identity;
				if (towerID == 1 && ((team == 2 && gameController.team2Cash >= 25) || (team == 1 && gameController.team1Cash >= 25 )) ) {
						if(team == 2) { gameController.team2Cash -= 25; }
						if(team == 1) { gameController.team1Cash -= 25; }
						Instantiate (attackTower, towerPosition, towerRotation);
						foreach (Renderer renderer in gameObject.transform.parent.GetComponentsInChildren(typeof(Renderer))){
							renderer.enabled = false;
						}
						foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
							collider.enabled = false;
						}
						transform.parent.GetComponent<TowerTouchTrigger2>().towerPlaced = true;
				} else if (towerID == 2  && ((team == 2 && gameController.team2Cash >= 50) || (team == 1 && gameController.team1Cash >= 50 ))) {
						if(team == 2) { gameController.team2Cash -= 50; }
						if(team == 1) { gameController.team1Cash -= 50; }
						Instantiate (boostTower, towerPosition, towerRotation);
						foreach (Renderer renderer in gameObject.transform.parent.GetComponentsInChildren(typeof(Renderer))){
							renderer.enabled = false;
						}
						foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
							collider.enabled = false;
						}
						transform.parent.GetComponent<TowerTouchTrigger2>().towerPlaced = true;
				} else { 
					transform.parent.GetComponent<TowerTouchTrigger2>().collider.enabled = true;
					if( towerID != 3 ){
						Vector3 PromptPosition = transform.position;
						Quaternion PromptRotation = Quaternion.identity;
						if (team == 2) { PromptRotation.y += 180; }
						Instantiate( NoCash, PromptPosition, PromptRotation );
					}
				}

				if(towerID == 3) {
					foreach (Renderer renderer in gameObject.transform.parent.GetComponentsInChildren(typeof(Renderer))){
						renderer.enabled = false;
					}
					foreach (SphereCollider collider in gameObject.transform.parent.GetComponentsInChildren(typeof(SphereCollider))){
						collider.enabled = false;
					}
					transform.parent.GetComponent<TowerTouchTrigger2>().collider.enabled = true;
				} else {
					
				}
			}
		}
}
