using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;
using System.Linq;

public class SelectionController : MonoBehaviour {
	#region Properties
	public List<Sprite> AblePlayers = new List<Sprite> ();
	public List<Sprite> NotAblePlayers = new List<Sprite> ();
	public List<Sprite> AbleArrows = new List<Sprite> ();
	public List<Sprite> NotAbleArrows = new List<Sprite> ();
	public List<Sprite> AbleSelection = new List<Sprite> ();
	public List<Sprite> NotAbleSelection = new List<Sprite> ();

	public List<GameObject> playersAreRandom = new List<GameObject>();

	[HideInInspector]
	public bool nobodyReady;
	public GameObject[] selectables = new GameObject[4];

	private List<XboxController> ableXboxList = new List<XboxController>();
	private List<KeyboardController> ableKeyboardList = new List<KeyboardController>();
	private List<XboxController> enableXboxList = new List<XboxController> ();
	private List<KeyboardController> enableKeyboardList = new List<KeyboardController> ();
	#endregion

	#region Methods
	void Awake(){
		ableXboxList.Add(XboxController.First);
		ableXboxList.Add(XboxController.Second);
		ableXboxList.Add(XboxController.Third);
		ableXboxList.Add(XboxController.Fourth);

		ableKeyboardList.Add(KeyboardController.First);
		ableKeyboardList.Add(KeyboardController.Second);

		for (int i = 0; i < selectables.Length; i++) {
			selectables [i] = GameObject.Find("Selectable" + (i + 1).ToString());
			PlayerPrefs.SetString ("Player " + (i + 1).ToString (), "none");
			PlayerPrefs.SetString ("Player " + (i + 1).ToString () + " " + "Player Random", "none");
		}
	}

	public void SpriteNotAble (Sprite player, Sprite arrow, Sprite selection){
		Sprite temp;
		temp = player; NotAblePlayers.Add (temp); AblePlayers.Remove (temp);
		temp = arrow; NotAbleArrows.Add (temp); AbleArrows.Remove (temp);
		temp = selection; NotAbleSelection.Add (temp); AbleSelection.Remove (temp);
	}

	public void SpriteAble (Sprite player, Sprite arrow, Sprite selection){
		Sprite temp;
		temp = player; AblePlayers.Add (temp); NotAblePlayers.Remove (temp);
		temp = arrow; AbleArrows.Add (temp); NotAbleArrows.Remove (temp);
		temp = selection; AbleSelection.Add (temp); NotAbleSelection.Remove (temp);
	}

	public void Selecting(GameObject selected){
		for (int i = 0; i < ableXboxList.Count; i++) {
			if (XCI.GetButtonDown (XboxButton.A, ableXboxList[i])) {
				ControlSet (selected, ableXboxList [i]);
				selected.GetComponent<PlayerSelection> ().confirmText.enabled = false;
			}
		}
		
		for (int i = 0; i < ableKeyboardList.Count; i++) {
			if (KCI.GetButtonDown (KeyboardButton.Jump, ableKeyboardList[i])) {
				ControlSet (selected, ableKeyboardList[i]);
				selected.GetComponent<PlayerSelection> ().confirmText.enabled = false;
			}
		}
	}

	void Deselecting(GameObject selected){
		for (int i = 0; i < enableXboxList.Count; i++) {
			if (XCI.GetButtonDown (XboxButton.B, enableXboxList[i]) && !selected.GetComponent<PlayerSelection>().isKeyboard && selected.GetComponent<PlayerSelection>().Xcontroller == enableXboxList[i]) {
				ControlUnset (selected, enableXboxList [i]);
				selected.GetComponent<PlayerSelection> ().confirmText.enabled = true;
			}
		}

		for (int i = 0; i < enableKeyboardList.Count; i++) {
			if (KCI.GetButtonDown (KeyboardButton.Action, enableKeyboardList[i]) && selected.GetComponent<PlayerSelection>().isKeyboard && selected.GetComponent<PlayerSelection>().Kcontroller == enableKeyboardList[i]) {
				ControlUnset (selected, enableKeyboardList[i]);
				selected.GetComponent<PlayerSelection> ().confirmText.enabled = true;
			}
		}
	}

	void ControlSet(GameObject selected, XboxController xbxCtrl){
		PlayerNumber(selected, ref selected.GetComponent<PlayerSelection>().playerNumber);
		selected.GetComponent<PlayerSelection> ().Xcontroller = xbxCtrl;
		selected.GetComponent<PlayerSelection> ().isSet = true;
		selected.GetComponent<PlayerSelection> ().isKeyboard = false;
		selected.GetComponent<SpriteRenderer> ().enabled = true;
		selected.GetComponent<PlayerSelection> ().Search ("Arrow").GetComponent<SpriteRenderer> ().enabled = true;
		selected.GetComponent<PlayerSelection> ().Search ("Text").GetComponent<TextMesh> ().text = selected.GetComponent<PlayerSelection>().playerNumber;
		ableXboxList.Remove (xbxCtrl);
		enableXboxList.Add (xbxCtrl);
	}

	void ControlSet(GameObject selected, KeyboardController kbrdCtrl){
		PlayerNumber(selected, ref selected.GetComponent<PlayerSelection>().playerNumber);
		selected.GetComponent<PlayerSelection> ().Kcontroller = kbrdCtrl;
		selected.GetComponent<PlayerSelection> ().isSet = true;
		selected.GetComponent<PlayerSelection> ().isKeyboard = true;
		selected.GetComponent<SpriteRenderer> ().enabled = true;
		selected.GetComponent<PlayerSelection> ().Search ("Arrow").GetComponent<SpriteRenderer> ().enabled = true;
		selected.GetComponent<PlayerSelection> ().Search ("Text").GetComponent<TextMesh> ().text = selected.GetComponent<PlayerSelection>().playerNumber;
		ableKeyboardList.Remove (kbrdCtrl);
		enableKeyboardList.Add (kbrdCtrl);
	}

	void ControlUnset(GameObject selected, XboxController xbxCtrl){
		selected.GetComponent<PlayerSelection> ().playerNumber = "";
		selected.GetComponent<PlayerSelection> ().isSet = false;
		selected.GetComponent<SpriteRenderer> ().enabled = false;
		selected.GetComponent<SpriteRenderer> ().sprite = AblePlayers [0];
		selected.GetComponent<PlayerSelection> ().Search ("Arrow").GetComponent<SpriteRenderer> ().enabled = false;
		selected.GetComponent<PlayerSelection> ().Search ("Arrow").GetComponent<SpriteRenderer> ().sprite = AbleArrows [0];
		selected.GetComponent<PlayerSelection> ().Search ("Text").GetComponent<TextMesh> ().text = "";
		enableXboxList.Remove (xbxCtrl);
		ableXboxList.Add (xbxCtrl);
	}

	void ControlUnset(GameObject selected, KeyboardController kbrdCtrl){
		selected.GetComponent<PlayerSelection> ().playerNumber = "";
		selected.GetComponent<PlayerSelection> ().isSet = false;
		selected.GetComponent<SpriteRenderer> ().enabled = false;
		selected.GetComponent<PlayerSelection> ().isKeyboard = false;
		selected.GetComponent<SpriteRenderer> ().sprite = AblePlayers [0];
		selected.GetComponent<PlayerSelection> ().Search ("Arrow").GetComponent<SpriteRenderer> ().enabled = false;
		selected.GetComponent<PlayerSelection> ().Search ("Arrow").GetComponent<SpriteRenderer> ().sprite = AbleArrows [0];
		selected.GetComponent<PlayerSelection> ().Search ("Text").GetComponent<TextMesh> ().text = "";
		enableKeyboardList.Remove (kbrdCtrl);
		ableKeyboardList.Add (kbrdCtrl);
	}

	void ReadyVerifier(){
		for (int i = 0; i < selectables.Length; i++) {
			if (selectables [i].GetComponent<PlayerSelection> ().isSet) {
				nobodyReady = false;
				break;
			}
			nobodyReady = true;
		}
	}

	void PlayerNumber(GameObject selected, ref string pn){
		for (int i = 0; i < selectables.Length; i++) {
			if (selected == selectables [i]) {
				pn = "Player " + (i + 1).ToString ();
			}
		}
	}

	public void SettingRandom(){
		for (int i = 0; i < playersAreRandom.Count; i++) {
			int randomNumber = Mathf.FloorToInt(Random.Range (0, (AblePlayers.Count - 1)*10)/10);
			int randomOrder = int.Parse(AblePlayers[randomNumber].name.Substring (7));

			NotAblePlayers.Add (AblePlayers[randomNumber]);
			AblePlayers.Remove (AblePlayers[randomNumber]);

			PlayerPrefs.SetInt(playersAreRandom[i].GetComponent<PlayerSelection>().playerNumber + " " + "Random Player", randomOrder);
			print (randomOrder);
		}
	}

	void AbleListsOrganize (){
		AbleArrows = AbleArrows.OrderBy( go => go.name ).ToList();
		AblePlayers = AblePlayers.OrderBy(go => go.name).ToList();
		AbleSelection = AbleSelection.OrderBy(go => go.name).ToList();
	}

	void Update() {
		AbleListsOrganize ();
	}

	void FixedUpdate(){
		for (int i = 0; i < selectables.Length; i++) {
			if (!selectables [i].GetComponent<PlayerSelection> ().ready) {
				Deselecting (selectables [i]);
			}
		}

		ReadyVerifier ();
	}

	void LateUpdate(){
		for (int i = 0; i < selectables.Length; i++) {
			if (!selectables [i].GetComponent<PlayerSelection> ().isSet) {
				Selecting(selectables[i]);
				break;
			}
		}
	}
	#endregion
}