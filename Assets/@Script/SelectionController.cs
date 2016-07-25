using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class SelectionController : MonoBehaviour {

	#region Properties
	public GameObject selectable1, selectable2, selectable3, selectable4;
	#endregion

	#region Methods
	void GettingReady(){
		if(XCI.GetButtonDown (XboxButton.A, XboxController.First) || 
		   XCI.GetButtonDown (XboxButton.A, XboxController.Second)|| 
		   XCI.GetButtonDown (XboxButton.A, XboxController.Third) || 
		   XCI.GetButtonDown (XboxButton.A, XboxController.Fourth)||
		   KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.First) || 
		   KCI.GetButtonDown (KeyboardButton.Jump, KeyboardController.Second)){

		}
	}
	#endregion
}
