using UnityEngine;
using System.Collections;

public class DestroyOnTouch : MonoBehaviour {
	public int promptLevel;
	public PracticeMode practiceMode;
	// Use this for initialization
	void Start () {
		if ( promptLevel == 0) {
			Debug.Log ("ERROR: PromptLevel not assigned");
		}
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			practiceMode = gameControllerObject.GetComponent<PracticeMode>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (practiceMode.currentLevel > promptLevel) {
			Destroy (gameObject);
		}
	}
	void OnTriggerStay(Collider other){
		if (other.tag == "Touch") {
			Destroy (gameObject);
		}
	}
}
