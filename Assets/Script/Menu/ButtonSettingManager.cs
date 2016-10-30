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

	public int minValue;
	public int maxValue;
	private int numberRound = 3;

	public Image arrowLeft;
	public Image arrowRight;
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
			if ((XCI.GetAxisRaw (XboxAxis.LeftStickX, XboxController.All) == 1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.First) == 1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.Second) == 1) && numberRound < maxValue) {
				canUseAxis = false;
				numberRound++;
				GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [5].Play ();
				SettingText ();	
			}
			if ((XCI.GetAxisRaw (XboxAxis.LeftStickX, XboxController.All) == -1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.First) == -1 || KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.Second) == -1) && numberRound > minValue) {
				canUseAxis = false;
				numberRound--;
				GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [5].Play ();
				SettingText ();
			}
		}

		if (XCI.GetAxisRaw (XboxAxis.LeftStickX, XboxController.All) == 0 && KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.First) == 0 && KCI.GetAxisRaw (KeyboardAxis.Horizontal, KeyboardController.Second) == 0) {
			canUseAxis = true;
		}
	}

	public void ShowArrows(){
		if(numberRound == minValue){
			arrowLeft.enabled = false;
		} else {
			arrowLeft.enabled = true;
		}
		
		if (numberRound == maxValue) {
			arrowRight.enabled = false;
		} else {
			arrowRight.enabled = true;
		}
	}

	void Update(){
		if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == btn.gameObject.name) {
			SettingChange ();
		}
		PlayerPrefs.SetInt ("NumberOfRounds", numberRound);
		ShowArrows();
	}
	#endregion
}
