using UnityEngine;
using System.Collections;

public class DestroyIfLocked : MonoBehaviour {
	private GameSettings gameSettings;
	// Use this for initialization
	void Start () {
		GameObject gameSettingsObject = GameObject.FindWithTag ("GameSettings");
		if (gameSettingsObject != null) {
			gameSettings = gameSettingsObject.GetComponent<GameSettings>();
			//practiceMode = gameSettingsObject.GetComponent<PracticeMode>();
			Debug.Log (gameSettings.stuff);
		}
		if (gameSettings.poisonUnlock == false) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
