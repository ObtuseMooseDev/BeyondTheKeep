using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public float MAXHEALTH;
	public int MAXCASH;

	public Texture bground1Wins, bground2Wins; 

	public GameObject touchObject;
	public GameObject tower;

	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float waveWait;
	public float startWait;

	public bool paused = false;
	private bool checkWinner = false;
	public bool triggerPause = false;

	public Vector3 touchCoordinates;
	public TextMesh winner, p1Cash, p2Cash, p1Health, p2Health;
	public float team1Health, team2Health;
	public int team1Cash, team2Cash;
	public bool gameover = false;
	public bool p1MinionPlaced = false;
	public bool p2MinionPlaced = false;
	private float addCash;

	public Texture pauseButton, facebookButton, resumeButton, restartButton, exitButton, pauseBG;
	public Texture pauseButton2, facebookButton2, resumeButton2, restartButton2, exitButton2, pauseBG2;
	public Texture pauseTutorial, pauseTutBG;
	public Texture winnerBG, loserBG; 
	public Texture winnerBG2, loserBG2;

	public GameObject PauseMenu;
	public PracticeMode practiceMode;

	public bool changeAudio;

	public int GUIWIDTH, WINWIDTH;

	public bool menuUp;

	public bool isPractice;

	private GameSettings gameSettings;

	void Start(){
		paused = false;
		triggerPause = false;
		menuUp = false;
		addCash = 0.0f;
		team1Health = MAXHEALTH;
		team2Health = MAXHEALTH;
		team1Cash = MAXCASH;
		team2Cash = MAXCASH;
		changeAudio = false;
		if( Screen.height >= 1080 ) { GUIWIDTH = 64; WINWIDTH = 1080; } else { GUIWIDTH = 32; WINWIDTH = 600; }

		GameObject gameSettingsObject = GameObject.FindWithTag ("GameSettings");
		if (gameSettingsObject != null) {
			gameSettings = gameSettingsObject.GetComponent<GameSettings>();
			practiceMode = gameSettingsObject.GetComponent<PracticeMode>();
			Debug.Log (gameSettings.stuff);
		}

		if (isPractice) {
			togglePause();
		}
		/**
		if (gameSettings.unlockAll) {
			Debug.Log ("UNLOCK ALL");
		} else {
			Debug.Log ("YOU GOOFED");
		} **/

	}
	void Update(){
		pause ();
		if(!paused && !isPractice){
			addCash = addCash + 0.01f;
		} 
		if (team1Cash == 0 && team2Cash == 0) {
			team1Cash += 15;
			team2Cash += 15;
		}
		if (addCash >= 0.60f) {
			if(p1MinionPlaced) { team1Cash++; }
			if(p2MinionPlaced) { team2Cash++; }
			addCash = 0.0f;
		}
		updateCash();
		updateHealth ();
		if (team1Health == 0 && team2Health == 0) {
			updateWinner(0);
		}
		else if (team1Health <= 0) { 
			updateWinner(2);
		}
		else if (team2Health <= 0) {
			updateWinner(1);
		}
		for (int i = 0; i < Input.touchCount; ++i) {
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began) {
				Quaternion touchRotation = Quaternion.identity;
				touchCoordinates = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
				touchCoordinates.y = 0;

				Instantiate (touchObject, touchCoordinates, touchRotation);
			}
		}
		if (checkWinner) {

		}
		if (changeAudio) {
			GetComponentInChildren<SwitchAudio>().changeAudio = true;
			//GetComponentInChildren<switchAudio>().audio.Stop();
		}
	}
	/**
	void onTriggerEnter(Collider other){
		if(!checkWinner && other.tag == "Touch"){
			triggerPause = true;
		}
	} **/
	void pause(){
		if(Input.GetKeyDown("escape") || triggerPause == true){
			Debug.Log ("Before: "  + Time.timeScale );
			paused = togglePause();
			triggerPause = false;
			Debug.Log ("After: " + Time.timeScale );
		}
		
	}

	void OnGUI()
	{
		GUIStyle labelStyle = GUI.skin.label;

		if( !paused ){

			if(GUI.Button( ( new Rect(Screen.width/20 * 19.5f, Screen.height/20  ,GUIWIDTH,GUIWIDTH) ), pauseButton, labelStyle))
				paused = togglePause();
			if(GUI.Button( ( new Rect(Screen.width/60, Screen.height/20 * 19 ,GUIWIDTH,GUIWIDTH) ), pauseButton2, labelStyle))
				paused = togglePause();
		}
		if(paused)
		{
			if( !menuUp ){
				//Instantiate( PauseMenu, MenuPosition, MenuRotation );
				menuUp = true;
			}
			if(GUI.Button( ( new Rect(Screen.width/16 * 15, Screen.height/16 ,GUIWIDTH,GUIWIDTH) ), pauseButton, labelStyle))
				paused = togglePause();
			if(GUI.Button( ( new Rect(Screen.width/16, Screen.height/16 * 15 ,GUIWIDTH,GUIWIDTH) ), pauseButton2, labelStyle))
				paused = togglePause();
			GUILayout.Label("Game is paused!");

			if( !isPractice ){
				GUI.Label( (new Rect( Screen.width/2 + (12) ,Screen.height/2 - (528/2) ,512,512)) , pauseBG);
				if(GUI.Button( ( new Rect(Screen.width/2 + (125 + 124) , Screen.height/2-(156/2),128,128) ), resumeButton, labelStyle))
					paused = togglePause();
				if(GUI.Button( ( new Rect(Screen.width/2 + 75, Screen.height/2-64, 64, 128) ), restartButton, labelStyle)){
					paused = togglePause ();
					Application.LoadLevel(1); 
				}
				if(GUI.Button( ( new Rect(Screen.width/2 + (100 + 64), Screen.height/2-64, 64, 128) ), exitButton, labelStyle)){
					paused = togglePause ();
					Application.LoadLevel(0);
				}

				GUI.Label( (new Rect( Screen.width/2 - ((12)+512) ,Screen.height/2 - (528/2) ,512,512)) , pauseBG2);
				if(GUI.Button( ( new Rect(Screen.width/2 - ((125 + 124) + 128) , Screen.height/2 - (128/3),128,128) ), resumeButton2, labelStyle))
					paused = togglePause();
				if(GUI.Button( ( new Rect(Screen.width/2 - (75+64), Screen.height/2-64, 64, 128) ), restartButton2, labelStyle)){
					paused = togglePause ();
					Application.LoadLevel(Application.loadedLevel); 
				}
				if(GUI.Button( ( new Rect(Screen.width/2 - ((100 + 64) + 64), Screen.height/2-64, 64, 128) ), exitButton2, labelStyle)){
					paused = togglePause ();
					Application.LoadLevel(0);
				}
			} else if ( isPractice ) {
				GUI.Label( (new Rect( Screen.width/4 ,Screen.height/32 * 13,Screen.width / 7.5f ,Screen.height / 4.21875f)) , pauseTutBG);
				if(GUI.Button( ( new Rect(Screen.width/7 * 2.15f, Screen.height/16 * 7.25f,Screen.width / 15,Screen.height / 8.4375f) ), pauseTutorial, labelStyle)){
					paused = togglePause();
				}

				if(GUI.Button( ( new Rect(Screen.width/7 * 1.85f , Screen.height/16 * 7.25f, Screen.width/30, Screen.height/8.4375f) ), exitButton, labelStyle)){
					paused = togglePause ();
					Application.LoadLevel(0);
				}
			} 


		}
		if (checkWinner && !isPractice) {
			if( team2Health <= 0 ){
				//GUI.Label( (new Rect( Screen.width/2 - WINWIDTH/10 * 1.1f,0 ,WINWIDTH*1.3f,WINWIDTH*1.3f)) , winnerBG);
				//GUI.Label( (new Rect( Screen.width/2 - (WINWIDTH*1.1f) , 0 ,WINWIDTH*1.3f,WINWIDTH*1.3f)) , loserBG);
			} else {
				//GUI.Label( (new Rect( Screen.width/2 - WINWIDTH/10 * 1.1f,0 ,WINWIDTH*1.3f,WINWIDTH*1.3f)) , loserBG2);
				//GUI.Label( (new Rect( Screen.width/2 - (WINWIDTH*1.1f) , 0 ,WINWIDTH*1.3f,WINWIDTH*1.3f)) , winnerBG2);
			}
			//GUI.Label( (new Rect( Screen.width/2 + (12) ,Screen.height/2 - (528/2) ,512,512)) , pauseBG);

			if(GUI.Button( ( new Rect(0, 0, Screen.width/2, Screen.height/4 ) ), " ", labelStyle)){
				Application.LoadLevel(1); }
			if(GUI.Button( ( new Rect(0, Screen.height/2 +  Screen.height/4 , Screen.width/2, Screen.height/4) ), " ", labelStyle)){
				Application.LoadLevel(0);
			}

			if(GUI.Button( ( new Rect(Screen.width/2, 0, Screen.width/2, Screen.height/4) ), " ", labelStyle)){
				Application.LoadLevel(0); }
			if(GUI.Button( ( new Rect(Screen.width/2, Screen.height/2 + Screen.height/4, Screen.width/2, Screen.height/4 ) ), " ", labelStyle)){
				Application.LoadLevel(1);
			}
		}
	} 

	public bool togglePause()
	{
		if(Time.timeScale == 0.0000001f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0.0000001f;
			return(true);
		}
	}

	void updateWinner (int i)
	{
		if (!isPractice) {
						if (i == 0) {
								//winner.text = "Tie";
						} else if (i == 1) {
								checkWinner = true;
								team2Health = 0;
								updateHealth ();
								foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
										if (renderer.gameObject.tag == "P1Wins")
												renderer.enabled = true;
								}
								foreach (BoxCollider collider in gameObject.GetComponentsInChildren(typeof(BoxCollider))) {
										if (collider.gameObject.tag == "P1Wins")
												collider.enabled = true;
								}
						} else if (i == 2) {
								checkWinner = true;
								team1Health = 0;
								updateHealth ();
								//winner.text = "Player 2 Wins!";
								foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren(typeof(SpriteRenderer))) {
										if (renderer.gameObject.tag == "P2Wins")
												renderer.enabled = true;
								}
								foreach (BoxCollider collider in gameObject.GetComponentsInChildren(typeof(BoxCollider))) {
										if (collider.gameObject.tag == "P2Wins")
												collider.enabled = true;
								}
						}
				}
		//Application.LoadLevel (Application.loadedLevel);
	}

	void updateCash ()
	{
		if (!paused) {
				p1Cash.text = "" + team1Cash;
				p2Cash.text = "" + team2Cash;
				}
	}
	void updateHealth()
	{
		if (team2Health > MAXHEALTH)
			p1Health.text = "INFINITY";
		else if(team2Health >= 0)
			p1Health.text = "" + (team2Health/MAXHEALTH)*100 +"%";
		if( team1Health > MAXHEALTH)
			p2Health.text = "INFINITY";
		else if(team1Health >= 0)
			p2Health.text = "" + (team1Health/MAXHEALTH)*100 +"%";
	}

}
