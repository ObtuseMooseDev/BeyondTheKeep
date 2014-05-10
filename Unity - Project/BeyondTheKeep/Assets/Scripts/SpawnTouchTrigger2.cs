using UnityEngine;
using System.Collections;

public class SpawnTouchTrigger2 : MonoBehaviour {
	public Sprite blueSprite;
	public Sprite redSprite;
	public Sprite greenSprite;
	public Sprite poisonSprite;
	public Sprite mblueSprite;
	public Sprite mredSprite;
	public Sprite mgreenSprite;
	public Sprite lblueSprite;
	public Sprite lredSprite;
	public Sprite lgreenSprite;
	public Sprite lposionSprite;
	public Sprite graySprite;
	public int team;
	private SpriteRenderer myRenderer;
	public GameController gameController;
	public PracticeMode practiceMode;
	public bool activeTower;

	public GameObject NoCash;

	// Use this for initialization
	void Start () {
		if (transform.position.x > 0) {
			team = 2;
		} else {
			team = 1;
		}
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
			if( gameController.isPractice ){
				practiceMode = gameControllerObject.GetComponent<PracticeMode>();
			}
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.isPractice){
			if (practiceMode.currentLevel == 5) {
				Destroy (gameObject);
			}
		}
		Sprite tempSprite;
		if (GetComponentInChildren<SpawnController> ().minionType == 1 && GetComponentInChildren<SpawnController> ().minionLevel == 1) {
			tempSprite = blueSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 2 && GetComponentInChildren<SpawnController> ().minionLevel == 1) {
			tempSprite = redSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 3 && GetComponentInChildren<SpawnController> ().minionLevel == 1) {
			tempSprite = greenSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 1 && GetComponentInChildren<SpawnController> ().minionLevel == 2) {
			tempSprite = mblueSprite; 
		} else if (GetComponentInChildren<SpawnController> ().minionType == 2 && GetComponentInChildren<SpawnController> ().minionLevel == 2) {
			tempSprite = mredSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 3 && GetComponentInChildren<SpawnController> ().minionLevel == 2) {
			tempSprite = mgreenSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 1 && GetComponentInChildren<SpawnController> ().minionLevel == 3) {
			tempSprite = lblueSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 2 && GetComponentInChildren<SpawnController> ().minionLevel == 3) {
			tempSprite = lredSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 3 && GetComponentInChildren<SpawnController> ().minionLevel == 3) {
			tempSprite = lgreenSprite;
		} else if (GetComponentInChildren<SpawnController> ().minionType == 4 && GetComponentInChildren<SpawnController> ().minionLevel == 1) {
			tempSprite = lposionSprite;
		} else {
			tempSprite = graySprite;
		}

		foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
			if(renderer.gameObject.tag == "rend"){
				myRenderer = renderer;
				myRenderer.sprite = tempSprite;
			}
		}

	}
	void OnTriggerStay(Collider other){
		Debug.Log (other.tag);
		if (other.tag == "Touch") {
			if(activeTower == false){
				foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){	
					if( renderer.gameObject.tag == "GUI" ) {
						renderer.enabled = true;
					}
				}
				foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					if( collider.gameObject.tag == "GUI" ) {
						collider.enabled = true;
					}
				}
			} else {
				foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){	
					if( renderer.gameObject.tag == "GUI2" ) {
						renderer.enabled = true;
					}
				}
				foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					if( collider.gameObject.tag == "GUI2" ) {
						collider.enabled = true;
					}
				}
			}
			Destroy (other.gameObject);
		}

	}
	public void changeSpriteElement (string element)
	{
			if( (team == 2 && gameController.team2Cash >= 15) || (team == 1 && gameController.team1Cash >= 15 ) ){
				if(team == 2){ gameController.team2Cash = gameController.team2Cash - 15; }
				if(team == 1){ gameController.team1Cash = gameController.team1Cash - 15; }
				Debug.Log ("Changed");
				activeTower = true;
				if(element == "blue"){
					GetComponentInChildren<SpawnController>().minionType = 1;
					GetComponentInChildren<SpawnController>().change = true;
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
						if(renderer.gameObject.tag == "rend"){
							myRenderer = renderer;
							myRenderer.sprite = blueSprite;
						}
					}
				} else if(element == "red"){
					GetComponentInChildren<SpawnController>().minionType = 2;
					GetComponentInChildren<SpawnController>().change = true;
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
						if(renderer.gameObject.tag == "rend"){
							myRenderer = renderer;
							myRenderer.sprite = redSprite;
						}
					}
				} else if(element == "green"){
					GetComponentInChildren<SpawnController>().minionType = 3;
					GetComponentInChildren<SpawnController>().change = true;
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
						if(renderer.gameObject.tag == "rend"){
							myRenderer = renderer;
							myRenderer.sprite = greenSprite;
						}
					}
				} else if(element == "poison" ){
					GetComponentInChildren<SpawnController>().minionType = 4;
					GetComponentInChildren<SpawnController>().change = true;
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
						if(renderer.gameObject.tag == "rend"){
							myRenderer = renderer;
							myRenderer.sprite = poisonSprite;
						}
					}
				}

				foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag == "GUI"  || renderer.gameObject.tag == "GUI2" || renderer.gameObject.tag == "GUI3" ) {
						renderer.enabled = false;
					}
				}
				foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					if( collider.gameObject.tag == "GUI" || collider.gameObject.tag == "GUI2" || collider.gameObject.tag == "GUI3") {
						collider.enabled = false;
					}	

				}
			} else {
				Vector3 PromptPosition = transform.position;
				PromptPosition.y += 2;
				Quaternion PromptRotation = Quaternion.identity;
				if (team == 2) { PromptRotation.y += 180; PromptPosition.x -= 2; } else { PromptPosition.x += 2; }
				Instantiate( NoCash, PromptPosition, PromptRotation );
			}
	/*** else if (element == "poison") {
			if( (team == 2 && gameController.team2Cash >= 30) || (team == 1 && gameController.team1Cash >= 30 ) ){
				if(team == 2){ gameController.team2Cash = gameController.team2Cash - 30; }
				if(team == 1){ gameController.team1Cash = gameController.team1Cash - 30; }

				GetComponentInChildren<SpawnController>().minionType = 4;
				GetComponentInChildren<SpawnController>().change = true;
				foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if(renderer.gameObject.tag == "rend"){
						myRenderer = renderer;
						myRenderer.sprite = poisonSprite;
					}
				}
				foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag == "GUI"  || renderer.gameObject.tag == "GUI2" || renderer.gameObject.tag == "GUI3" ) {
						renderer.enabled = false;
					}
				}
				foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					if( collider.gameObject.tag == "GUI" || collider.gameObject.tag == "GUI2" || collider.gameObject.tag == "GUI3") {
						collider.enabled = false;
					}	
					
				}
			} else {
				Vector3 PromptPosition = transform.position;
				PromptPosition.y += 2;
				Quaternion PromptRotation = Quaternion.identity;
				if (team == 2) { PromptRotation.y += 180; PromptPosition.x -= 2; } else { PromptPosition.x += 2; }
				Instantiate( NoCash, PromptPosition, PromptRotation );
			}
		} **/
	}
	public void showElements(){
		foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){	
			if( renderer.gameObject.tag == "GUI3" ) {
				renderer.enabled = true;
			}
		}
		foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
			if( collider.gameObject.tag == "GUI3" ) {
				collider.enabled = true;
			}
		}
	}
	public void increaseSpawn(){
		if( GetComponentInChildren<SpawnController>().minionCount < 3 ) { 
			if ((team == 2 && gameController.team2Cash >= 30) || (team == 1 && gameController.team1Cash >= 30)) {
				if(team == 2){ gameController.team2Cash = gameController.team2Cash - 30; }
				if(team == 1){ gameController.team1Cash = gameController.team1Cash - 30; }
				GetComponentInChildren<SpawnController>().minionCount++;
				GetComponentInChildren<SpawnController>().change = true;
				foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){	
					if( renderer.gameObject.tag == "GUI2" || renderer.gameObject.tag == "GUI3" ) {
						renderer.enabled = false;
					}
				}
				foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					if( collider.gameObject.tag == "GUI2" || collider.gameObject.tag == "GUI3") {
						collider.enabled = false;
					}
				}
			} else {
				Vector3 PromptPosition = transform.position;
				PromptPosition.y += 2;
				Quaternion PromptRotation = Quaternion.identity;
				if (team == 2) { PromptRotation.y += 180; PromptPosition.x -= 2; } else { PromptPosition.x += 2; }
				Instantiate( NoCash, PromptPosition, PromptRotation );
			}
		}
	}
	public void levelUp(){
		int cost;
		cost = GetComponentInChildren<SpawnController> ().minionLevel * 30;
		if( GetComponentInChildren<SpawnController> ().minionLevel < 3) {
			if ((team == 2 && gameController.team2Cash >= cost) || (team == 1 && gameController.team1Cash >= cost)) {
				if(team == 2){ gameController.team2Cash = gameController.team2Cash - cost; }
				if(team == 1){ gameController.team1Cash = gameController.team1Cash - cost; }
				GetComponentInChildren<SpawnController> ().minionLevel++;
				GetComponentInChildren<SpawnController> ().change = true;
				foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){	
					if( renderer.gameObject.tag == "GUI2" || renderer.gameObject.tag == "GUI3") {
						renderer.enabled = false;
					}
				}
				foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					if( collider.gameObject.tag == "GUI2" || collider.gameObject.tag == "GUI3" ) {
						collider.enabled = false;
					}
				}
			} else {
				Vector3 PromptPosition = transform.position;
				PromptPosition.y += 2;
				Quaternion PromptRotation = Quaternion.identity;
				if (team == 2) { PromptRotation.y += 180; PromptPosition.x -= 2; } else { PromptPosition.x += 2; }
				Instantiate( NoCash, PromptPosition, PromptRotation );
			}
		}
	}
	public void cancel(){
		foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){	
			if( renderer.gameObject.tag == "GUI" || renderer.gameObject.tag == "GUI2" || renderer.gameObject.tag == "GUI3") {
				renderer.enabled = false;
			}
		}
		foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
			if( collider.gameObject.tag == "GUI"  || collider.gameObject.tag == "GUI2" || renderer.gameObject.tag == "GUI3" ) {
				collider.enabled = false;
			}
		}
	}
}
