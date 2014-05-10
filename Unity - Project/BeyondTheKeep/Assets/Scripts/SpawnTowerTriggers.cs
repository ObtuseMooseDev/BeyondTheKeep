using UnityEngine;
using System.Collections;

public class SpawnTowerTriggers : MonoBehaviour {
	public GameObject towerTouchTrigger;
	private Vector3 tPosition;
	private Quaternion towerRotation;
	// Use this for initialization
	void Start () {
		tPosition = transform.position;
		towerRotation = Quaternion.identity;
		if (tPosition.x > 0) {
						towerRotation.y = 180;
				}
		for(int i = 0; i <= 3; i++){
			Instantiate (towerTouchTrigger, tPosition, towerRotation);
			tPosition.x += 2.7f;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
