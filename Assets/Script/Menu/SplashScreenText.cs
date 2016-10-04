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
    public 
	// Use this for initialization
	void Start () {
		if (queriedNumberOfCtrlrs == 1){
			TriggerTxt.text = "Press A to continue";
		}else{
			TriggerTxt.text = "Press SPACE to continue";
		}
	}
}
