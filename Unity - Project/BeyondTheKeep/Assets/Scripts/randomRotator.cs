using UnityEngine;
using System.Collections;

public class randomRotator : MonoBehaviour {

	public float tumble;

	void Start(){
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	} 
}
