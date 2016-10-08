using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetScore : MonoBehaviour {
	#region Properties
    public GameObject[] HUDScore = new GameObject[4];
	public GameObject[] players;
	#endregion

	#region Methods
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
	}

	void SettingText(){
		for (int i = 0; i < players.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none") {
				HUDScore[i].GetComponent<Text>().text = PlayerPrefs.GetInt("RoundWinner " + (i + 1).ToString()).ToString();
				HUDScore[i].GetComponent<Text> ().color = new Vector4(255,255,255,255);
			}
		}
	}

	void Update(){
		SettingText ();
	}
	#endregion
}
