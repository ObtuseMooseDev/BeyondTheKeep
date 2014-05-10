using UnityEngine;
using System.Collections;

public class SpawnTouchTrigger : MonoBehaviour {
	public Sprite blueSprite;
	public Sprite redSprite;
	public Sprite greenSprite;
	private int team;
	private SpriteRenderer myRenderer;
	//string element;
	//private GameObject gObj;
	public GameController gameController;
	public bool activeTower;
	private Vector3 touchCoordinates, objectCoordinates;
	// Use this for initialization
	void Start () {
		objectCoordinates = Camera.main.WorldToScreenPoint(transform.position);
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
	
	// Update is called once per frame
	void Update () {
		/**
		if (Input.GetMouseButtonDown (0)) {
			changeSpriteElement("blue");
		} **/

		if(activeTower == false){
			for (int i = 0; i < Input.touchCount; ++i) {
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began) {
					touchCoordinates = Input.GetTouch(i).position;
					objectCoordinates = Camera.main.WorldToScreenPoint(transform.position);
					if(touchCoordinates.x <= objectCoordinates.x + 25 && touchCoordinates.x >= objectCoordinates.x - 25 
					   && touchCoordinates.y <= objectCoordinates.y + 25 && touchCoordinates.y >= objectCoordinates.y - 25){
						activeTower = true;
					}
				}	
			}
		} else if (activeTower == true){
			foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){	
				renderer.enabled = true;
			}
			for (int i = 0; i < Input.touchCount; ++i) {
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began) {
					touchCoordinates = Input.GetTouch(i).position;
					objectCoordinates = Camera.main.ScreenToWorldPoint (objectCoordinates);
					touchCoordinates = Camera.main.ScreenToWorldPoint (touchCoordinates);
					if(team == 1 && gameController.team1Cash >= 5){

						if(touchCoordinates.x <= transform.position.x + 3 && touchCoordinates.x >= transform.position.x - 0.5
						   && touchCoordinates.z <= transform.position.z + 0.25 && touchCoordinates.z >= transform.position.z - 0.25){
								foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
									if(renderer.gameObject.tag == "rend"){
										myRenderer = renderer;
										myRenderer.sprite = blueSprite;
										changeSpriteElement("blue");
										gameController.team1Cash = gameController.team1Cash - 5;
									}
									if( renderer.gameObject.tag == "GUI" ) {
										renderer.enabled = false;
										activeTower = false;
									}
								}
								
						} else if(touchCoordinates.x <= transform.position.x + 2.0 && touchCoordinates.x >= transform.position.x + 0
						          && touchCoordinates.z <= transform.position.z - 0.5 && touchCoordinates.z >= transform.position.z - 2.5){
							foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
								if(renderer.gameObject.tag == "rend"){
									myRenderer = renderer;
									myRenderer.sprite = greenSprite;
									changeSpriteElement("green");
									gameController.team1Cash = gameController.team1Cash - 5;

								}
								if( renderer.gameObject.tag == "GUI" ) {
									renderer.enabled = false;
									activeTower = false;
								}
							}
							
						} else if(touchCoordinates.x <= transform.position.x + 2.0 && touchCoordinates.x >= transform.position.x + 0
						          && touchCoordinates.z <= transform.position.z + 2.5 && touchCoordinates.z >= transform.position.z + 0.5){
							foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
								if(renderer.gameObject.tag == "rend"){
									myRenderer = renderer;
									myRenderer.sprite = redSprite;
									changeSpriteElement("red");
									gameController.team1Cash = gameController.team1Cash - 5;
								}
								if( renderer.gameObject.tag == "GUI" ) {
									renderer.enabled = false;
									activeTower = false;
								}
							}
						} else {
							foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
								if( renderer.gameObject.tag == "GUI" ) {
									renderer.enabled = false;
									activeTower = false;
								}
								
							}
						}
					} else if (team == 2 && gameController.team2Cash >= 5){

						if(touchCoordinates.x <= transform.position.x - 0.5 && touchCoordinates.x >= transform.position.x - 3.0
						   && touchCoordinates.z <= transform.position.z + 0.25 && touchCoordinates.z >= transform.position.z - 0.25){
							foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
								if(renderer.gameObject.tag == "rend"){
									myRenderer = renderer;
									myRenderer.sprite = blueSprite;
									changeSpriteElement("blue");
									gameController.team2Cash = gameController.team2Cash - 5;
								}
								if( renderer.gameObject.tag == "GUI" ) {
									renderer.enabled = false;
									activeTower = false;
								}
							}
							
						} else if(touchCoordinates.x <= transform.position.x - 0.0 && touchCoordinates.x >= transform.position.x - 2.0
						          && touchCoordinates.z <= transform.position.z - 0.5 && touchCoordinates.z >= transform.position.z - 2.5){
							foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
								if(renderer.gameObject.tag == "rend"){
									myRenderer = renderer;
									myRenderer.sprite = redSprite;
									changeSpriteElement("red");
									gameController.team2Cash = gameController.team2Cash - 5;
								} 
								if( renderer.gameObject.tag == "GUI" ) {
									renderer.enabled = false;
									activeTower = false;
								}
							}
							
						} else if(touchCoordinates.x <= transform.position.x + 0.0 && touchCoordinates.x >= transform.position.x - 2.0
						          && touchCoordinates.z <= transform.position.z + 2.5 && touchCoordinates.z >= transform.position.z + 0.5){
							foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
								if(renderer.gameObject.tag == "rend"){
									myRenderer = renderer;
									myRenderer.sprite = greenSprite;
									changeSpriteElement("green");
									gameController.team2Cash = gameController.team2Cash - 5;
								}
								if( renderer.gameObject.tag == "GUI" ) {
									renderer.enabled = false;
									activeTower = false;
								}
							}
						} else { 
							foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
								if( renderer.gameObject.tag == "GUI" ) {
									renderer.enabled = false;
									activeTower = false;
								}

							}
						}
					}

				} 
			}
		}
	}

	void changeSpriteElement (string element)
	{
		if(element == "blue"){
			GetComponentInChildren<SpawnController>().minionType = 1;
			GetComponentInChildren<SpawnController>().change = true;
		} else if(element == "red"){
			GetComponentInChildren<SpawnController>().minionType = 2;
			GetComponentInChildren<SpawnController>().change = true;
		} else if(element == "green"){
			GetComponentInChildren<SpawnController>().minionType = 3;
			GetComponentInChildren<SpawnController>().change = true;
		}
	}
}
