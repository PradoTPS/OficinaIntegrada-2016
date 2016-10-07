using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;

public class SetScore : MonoBehaviour {
	#region Properties
    public GameObject[] HUDScore = new GameObject[4];
	private List<Player> alive;
	GameObject Handler;
	bool end = false;

	#endregion

	#region Methods
	void Start () {
		Handler = GameObject.Find ("GameHandler ");
		alive = Handler.GetComponent<GameHandler>().alive;
		SettingText ();
	}

	void SettingText(){
		for (int i = 0; i < HUDScore.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none") {
				int playerScore = PlayerPrefs.GetInt("RoundWinner " + alive[i].gameObject.name);
				HUDScore[i].GetComponent<Text>().text = playerScore.ToString();
			}
		}
	}

	void Update(){
		if (alive.Count == 1) {
			PlayerPrefs.GetInt ("RoundWinner " + alive [0].gameObject.name, PlayerPrefs.GetInt ("RoundWinner " + alive [0].gameObject.name) + 1);
		}
		SettingText ();
	}
	#endregion
}
