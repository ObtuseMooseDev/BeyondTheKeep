using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}
public class AttackTowerController : MonoBehaviour {

	private float nextFire;
	public float fireRate;

	private float dx, dy;
	private float rotateY;

	public float shotlead;
	public float team;

	private float radians;
	private float angle; 
	public Boundary boundary;

	public GameObject shot;
	public GameObject target;
	public Transform shotSpawn;

	private bool shoot = false;
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
		if (shoot && Time.time > nextFire ) {

			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play();

			shoot = false;
			target = null;
		}
		shoot = false;
		target = null;

	}

	void FixedUpdate(){
		//legacy function, now irrelevent.
	}
	void OnTriggerEnter(Collider other){
		//legacy function, now irrelevent.
	}
	void OnTriggerStay (Collider other) {
		if (target == null) {
			target = null; // sets destroyed target reference to null. 
		}
		if (other.tag == "Boundary" || other.tag == "Player" || other.tag == "Bolt" || (other.tag == "Team1Minion" && team == 1) || (other.tag == "Team2Minion" && team == 2)) { //ensures the target is not a boundary or player
			return;
		} else if(target == null) { // if no target, set target.
			target = other.gameObject; 
		}

		if (other.tag == "Boundary" || (other.tag == "Team1Minion" && team == 1) || (other.tag == "Team2Minion" && team == 2) ) { 
			return;
		}
		//CHANGE ASTEROID TAG TO MINION
		else if((other.tag == "Team1Minion" || other.tag == "Team2Minion") && other.gameObject == target){
			if( other.GetComponent<DestroyByContact>().health > 0 ) {
				shoot = true; //set shoot to true thus starting a shot animation.
			}
			dx = transform.position.x - (other.gameObject.transform.position.x + shotlead); // delta x between Tower and Target
			dy = transform.position.z - (other.gameObject.transform.position.z); // delta y (technically delta z, between Tower and Target
			
			radians = Mathf.Atan2(dx,dy); // calculate the radian angle using cosine law
			
			angle = radians * 180 / Mathf.PI - 180; // convert radian to angles
			rotateY = Mathf.LerpAngle(transform.rotation.y, angle, 5); // create angular rotation

			//.Log(rotateY);
			transform.eulerAngles = new Vector3(0, rotateY, 0.0f); // rotate about y axis

		}
	}

	void OnTriggerExit( Collider other){
		//if the target escapes, target  = null;
		target = null;
	}
}
