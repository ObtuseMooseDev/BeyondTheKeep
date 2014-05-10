using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {
	public float team;
	public int minionType; // 1 = Blue, 2 = Red, 3 = Green //
	public int minionLevel; // 1, 2, 3 //
	public int poisonCount;
	public int timesBoosted;
	public bool minionBoosted, noMoreBoost, nextHitWins, poisoned;
	private GameController gameController;
	private PracticeMode practiceMode;
	public Sprite redGlow;
	private SpriteRenderer myRenderer;

	void Start () {
		nextHitWins = false;
		poisonCount = 0;
		timesBoosted = 0;
		if (transform.position.x <= 0) {
			team = 2;
		} else {
			team = 1;
		}
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
			practiceMode = gameControllerObject.GetComponent<PracticeMode>();

		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		if (minionLevel == 1) {
						if( minionType == 4 ) {
							GetComponent<DestroyByContact> ().health = 1;
							GetComponent<DestroyByContact> ().attack = 1;
						} else {
							GetComponent<DestroyByContact> ().health = 2;
							GetComponent<DestroyByContact> ().attack = 2;
						}
				} else if (minionLevel == 2) {
						GetComponent<DestroyByContact> ().health = 4;
						GetComponent<DestroyByContact> ().attack = 4;
				} else if (minionLevel == 3) {
						GetComponent<DestroyByContact> ().health = 6;
						GetComponent<DestroyByContact> ().attack = 6;
				} 
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (minionBoosted == true && noMoreBoost == false){
		if( gameController.isPractice ){
			if( practiceMode.changeLevel || practiceMode.gameOver ){
				Destroy (gameObject);
			}

		}
		if( GetComponent<DestroyByContact> ().health <= 0 ){
			//ADD CASH TO OTHER TEAM CODE HERE
			Destroy (gameObject);
		}
		if (poisoned) {
			poisonCount++;
			if( poisonCount == 60 ){
				GetComponent<DestroyByContact> ().health--;
				poisonCount = 0;
			}
		}
		if( ( team == 1 && gameController.team2Health <= GetComponent<DestroyByContact>().attack) || ( team == 2 && gameController.team1Health <= GetComponent<DestroyByContact>().attack) ){
			nextHitWins = true;
			gameController.changeAudio = true;
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				if(renderer.tag == "nextImg"){
					renderer.enabled = true;
				}
			}
		}
		if (minionBoosted == true){
			if( timesBoosted == 1 ){
				//Debug.Log ("Before Boost" + gameObject.GetComponent<DestroyByContact>().health);
				gameObject.GetComponent<DestroyByContact>().health = gameObject.GetComponent<DestroyByContact>().health + 2;
				gameObject.GetComponent<DestroyByContact>().attack = gameObject.GetComponent<DestroyByContact>().attack + 2;
				//Debug.Log ("After Boost" + gameObject.GetComponent<DestroyByContact>().health);
			} else {
				//Debug.Log ("Before Boost" + gameObject.GetComponent<DestroyByContact>().health);
				gameObject.GetComponent<DestroyByContact>().health = gameObject.GetComponent<DestroyByContact>().health + 4;
				gameObject.GetComponent<DestroyByContact>().attack = gameObject.GetComponent<DestroyByContact>().attack + 4;
				//Debug.Log ("After Boost" + gameObject.GetComponent<DestroyByContact>().health);
			}
			//noMoreBoost = true;

			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				if(renderer.tag == "boostImg"){
					renderer.enabled = true;
				}
			}
			//Debug.Log ("Minion Boost, Attack: " + gameObject.GetComponent<DestroyByContact>().attack );
			minionBoosted = false;
			timesBoosted = 0;

		}
	}
}
