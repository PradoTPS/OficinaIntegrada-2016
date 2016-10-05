using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;

public class SplashScreenText : MonoBehaviour {

	public Text TriggerTxt;

	// Use this for initialization
	void Awake () {
		if (XCI.GetNumPluggedCtrlrs () >= 1){
			TriggerTxt.text = "Press A to continue";
		} else {
			TriggerTxt.text = "Press SPACE to continue";
		}
	}
}
