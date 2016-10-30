using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using XboxCtrlrInput;
using KeyboardInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioBehaviour : MonoBehaviour {

	public AudioSource[] audios = new AudioSource[16];
	private	bool playResult;

	/*
	Related to Player:
	0 - AddScore
	4 - Death
	6 - Jump
	8 - Punch
	12 - Walk1
	13 - Walk2
	14 - Walk2

	Related to UI:
	1 - Back -----> ok!
	5 - Deselecting
	7 - PressStart -----> ok!
	9 - Selecting -----> ok!
	11 - UI -----> ok!
	2 - countdown finish 
	3 - countdown start

	Related to Songs:
	10 - ThemeSong -----> ok!
	15 - WinnerSong -----> ok!

	*/

	void Awake () {
		if (GameObject.FindGameObjectsWithTag ("Audio").Length == 2) {
			Destroy (this.gameObject);
		} else if (SceneManager.GetActiveScene ().name == "Result") {
			playResult = true;
		}
		DontDestroyOnLoad (transform.gameObject);				
	}

	void loopTheme(){
		if (!audios [10].isPlaying) {
			audios [10].Play ();
		}

		if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Result") {			
			audios [10].Stop ();
		}
	}

	void winTheme(){
		if (!audios [15].isPlaying && SceneManager.GetActiveScene().name == "Result" && playResult) {
			audios [15].Play ();
			playResult = false;
		}
	}

	void pressStart(){
		if (SceneManager.GetActiveScene().name == "SplashScreen" && (XCI.GetButtonDown (XboxButton.A, XboxController.All) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.Second) ) ) {
			audios [7].Play ();
		}
	}

	void Update () {
		loopTheme ();
		winTheme ();
		pressStart ();
	}
}
