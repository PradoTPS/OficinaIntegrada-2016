using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;

public class SplashScreenText : MonoBehaviour {

	int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs ();
	public Text TriggerTxt;
	// Use this for initialization
	void Start () {
		if (queriedNumberOfCtrlrs == 1){
			TriggerTxt.text = "sgasdg";
		}else{
			TriggerTxt.text = "Press SPACE to continue";
		}
	}
}
