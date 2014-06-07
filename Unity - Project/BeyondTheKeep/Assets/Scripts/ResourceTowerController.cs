using UnityEngine;
using System.Collections;

public class ResourceTowerController : MonoBehaviour {
	private int team;
	private int bufferCount; 
	public GameController gameController;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

		if (transform.position.x > 0)
						team = 1;
				else
						team = 2;
		bufferCount = 60;
	}
	
	// Update is called once per frame
	void Update () {
		if (bufferCount == 60) {
			bufferCount = 0;
			if ( team == 2 ){
				gameController.team1Cash += 5;
			} else {
				gameController.team2Cash += 5;
			}
		} else {
			bufferCount++;
		}
	}
}
