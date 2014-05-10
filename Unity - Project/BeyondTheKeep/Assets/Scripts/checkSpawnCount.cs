using UnityEngine;
using System.Collections;

public class checkSpawnCount : MonoBehaviour {
	public Sprite oneMin;
	public Sprite twoMin;
	public Sprite threeMin;
	private Sprite tempSprite;
	private int minCount;
	public SpriteRenderer myRenderer;
	// Use this for initialization
	void Start () {
		minCount = 0;
		tempSprite = oneMin;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.parent.GetComponent<SpawnController> ().minionCount);
		if (transform.parent.GetComponent<SpawnController>().minionCount == 1) {
			tempSprite = oneMin;
		} else if(transform.parent.GetComponent<SpawnController>().minionCount == 2){
			tempSprite = twoMin;
		} else if(transform.parent.GetComponent<SpawnController>().minionCount == 3){
			tempSprite = threeMin;
		}
		if (transform.parent.GetComponent<SpawnController>().minionCount != minCount) {
			foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))){
				myRenderer = renderer;
			}
			myRenderer.sprite = tempSprite;
			minCount = transform.parent.GetComponent<SpawnController>().minionCount;
		}
	}
}
