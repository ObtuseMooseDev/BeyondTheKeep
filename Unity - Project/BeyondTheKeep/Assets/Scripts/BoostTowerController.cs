using UnityEngine;
using System.Collections;

//[System.Serializable]
//public class Boundary{
//	public float xMin, xMax, zMin, zMax;
//}
public class BoostTowerController : MonoBehaviour {
	
	public float shotlead;
	public float team;

	public GameObject boost;

	private float radians;
	private float angle; 
	
	public Transform shotSpawn;
	public GameObject target;
	public bool minionBoosted;
	
	void Start(){
		if (transform.position.x <= 0) {
			team = 2;
		} else {
			team = 1;
		}
		if (team == 1) {
			shotlead = shotlead * 1;
		} else {
			shotlead = shotlead * -1;
		}
	}
	void Update(){
		
		
	}


	void FixedUpdate(){
		//legacy function, now irrelevent.
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<MinionController> ().team == team) {
			//Quaternion touchRotation = Quaternion.identity;

			other.gameObject.GetComponent<MinionController> ().minionBoosted = true;
			other.gameObject.GetComponent<MinionController> ().timesBoosted++;
			//Instantiate(boost, other.gameObject.transform.position, touchRotation);
		}
	}
	//	void OnTriggerStay (Collider other) {
	
	//	}
	
	void OnTriggerExit( Collider other){
		//if the target escapes, target  = null;
		target = null;
	}
}
