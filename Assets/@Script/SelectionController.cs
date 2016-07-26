using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;

public class SelectionController : MonoBehaviour {

	#region Properties
	public GameObject[] selectables = new GameObject[4];

	private List<XboxController> xboxList = new List<XboxController>() {
		XboxController.First,
		XboxController.Second,
		XboxController.Third,
		XboxController.Fourth
	};

	private List<KeyboardController> keyboardList = new List<KeyboardController>() {
		KeyboardController.First,
		KeyboardController.Second
	};
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < selectables.Length; i++) {
			selectables [i] = GameObject.Find("Selectable" + (i + 1).ToString());
		}
	}

	void Selecting(GameObject selected){
		for (int i = 0; i < xboxList.Count; i++) {
			if (XCI.GetButtonDown (XboxButton.A, xboxList[i])) {
				ControlSet (selected, xboxList [i]);
			}
		}

		for (int i = 0; i < keyboardList.Count; i++) {
			if (KCI.GetButtonDown (KeyboardButton.Jump, keyboardList[i])) {
				ControlSet (selected, keyboardList[i]);
			}
		}
	}

	void ControlSet(GameObject selected, XboxController xbxCtrl){
		selected.GetComponent<PlayerSelection> ().Xcontroller = xbxCtrl;
		selected.GetComponent<PlayerSelection> ().isSet = true;
		selected.GetComponent<PlayerSelection> ().isKeyboard = false;
		xboxList.Remove (xbxCtrl);
	}

	void ControlSet(GameObject selected, KeyboardController kbrdCtrl){
		selected.GetComponent<PlayerSelection> ().Kcontroller = kbrdCtrl;
		selected.GetComponent<PlayerSelection> ().isSet = true;
		selected.GetComponent<PlayerSelection> ().isKeyboard = true;
		keyboardList.Remove (kbrdCtrl);
	}

	void Update(){
		Debug.LogFormat ("{0}, {1}, {2}, {3}", selectables[0].GetComponent<PlayerSelection> ().isSet, selectables[1].GetComponent<PlayerSelection> ().isSet, selectables[2].GetComponent<PlayerSelection> ().isSet, selectables[3].GetComponent<PlayerSelection> ().isSet);

		for (int i = 0; i < selectables.Length; i++) {
			if (!selectables [i].GetComponent<PlayerSelection> ().isSet) {
				Selecting(selectables[i]);
				break;
			}
		}
	}
	#endregion
}
