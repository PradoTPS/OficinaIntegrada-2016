using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetScore : MonoBehaviour {
	#region Properties
    public GameObject[] HUDScore = new GameObject[4];
	public GameObject[] players;
	public Text winnerText;

	private int numPlayers;

	private string winnerName;
	private string currentScene;
	#endregion

	#region Methods
	void Start () {
		SettingPlayers ();
	}

	void SettingText(){
		for (int i = 0; i < players.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none") {
				HUDScore [i].GetComponent<Text> ().text = PlayerPrefs.GetInt ("RoundWinner " + (i + 1).ToString ()).ToString ();
				HUDScore [i].GetComponent<Text> ().color = new Vector4 (255, 255, 255, 255);
			}
		}
	}
	void SettingPlayers(){
		currentScene = EditorApplication.currentScene.Substring (14);
		if (currentScene == "Game.unity") {
			players = GameObject.FindGameObjectsWithTag ("Player");
		}else if (currentScene == "Result.unity") {
			for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
				players[i] = GameObject.Find("Player" + (i+1).ToString() + " " + "HUD");
			}
		}
	}
	void SettingWinner(){
		int highestScore = 0;
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			if (highestScore == 0) {
				highestScore = int.Parse (HUDScore [i].GetComponent<Text> ().text);
				winnerName = players [i].GetComponent<Image> ().sprite.name;
			} else if (highestScore < int.Parse (HUDScore [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (HUDScore [i].GetComponent<Text> ().text);
				winnerName = players [i].GetComponent<Image> ().sprite.name;
			}
		}

		print (highestScore);
	}

	void Update(){
		SettingText ();
	}
	#endregion
}
