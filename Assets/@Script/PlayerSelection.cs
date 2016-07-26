using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class PlayerSelection : MonoBehaviour {

	#region Properties
	public XboxController Xcontroller;
	public KeyboardController Kcontroller;

	public bool isSet = false;
	public bool isKeyboard;
	#endregion
	
	#region Methods
	void Update(){
		if (isSet)
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}
	#endregion
}
