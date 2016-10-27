using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;

public class ButtonManager : MonoBehaviour {
	#region Properties
	public string nextScene;
	public bool itsNotButton;
	public Scene GameScene;
	#endregion

	#region Methods
	public void GoTo(){
		if (XCI.GetButtonDown (XboxButton.A, XboxController.All)) { 
			PlayerPrefs.SetInt ("PlayerController", 0);
			SceneManager.LoadScene (nextScene);		
		} else if (KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.Second)){
			PlayerPrefs.SetInt ("PlayerController", 1);
			SceneManager.LoadScene (nextScene);
		}
	}

	void Update(){
		if (itsNotButton) {
			if (XCI.GetButtonDown (XboxButton.A, XboxController.All)) { 
				PlayerPrefs.SetInt ("PlayerController", 0);
				GoTo ();
			} else if (KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.Second)){
				PlayerPrefs.SetInt ("PlayerController", 1);
				print (PlayerPrefs.GetInt ("PlayerController"));
				GoTo ();		
			}
		}
	}
	#endregion
}
