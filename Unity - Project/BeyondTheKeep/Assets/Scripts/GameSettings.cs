using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {
	public bool unlockAll = false;
	public string stuff = "huh";

	public string currentMenuStyle;
	public string currentBackground; 

	public bool BUY_MS_LightBlue, BUY_MS_Red, BUY_MS_Yellow; 
	public bool BUY_BG_Flame, BUY_BG_Gem, BUY_BG_Velvet; 

	public bool poisonUnlock = false;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		currentMenuStyle = "defualt";
		currentBackground = "space";
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setPurchased (string buyID)
	{
		if (buyID == "MS_LightBlue") {
			BUY_MS_LightBlue = true;
		} else if ( buyID == "MS_Red" ){
			BUY_MS_Red = true;
		} else if ( buyID == "MS_Yellow"){
			BUY_MS_Yellow = true;
		} else if ( buyID == "BG_Flame" ) {
			BUY_BG_Flame = true;
		} else if ( buyID == "BG_Gem" ){
			BUY_BG_Gem = true;
		} else if ( buyID == "BG_Velvet"){
			BUY_BG_Velvet = true;
		} else if ( buyID == "MT_Poison"){

		}
	}

	public bool checkPurchased (string buyID)
	{
		if (buyID == "MS_LightBlue") {
			if( BUY_MS_LightBlue == true ) { return true; } else { return false; }
		} else if ( buyID == "MS_Red" ){
			if( BUY_MS_Red == true ) { return true; } else { return false; }
		} else if ( buyID == "MS_Yellow"){ 
			if (BUY_MS_Yellow == true) { return true; } else { return false; }
		} else if ( buyID == "BG_Flame"){ 
			if (BUY_BG_Flame == true) { return true; } else { return false; }
		} else if ( buyID == "BG_GEM"){ 
			if (BUY_BG_Gem == true) { return true; } else { return false; }
		} else if ( buyID == "BG_Velvet"){ 
			if (BUY_BG_Velvet == true) { return true; } else { return false; }
		} else {
			return false; 

		}
	}

	public void changeCurrentMenuStyle( string newId ) {
		currentMenuStyle = newId;
	}
	public void changeCurrentBackground ( string newId ){
		currentBackground = newId;
	}
	public void unlockPoison( string newId){
		poisonUnlock = true;
	}
}
