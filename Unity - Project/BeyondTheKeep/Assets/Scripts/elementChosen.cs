using UnityEngine;
using System.Collections;

public class elementChosen : MonoBehaviour {
	public string element; 
	public bool changeElement, increase, upgrade, cancel;
	private SpawnTouchTrigger2 parent;
	public GameController gameController;
	public int team;
	public int spawnCounter;
	public Sprite spawn3Sprite, spawnNoMore;
	public int upgradeCounter;
	public Sprite upgrade3Sprite, upgradeNoMore;
	private SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
		//spawnCounter = 1;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (transform.position.x > 0) {
			team = 2;
		} else {
			team = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other){
		Debug.Log (other.tag);
		if (other.tag == "Touch") {
			if( changeElement ){
				transform.parent.gameObject.GetComponent<SpawnTouchTrigger2> ().showElements();
			} else if ( increase ){

				if( (team == 2 && gameController.team2Cash >= 30) || (team == 1 && gameController.team1Cash >= 30) ){
					spawnCounter++;
					if( spawnCounter == 2 ) {
						foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
							if( renderer.gameObject.tag != "minionSprite" ){
								myRenderer = renderer;
								myRenderer.sprite = spawn3Sprite;
							}
						}
					}
					else if ( spawnCounter == 3 ){
						foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
							if( renderer.gameObject.tag != "minionSprite" ){
								myRenderer = renderer;
								myRenderer.sprite = spawnNoMore;
							}
						}
					}
				}
				transform.parent.gameObject.GetComponent<SpawnTouchTrigger2> ().increaseSpawn();

			} else if ( upgrade && element != "poison") {
				int cost = upgradeCounter * 30;
				if ((team == 2 && gameController.team2Cash >= cost) || (team == 1 && gameController.team1Cash >= cost)) {
					upgradeCounter++;
					if( upgradeCounter == 2 ) {
						foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
							if( renderer.gameObject.tag != "minionSprite" ){
								myRenderer = renderer;
								myRenderer.sprite = upgrade3Sprite;
							}
						}
					}
					else if ( upgradeCounter == 3){
						foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
							if( renderer.gameObject.tag != "minionSprite" ){
								myRenderer = renderer;
								myRenderer.sprite = upgradeNoMore;
							}
						}
					}
				}
				transform.parent.gameObject.GetComponent<SpawnTouchTrigger2> ().levelUp();
			} else if ( upgrade && element == "poison") {
				foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag != "minionSprite" ){
						myRenderer = renderer;
						myRenderer.sprite = upgradeNoMore;
					}
				}
			}
			else if ( cancel ) {
				transform.parent.gameObject.GetComponent<SpawnTouchTrigger2> ().cancel();
			}
			else {
				transform.parent.gameObject.GetComponent<SpawnTouchTrigger2> ().changeSpriteElement (element);
				if(team == 1) { 
					gameController.p1MinionPlaced = true; 
				} else if (team == 2) { 
					gameController.p2MinionPlaced = true; 
				}
				//spawnCounter++;
			}
			Destroy (other.gameObject);
		}

	}

}
