using UnityEngine;
using System.Collections;

public class switchBackground : MonoBehaviour {
	private GameSettings gameSettings;
	public string menuStyle, backgroundStyle; 
	public Sprite lightBlue, red, yellow;
	public Sprite lightblueStart, redStart, yellowStart;
	public Sprite lightblueStartPressed, redStartPressed, yellowStartPressed;

	public Sprite space, fire, gem, velvet;

	private SpriteRenderer myRenderer;
	// Use this for initialization
	void Start () {
		menuStyle = "default";
		backgroundStyle = "space";
		GameObject gameSettingsObject = GameObject.FindWithTag ("GameSettings");
		if (gameSettingsObject != null) {
			gameSettings = gameSettingsObject.GetComponent<GameSettings>();
			Debug.Log (gameSettings.stuff);
		}
	}
	
	// Update is called once per frame
	void Update () {
				if (tag == "CurrentMenuStyles") {
						if (gameSettings.currentMenuStyle != menuStyle) {
								menuStyle = gameSettings.currentMenuStyle;

								if (menuStyle == "MS_LightBlue") {
										foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
												if (renderer.gameObject.tag == tag) {
														myRenderer = renderer;
												}
										}
										myRenderer.sprite = lightBlue;
								} else if (menuStyle == "MS_Red") {
										foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
												if (renderer.gameObject.tag == tag) {
														myRenderer = renderer;
												}
										}
										myRenderer.sprite = red;
								} else if (menuStyle == "MS_Yellow") {
										foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
												if (renderer.gameObject.tag == tag) {
														myRenderer = renderer;
												}
										}
										myRenderer.sprite = yellow;
								}
						} 
		} else if (tag == "Background") {
			if (gameSettings.currentBackground != backgroundStyle) {
				backgroundStyle = gameSettings.currentBackground;
				
				if (backgroundStyle == "space") {
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
						if (renderer.gameObject.tag == tag) {
							myRenderer = renderer;
						}
					}
					myRenderer.sprite = space;
				} else if (backgroundStyle == "BG_Flame") {
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
						if (renderer.gameObject.tag == tag) {
							myRenderer = renderer;
						}
					}
					myRenderer.sprite = fire;
				} else if (backgroundStyle == "BG_Gem") {
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
						if (renderer.gameObject.tag == tag) {
							myRenderer = renderer;
						}
					}
					myRenderer.sprite = gem;
				} else if (backgroundStyle == "BG_Velvet") {
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
						if (renderer.gameObject.tag == tag) {
							myRenderer = renderer;
						}
					}
					myRenderer.sprite = velvet;
				}
			} 
		} else if (tag == "Start") {
			if (gameSettings.currentMenuStyle != menuStyle) {
				menuStyle = gameSettings.currentMenuStyle;
				
				if (menuStyle == "MS_LightBlue") {
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
						if (renderer.gameObject.tag == tag) {
							myRenderer = renderer;
						}
						renderer.gameObject.GetComponent<DestroyByContact>().pressed = lightblueStartPressed;
					}
					myRenderer.sprite = lightblueStart;
				} else if (menuStyle == "MS_Red") {
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
						if (renderer.gameObject.tag == tag) {
							myRenderer = renderer;
						}
						renderer.gameObject.GetComponent<DestroyByContact>().pressed = redStartPressed;
					}
					myRenderer.sprite = redStart;
				} else if (menuStyle == "MS_Yellow") {
					foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
						if (renderer.gameObject.tag == tag) {
							myRenderer = renderer;
						}
						renderer.gameObject.GetComponent<DestroyByContact>().pressed = yellowStartPressed;
					}
					myRenderer.sprite = yellowStart;
				}
			}
		}
	}
}
