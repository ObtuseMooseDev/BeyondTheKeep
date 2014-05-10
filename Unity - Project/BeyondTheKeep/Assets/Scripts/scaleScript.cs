using UnityEngine;
using System.Collections;

public class scaleScript : MonoBehaviour {
	public GameController gameController;
	public float scaleFactor;
	// Use this for initialization
	void Start () {
		scaleFactor = 0;
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
		if (scaleFactor != gameController.GetComponent<GameController> ().MAXHEALTH - gameController.GetComponent<GameController> ().team1Health) {
						scaleFactor = gameController.GetComponent<GameController> ().MAXHEALTH - gameController.GetComponent<GameController> ().team1Health;
						Vector3 temp = new Vector3(0,0,0);
						temp = gameObject.transform.position;
						
						//scaleFactor = transform.position.z - scaleFactor;
						//transform.right = scaleFactor;
		}

	}	
}
