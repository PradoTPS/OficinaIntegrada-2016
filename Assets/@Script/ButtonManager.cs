using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class ButtonManager : MonoBehaviour {
	#region Properties
	public string nextScene;
	public bool isInput;
	public Scene GameScene;

	#endregion

	#region Methods
	public void GoTo(){
		SceneManager.LoadScene (nextScene);
	}

	void Update(){
		if (isInput) {
			if (XCI.GetButtonDown (XboxButton.A, XboxController.All) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.Second)) {
				GoTo ();

			}
		}
	}
	#endregion
}
