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
	0 - AddScore -----> ok!
	4 - Death -----> ok!
	6 - Jump -----> ok!
	8 - Punch -----> ok!
	12 - Walk1
	13 - Walk2
	14 - Walk2

	Related to UI:
	1 - Back -----> ok!
	5 - Deselecting -----> ok!
	7 - PressStart -----> ok!
	9 - Selecting -----> ok!
	11 - UI -----> ok!
	2 - countdown finish -----> ok!
	3 - countdown start -----> ok!

	Related to Songs:
	10 - ThemeSong -----> ok!
	15 - WinnerSong -----> ok!

	*/

	void Awake () {
		if (GameObject.FindGameObjectsWithTag ("Audio").Length == 2) {
			Destroy (this.gameObject);
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

	void Update () {
		loopTheme ();
	}
}
