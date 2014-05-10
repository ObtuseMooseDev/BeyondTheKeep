using UnityEngine;
using System.Collections;

public class MakeVisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Mouse Position X : " + Input.mousePosition ) ;
		//Debug.Log ("This.transform position : " + Camera.main.WorldToScreenPoint(transform.position) );
		Vector3 mouseCoordinates = Input.mousePosition;
		Vector3 thisPosition = Camera.main.WorldToScreenPoint (transform.position);
		if ( mouseCoordinates.x <= (thisPosition.x + 50) && mouseCoordinates.x >= (thisPosition.x - 50)
		    && mouseCoordinates.y <= (thisPosition.y + 50) && mouseCoordinates.y >= (thisPosition.y - 50) ) {
						renderer.enabled = true;
						Debug.Log ("Mouse Touch!");
				} else {
						renderer.enabled = false;
				}
	}
}
