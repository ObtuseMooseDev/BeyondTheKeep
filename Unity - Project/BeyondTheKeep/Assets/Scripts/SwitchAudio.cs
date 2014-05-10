using UnityEngine;
using System.Collections;

public class SwitchAudio : MonoBehaviour {
	
	public AudioClip UnrulySpeed;
	private AudioSource myAudio;
	public bool changeAudio, onlyOnce;
	//private AudioClip audioclip;
	void Start(){
		onlyOnce = true;
	}
	
	void Update(){
		if( changeAudio && onlyOnce ){
			CheckSongState("SpeedUp");
			onlyOnce = false;
		}
	}
	
	void CheckSongState(string song){
		if(song == "SpeedUp"){
			foreach (AudioSource audio in gameObject.GetComponentsInChildren(typeof(AudioSource))){
				if(audio.gameObject.tag == "SongSwitcher"){
					myAudio = audio;
					myAudio.clip = UnrulySpeed;
					myAudio.Play();
				}
			}
		}
	}
}
