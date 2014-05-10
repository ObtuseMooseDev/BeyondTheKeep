using UnityEngine;
using System.Collections;

public class TouchCreate : MonoBehaviour {
	public Vector3 touchCoordinates;
	public GameObject touchObject;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Input.touchCount; ++i) {
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began) {
				Quaternion touchRotation = Quaternion.identity;
				touchCoordinates = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
				touchCoordinates.y = 0;
				
				Instantiate (touchObject, touchCoordinates, touchRotation);
			}
		}
	}
}
