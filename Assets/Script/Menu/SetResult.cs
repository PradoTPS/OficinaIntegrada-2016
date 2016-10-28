using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetResult : MonoBehaviour {
	#region Properties
	public GameObject[] Score = new GameObject[4];
	public GameObject[] players;
	public Image winnerImage;
	public Text winnerText;

	private int numPlayers;

	private string winnerName;
	#endregion

	#region Methods
	void Start () {
		SettingPlayers ();
	}

	void SettingText(){
		for (int i = 0; i < players.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none") {
				Score [i].GetComponent<Text> ().text = PlayerPrefs.GetInt ("RoundWinner " + (i + 1).ToString ()).ToString ();
				Score [i].GetComponent<Text> ().color = new Vector4 (255, 255, 255, 255);
			}
		}
	}
	void SettingPlayers(){
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			players[i] = GameObject.Find("Player" + (i+1).ToString() + " " + "HUD");
		}
	}
	void SettingWinner(){
		int highestScore = 0;
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			if (highestScore == 0) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].GetComponent<Image> ().sprite.name;
				winnerImage.sprite = players [i].GetComponent<Image> ().sprite;
			} else if (highestScore < int.Parse (Score [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].GetComponent<Image> ().sprite.name;
				winnerImage.sprite = players [i].GetComponent<Image> ().sprite;
			}
		}
		winnerText.text = winnerName + " is the Winner";

	}

	void Update(){
		SettingText ();
		SettingWinner ();
	}
	#endregion
}
