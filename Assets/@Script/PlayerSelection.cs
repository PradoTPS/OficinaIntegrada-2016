using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class PlayerSelection : MonoBehaviour {

	#region Properties
	public GameObject selectable1, selectable2, selectable3, selectable4;
	#endregion

	#region Methods
	void GettingReady(){
		if(XCI.GetButtonDown (XboxButton.A, XboxController.All) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First) || KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.Second)){
			
		}
	}
	#endregion
}
