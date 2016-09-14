using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class BackToScene : MonoBehaviour {
	#region Properties
	public string sceneToGoBack;
	#endregion

	#region Methods
	void GoingBack(){
		if (XCI.GetButtonDown (XboxButton.B, XboxController.All) || KCI.GetButtonDown(KeyboardButton.Action, KeyboardController.First) || KCI.GetButtonDown(KeyboardButton.Action, KeyboardController.Second)) {
			if (SceneManager.GetActiveScene ().name == "CharactersSelection") {
				if (GameObject.Find ("SelectionHandler").GetComponent<SelectionController> ().nobodyReady){
					SceneManager.LoadScene (sceneToGoBack);
				}
			} else {
				SceneManager.LoadScene (sceneToGoBack);
			}
		}
	}

	void Update(){
		GoingBack ();
	}
	#endregion
}
