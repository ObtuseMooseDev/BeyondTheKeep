using UnityEngine;
using System.Collections;

public class TowerTouchTrigger2 : MonoBehaviour {
	public bool activeTower, towerPlaced;
	private Vector3 touchCoordinates;
	public int team;
	public bool toggle;
	// Use this for initialization
	void Start () {
		towerPlaced = false;
		toggle = true;
		if(gameObject.transform.position.x <= 0) { team = 1;} else { team = 2; }
	}
	
	// Update is called once per frame
	void Update () {
		/**
		for (int i = 0; i < Input.touchCount; ++i) {
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began) {
				touchCoordinates = Input.GetTouch(i).position;
				touchCoordinates = Camera.main.ScreenToWorldPoint(touchCoordinates);
			}
		}
		if (toggle == true) {
			delay++;
		}
		if(delay >= 60){
			if( ( touchCoordinates.x <= 0 && team == 1 ) || (touchCoordinates.x > 0 && team == 2) ){
				foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){
					renderer.enabled = false;
					//activeTower = false;
				}
				foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					collider.enabled = false;
				}
			}
			toggle = false;
			delay = 0;
		}
		touchCoordinates.x = 1000;
		touchCoordinates.y = 1000;
		**/
	}
	void OnTriggerStay(Collider other){
		if (other.tag == "Touch" && towerPlaced == false) {
			//toggle = true;
			Destroy(other.gameObject);
			foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))){
					renderer.enabled = true;
			}
			foreach (SphereCollider collider in gameObject.GetComponentsInChildren(typeof(SphereCollider))){
					collider.enabled = true;
			} 
			gameObject.collider.enabled = false;
		}
	}
}
