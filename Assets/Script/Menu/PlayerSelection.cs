using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;

public class PlayerSelection : MonoBehaviour {
	#region Properties
	public XboxController Xcontroller;
	public KeyboardController Kcontroller;

	public List<Sprite> playersToChoose = new List<Sprite> ();
	public List<Sprite> arrows = new List<Sprite> ();
	public List<Sprite> selection = new List<Sprite> ();

	public Text confirmText;

	public string playerNumber;

	public bool isSet = false;
	public bool isKeyboard;
	public bool ready = false;
	private bool canUseAxis = true;
	#endregion
	
	#region Methods
	void Start(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = playersToChoose [0];
		Search("Arrow").GetComponent<SpriteRenderer> ().sprite = arrows [0];
	}

	void ChangeCurrentSprite(string direction){
		for (int i = 0; i < playersToChoose.Count; i++) {
			if (gameObject.GetComponent<SpriteRenderer> ().sprite == playersToChoose [i]) {
				if (direction == "next" && i + 1 <= playersToChoose.Count - 1) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = playersToChoose [i + 1];
					Search("Arrow").GetComponent<SpriteRenderer> ().sprite = arrows [i + 1];
				} else if (direction == "previous" && i - 1 >= 0) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = playersToChoose [i - 1];
					Search("Arrow").GetComponent<SpriteRenderer> ().sprite = arrows [i - 1];
				}
				break;
			}
		}
	}

	void Passing(){
		if (!isKeyboard && isSet && !ready) {
			if (XCI.GetAxisRaw (XboxAxis.LeftStickX, Xcontroller) == 1f && canUseAxis) {
				canUseAxis = false;
				ChangeCurrentSprite ("next");
			} else if (XCI.GetAxisRaw (XboxAxis.LeftStickX, Xcontroller) == -1f && canUseAxis) {
				canUseAxis = false;
				ChangeCurrentSprite ("previous");
			} else if (XCI.GetAxisRaw (XboxAxis.LeftStickX, Xcontroller) == 0f) {
				canUseAxis = true;
			}
		} else if (isKeyboard && isSet && !ready) {
			if (KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller) == 1f && canUseAxis) {
				canUseAxis = false;
				ChangeCurrentSprite ("next");
			} else if (KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller) == -1f && canUseAxis) {
				canUseAxis = false;
				ChangeCurrentSprite ("previous");
			} else if (KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller) == 0f) {
				canUseAxis = true;
			}
		}
	}

	void Choosing(){
		if (!isKeyboard && isSet && !ready) {
			if (XCI.GetButtonDown (XboxButton.A, Xcontroller)) {
				ready = true;
				PlayerPrefs.SetString (playerNumber, gameObject.GetComponent<SpriteRenderer> ().sprite.name);
				GiveControll ();

				if (gameObject.GetComponent<SpriteRenderer> ().sprite.name != "Player Random") {
					Search ("Selection").GetComponent<SpriteRenderer> ().sprite = selection [int.Parse(gameObject.GetComponent<SpriteRenderer> ().sprite.name.Substring (7))];
				} else {
					Search ("Selection").GetComponent<SpriteRenderer> ().sprite = selection [selection.Count - 1];
				}
			}
		} else if (isKeyboard && isSet && !ready) {
			if (KCI.GetButtonDown (KeyboardButton.Jump, Kcontroller)) {
				ready = true;
				PlayerPrefs.SetString (playerNumber, gameObject.GetComponent<SpriteRenderer> ().sprite.name);
				GiveControll ();

				if (gameObject.GetComponent<SpriteRenderer> ().sprite.name != "Player Random") {
					Search ("Selection").GetComponent<SpriteRenderer> ().sprite = selection [int.Parse(gameObject.GetComponent<SpriteRenderer> ().sprite.name.Substring (7))];
				} else {
					Search ("Selection").GetComponent<SpriteRenderer> ().sprite = selection [selection.Count - 1];
				}
			}
		}
	}

	void Unchoosing(){
		if (!isKeyboard && isSet && ready) {
			if (XCI.GetButtonDown (XboxButton.B, Xcontroller)) {
				ready = false;
				Search ("Selection").GetComponent<SpriteRenderer> ().sprite = selection [0];
				PlayerPrefs.SetString (playerNumber, "none");
			}
		} else if (isKeyboard && isSet && ready) {
			if (KCI.GetButtonDown (KeyboardButton.Action, Kcontroller)) {
				ready = false;
				Search ("Selection").GetComponent<SpriteRenderer> ().sprite = selection [0];
				PlayerPrefs.SetString (playerNumber, "none");
			}
		}
	}

	void GiveControll(){
		if (isKeyboard) {
			switch (Kcontroller) {
				case KeyboardController.First:
					PlayerPrefs.SetInt (playerNumber + " Controller", 1);
					PlayerPrefs.SetString(playerNumber + " isKeyboard", "true");
					break;
				case KeyboardController.Second:
					PlayerPrefs.SetInt (playerNumber + " Controller", 2);
					PlayerPrefs.SetString(playerNumber + " isKeyboard", "true");
					break;
			}
		} else {
			switch (Xcontroller) {
				case XboxController.First:
					PlayerPrefs.SetInt (playerNumber + " Controller", 1);
					PlayerPrefs.SetString(playerNumber + " isKeyboard", "false");
					break;
				case XboxController.Second:
					PlayerPrefs.SetInt (playerNumber + " Controller", 2);
					PlayerPrefs.SetString(playerNumber + " isKeyboard", "false");
					break;
				case XboxController.Third:
					PlayerPrefs.SetInt (playerNumber + " Controller", 3);
					PlayerPrefs.SetString(playerNumber + " isKeyboard", "false");
					break;
				case XboxController.Fourth:
					PlayerPrefs.SetInt (playerNumber + " Controller", 4);
					PlayerPrefs.SetString(playerNumber + " isKeyboard", "false");
					break;
			}
		}
	}

	public Transform Search(string name){
		return Search(gameObject.transform , name);
	}

	public Transform Search(Transform target, string name){
		if (target.name == name){
			return target;
		}

		for (int i = 0; i < target.childCount; ++i) {
			Transform result = Search(target.GetChild(i), name);

			if (result != null) {
				return result;
			}
		}

		return null;
	}

	void FixedUpdate(){
		Passing ();
		Choosing ();
	}

	void LateUpdate(){
		Unchoosing ();
	}
	#endregion
}