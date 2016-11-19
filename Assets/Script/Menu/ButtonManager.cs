using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, ISelectHandler {
	#region Properties
	public string nextScene;
	public bool itsNotButton;
	public Scene GameScene;
	private bool hasSounded;
	#endregion

	#region Methods
	public void GoTo(){
		if (SceneManager.GetActiveScene ().name == "SplashScreen") {
			GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [7].Play ();
		} else {
			GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [9].Play ();
		}

		if (XCI.GetButtonDown (XboxButton.A, XboxController.First)) { 
			PlayerPrefs.SetInt ("PlayerController", 0);
		}

		if (KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First)){
			PlayerPrefs.SetInt ("PlayerController", 1);
		}

		SceneManager.LoadScene (nextScene);
	}
		
	public void OnSelect(BaseEventData eventData) {
		if (hasSounded) {
			GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour>().audios[11].Play();		
		}
	}

	void Update(){
		if (itsNotButton) {
			if (XCI.GetButtonDown (XboxButton.A)) { 
				PlayerPrefs.SetInt ("PlayerController", 0);
				GoTo ();
			} 
			if (KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.Second)){
				PlayerPrefs.SetInt ("PlayerController", 1);
				GoTo ();		
			}
		}
	}

	void LateUpdate(){
		hasSounded = true;
	}
	#endregion
}
