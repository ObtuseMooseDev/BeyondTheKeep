using UnityEngine;
using System.Collections;

public class ChangeDisplay : MonoBehaviour {
	public bool change;
	public bool guiActive;
	private GameObject tempObject;

	private SpriteRenderer myRenderer;

	public Sprite guiSelected;
	public Sprite guiRest;
	// Use this for initialization
	void Start () {
		change = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (guiActive) {
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				if(renderer.gameObject.tag == tag){
					myRenderer = renderer;
				}
			}
			myRenderer.sprite = guiSelected;
		} else if ( !guiActive ) {
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				if(renderer.gameObject.tag == tag){
					myRenderer = renderer;
				}
			}
			myRenderer.sprite = guiRest;
		}
		if (change == true) {
			guiActive = true;
			if(tag == "Backgrounds"){
				tempObject = GameObject.FindWithTag ("MenuStyles");
				tempObject.GetComponent<ChangeDisplay>().guiActive = false;
				foreach (SpriteRenderer renderer in tempObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag != "MenuStyles" ){
						renderer.enabled = false;
						renderer.gameObject.collider.enabled = false;
					}
				}
				tempObject = GameObject.FindWithTag ("MinionTowers");
				tempObject.GetComponent<ChangeDisplay>().guiActive = false;
				foreach (SpriteRenderer renderer in tempObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag != "MinionTowers" ){
						renderer.enabled = false;
						renderer.gameObject.collider.enabled = false;
					}
				}
			} else if(tag == "MenuStyles"){
				tempObject = GameObject.FindWithTag ("Backgrounds");
				tempObject.GetComponent<ChangeDisplay>().guiActive = false;
				foreach (SpriteRenderer renderer in tempObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag != "Backgrounds" ){
						renderer.enabled = false;
						renderer.gameObject.collider.enabled = false;
					}
				}
				tempObject = GameObject.FindWithTag ("MinionTowers");
				tempObject.GetComponent<ChangeDisplay>().guiActive = false;
				foreach (SpriteRenderer renderer in tempObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag != "MinionTowers" ){
						renderer.enabled = false;
						renderer.gameObject.collider.enabled = false;
					}
				}
			} else if(tag == "MinionTowers"){
				tempObject = GameObject.FindWithTag ("Backgrounds");
				tempObject.GetComponent<ChangeDisplay>().guiActive = false;
				foreach (SpriteRenderer renderer in tempObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag != "Backgrounds" ){
						renderer.enabled = false;
						renderer.gameObject.collider.enabled = false;
					}
				}
				tempObject = GameObject.FindWithTag ("MenuStyles");
				tempObject.GetComponent<ChangeDisplay>().guiActive = false;
				foreach (SpriteRenderer renderer in tempObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag != "MenuStyles"){
						renderer.enabled = false;
						renderer.gameObject.collider.enabled = false;
					}
				}
			}
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				renderer.enabled = true;
			}
			foreach (BoxCollider collider in gameObject.GetComponentsInChildren(typeof(BoxCollider))){
				collider.enabled = true;
			}
			change = false;

		}
	}
}
