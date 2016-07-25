using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class PlayerSelection : MonoBehaviour {

	#region Properties
	public XboxController Xcontroller;
	public KeyboardController Kcontroller;

	public bool isSet = false;
	#endregion
	
	#region Methods
	void Awake(){

	}
	#endregion
}
