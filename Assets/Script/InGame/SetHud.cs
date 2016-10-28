using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetHud : MonoBehaviour {
	#region Properties
	private GameObject[] playersHUD = new GameObject[4];
	public Sprite[] HUDImages;
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < playersHUD.Length; i++) {
			playersHUD [i] = GameObject.Find ("Player" + (i + 1).ToString () + " " + "HUD");
		}

		SettingHUD ();
	}

	void SettingHUD(){
		PlayerPrefs.SetInt ("numPlayers", 0);
		for (int i = 0; i < playersHUD.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none" && PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "Player Random") {
				DefineHUDImage (int.Parse(PlayerPrefs.GetString ("Player " + (i + 1).ToString ()).Substring (7)) - 1, i);
				PlayerPrefs.SetInt ("numPlayers", PlayerPrefs.GetInt("numPlayers")+1);
			} else if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) == "Player Random") {
				DefineHUDImage (PlayerPrefs.GetInt("Player " + (i + 1).ToString() + " " + "Random Player"), i);
			}
		}
	}

	void DefineHUDImage(int playerPrefs, int currentNumber){
		playersHUD[currentNumber].GetComponent<Image>().sprite = HUDImages[playerPrefs];
		playersHUD [currentNumber].GetComponent<Image> ().color = new Vector4 (255, 255, 255, 255);
	}
	#endregion
}
