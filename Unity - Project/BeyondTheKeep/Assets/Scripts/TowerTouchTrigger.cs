using UnityEngine;
using System.Collections;

public class TowerTouchTrigger : MonoBehaviour {
	private bool activeTower, towerPlaced;
	private float team;
	private Renderer makeVisible;
	public GameObject tower, boost;
	private Vector3 touchCoordinates, objectCoordinates;
	private Vector3 mousePos, objectPos;
	public GameController gameController;
	// Use this for initialization
	void Start () {
		if(transform.position.x >= 0){
			team = 2;
		} else {
			team = 1;
		}
		activeTower = false; towerPlaced = false;
		objectCoordinates = Camera.main.WorldToScreenPoint(transform.position);
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {

				if(activeTower == false){
					for (int i = 0; i < Input.touchCount; ++i) {
						Touch touch = Input.GetTouch(i);
						if (touch.phase == TouchPhase.Began) {
							touchCoordinates = Input.GetTouch(i).position;

						if(touchCoordinates.x <= objectCoordinates.x + 25 && touchCoordinates.x >= objectCoordinates.x - 25 
					   			&& touchCoordinates.y <= objectCoordinates.y + 25 && touchCoordinates.y >= objectCoordinates.y - 25){
								activeTower = true;
							}
						}	
					}
				}
				else if (activeTower == true && towerPlaced == false) {
					foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer)))
						
					{
						
						renderer.enabled = true;
						
					}
					for (int i = 0; i < Input.touchCount; ++i) {
						Touch touch = Input.GetTouch(i);
						if (touch.phase == TouchPhase.Began) {
							touchCoordinates = Input.GetTouch(i).position;
							touchCoordinates = Camera.main.ScreenToWorldPoint(touchCoordinates);
						if(touchCoordinates.x <= transform.position.x - 0.25 && touchCoordinates.x >= transform.position.x - 1.5
					   		&& touchCoordinates.z <= transform.position.z + 0.5 && touchCoordinates.z >= transform.position.z - 0.5){
									Quaternion towerRotation = Quaternion.identity;
									if(team == 2 && gameController.team2Cash >= 25){ 
										Instantiate(tower, transform.position, towerRotation);
										foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){
											renderer.enabled = false;
										}
										towerPlaced = true;
										Destroy(gameObject);
										gameController.team2Cash = gameController.team2Cash - 25;
									}
									else if( team == 1 && gameController.team1Cash >= 25){ 
										Instantiate(tower, transform.position, towerRotation);
										foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){
											renderer.enabled = false;
										}
										towerPlaced = true;
										Destroy(gameObject);
										gameController.team1Cash = gameController.team1Cash - 25; 
									}
									
						} else if(touchCoordinates.x <= transform.position.x + 1.5 && touchCoordinates.x >= transform.position.x + 0.25 
					          && touchCoordinates.z <= transform.position.z + 0.5 && touchCoordinates.z >= transform.position.z - 0.5){
									Quaternion towerRotation = Quaternion.identity;
									if(team == 2 && gameController.team2Cash >= 50){ 
										Instantiate(boost, transform.position, towerRotation);
											foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){
												renderer.enabled = false;
											}
										towerPlaced = true;
										Destroy(gameObject);
										gameController.team2Cash = gameController.team2Cash - 50;
									} else if ( team == 1 && gameController.team1Cash >= 50 ) {
										Instantiate(boost, transform.position, towerRotation);
										foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){
											renderer.enabled = false;
										}
										towerPlaced = true;
										Destroy(gameObject);
										gameController.team1Cash = gameController.team1Cash - 50;
									}	
					} else if( ( touchCoordinates.x <= 0 && team == 1 ) || (touchCoordinates.x > 0 && team == 2) ){
									foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){
										renderer.enabled = false;
										activeTower = false;
									}	
							}
					}
				}
		}
	}
}
