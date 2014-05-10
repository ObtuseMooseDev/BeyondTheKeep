using UnityEngine;
using System.Collections;

public class GUIPress : MonoBehaviour {
	public string buyID;
	private SpriteRenderer myRenderer;
	public Sprite pressed, rest, activeSprite;
	public bool restState;
	public bool activeState, activeChange;
	public int restBuffer;
	public bool purchased;
	private GameSettings gameSettings;
	// Use this for initialization
	void Start () {
		GameObject gameSettingsObject = GameObject.FindWithTag ("GameSettings");
		if (gameSettingsObject != null) {
			gameSettings = gameSettingsObject.GetComponent<GameSettings>();
			Debug.Log (gameSettings.stuff);
		}

		restState = true;
		purchased = false;
		if (gameSettings.checkPurchased (buyID)) {
			purchased = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!restState) {
			restBuffer++;
			if(restBuffer == 20){
				switchSpriteRenderer();
				restBuffer = 0;
				restState = true;
				if(tag == "buyItem") { purchased = true; }
			}
		}
		if (activeChange && activeState && purchased) {
			foreach(SpriteRenderer renderer in transform.parent.gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				if( renderer.gameObject.tag == tag ){
					if( renderer.gameObject.GetComponent<GUIPress>().activeState == true ){
						renderer.gameObject.GetComponent<GUIPress>().activeState = false;
					}
				}
			}
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){

				if( renderer.gameObject.tag == tag ){
					myRenderer = renderer;
				}
			}
			myRenderer.sprite = activeSprite;
			activeState = true;
			activeChange = false;
		} else if( purchased && !activeState ){
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				if( renderer.gameObject.tag == tag ){
					myRenderer = renderer;
				}
			}
			myRenderer.sprite = rest;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Touch") {


			if (purchased) {
				activeChange = true;
				activeState = true;
				makeActive(buyID);
			} else if( !purchased ){
				foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
					if( renderer.gameObject.tag == tag ){
						myRenderer = renderer;
					}
				}
				myRenderer.sprite = pressed;
				restState = false;
				buttonAction(tag);
			} 
			Destroy( other.gameObject );
		}
	}

	void buttonAction (string tag)
	{
		if (tag == "Back") {
			Application.LoadLevel (0);
		} else if (tag == "buyItem"){
			gameSettings.setPurchased(buyID);
		}
	}

	void switchSpriteRenderer ()
	{
		foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
			if( renderer.gameObject.tag == tag ){
				myRenderer = renderer;
			}
		}
		myRenderer.sprite = rest;
	}

	void makeActive (string buyID)
	{
		if( buyID == "MS_Red" || buyID == "MS_LightBlue" || buyID == "MS_Yellow" )
			gameSettings.changeCurrentMenuStyle (buyID);
		if (buyID == "BG_Gem" || buyID == "BG_Velvet" || buyID == "BG_Flame")
			gameSettings.changeCurrentBackground (buyID);
		if (buyID == "MT_Poison")
			gameSettings.unlockPoison (buyID);
	}
}
