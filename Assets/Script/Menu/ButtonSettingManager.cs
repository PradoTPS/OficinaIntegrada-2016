using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;

public class ButtonSettingManager : MonoBehaviour {
	#region Properties
	private bool canUseAxis = true;

	private int numberRound = 3;

	public Text buttonText;
	public Button btn;
	#endregion
	
	#region Methods
	void Start(){
		SettingText ();
	}

	public void SettingText(){
		buttonText.text = numberRound.ToString ();
	}

	public void SettingChange(){
		if (canUseAxis) {
			if (XCI.GetAxisRaw (XboxAxis.LeftStickX, XboxController.All) == 1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.First) == 1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.Second) == 1 && numberRound < 15) {
				canUseAxis = false;
				numberRound++;
				SettingText ();	
			}
			if (XCI.GetAxisRaw (XboxAxis.LeftStickX, XboxController.All) == -1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.First) == -1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.Second) == -1 && numberRound > 3) {
				canUseAxis = false;
				numberRound--;
				SettingText ();
			}
		}

		if (XCI.GetAxisRaw (XboxAxis.LeftStickX, XboxController.All) == 0 && KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.First) == 0 && KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.Second) == 0) {
			canUseAxis = true;
		}
	}

	void Update(){
		if (EventSystem.current.currentSelectedGameObject.name == btn.gameObject.name) {
			SettingChange ();
		}
		PlayerPrefs.SetInt ("NumberOfRounds", numberRound);
	}
	#endregion
}
