using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;

	void Start(){
			
			if (tag == "Bolt") {
						rigidbody.velocity = transform.forward * speed;
				} else {
						rigidbody.velocity = transform.right * speed;
				}
	}
}
