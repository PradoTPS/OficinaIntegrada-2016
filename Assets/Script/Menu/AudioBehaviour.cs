using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
using KeyboardInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioBehaviour : MonoBehaviour {

	public AudioSource[] audios = new AudioSource[16];
	private	bool playResult;

	void Awake () {
		if (GameObject.FindGameObjectsWithTag ("Audio").Length == 2) {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (transform.gameObject);

		audios [4].volume = 1;
	}

	void loopTheme(){
		if (!audios [10].isPlaying) {
			audios [10].Play ();
		}

		if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Result" || SceneManager.GetActiveScene().name == "AnimTest" || SceneManager.GetActiveScene().name == "Testing") {			
			audios [10].Stop ();
		}

		if (SceneManager.GetActiveScene ().name != "Result") {
			audios [15].Stop ();
		}
	}

	void Update () {
		loopTheme ();
	}
}
