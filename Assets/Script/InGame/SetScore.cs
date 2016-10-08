using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;

public class SetScore : MonoBehaviour {
	#region Properties
    public GameObject[] HUDScore = new GameObject[4];
	public List<Player> playerNum = new List<Player>();
	private List<Player> alive;
	GameObject Handler;
	bool end = false;

	#endregion

	#region Methods
	void Start () {
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			playerNum.Add (GameObject.FindGameObjectsWithTag ("Player") [i].GetComponent<Player> ());
		}
		Handler = GameObject.Find ("GameHandler ");
		alive = Handler.GetComponent<GameHandler>().alive;
		SettingText ();
	}

	void SettingText(){
		for (int i = 0; i < alive.Count; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none") {
				int playerScore = PlayerPrefs.GetInt("RoundWinner " + playerNum[i].gameObject.name);
				HUDScore[i].GetComponent<Text>().text = playerScore.ToString();
				HUDScore [i].GetComponent<Text> ().fontSize = 70;
				HUDScore[i].GetComponent<Text>().name = "Text" + playerNum[i].gameObject.name;
				
			}
		}
	}

	void Update(){
		if (alive.Count == 1 && !end) {
			//PlayerPrefs.SetInt ("RoundWinner " + alive [0].gameObject.name, PlayerPrefs.GetInt ("RoundWinner " + alive [0].gameObject.name) + 1);
			switch (alive [0].gameObject.name) {
			case "Seaweed(Clone)":
				PlayerPrefs.SetInt ("RoundWinner Seaweed(Clone)", PlayerPrefs.GetInt ("RoundWinner Seaweed(Clone)") + 1);
				Text SeaweedScore = GameObject.Find ("TextSeaweed(Clone)").GetComponent<Text>();
				SeaweedScore.text = PlayerPrefs.GetInt ("RoundWinner Seaweed(Clone)").ToString();
				break;

			case "Weed(Clone)":
				PlayerPrefs.SetInt ("RoundWinner Weed(Clone)", PlayerPrefs.GetInt ("RoundWinner Weed(Clone)") + 1);
				Text WeedScore = GameObject.Find ("TextWeed(Clone)").GetComponent<Text>();
				WeedScore.text = PlayerPrefs.GetInt ("RoundWinner Weed(Clone)").ToString();
				break;

			case "Tedd(Clone)":
				PlayerPrefs.SetInt ("RoundWinner Tedd(Clone)", PlayerPrefs.GetInt ("RoundWinner Tedd(Clone)") + 1);
				Text TeddScore = GameObject.Find ("TextTedd(Clone)").GetComponent<Text>();
				TeddScore.text = PlayerPrefs.GetInt ("RoundWinner Tedd(Clone)").ToString();
				break;

			case "Ingreed(Clone)":
				PlayerPrefs.SetInt ("RoundWinner Ingreed(Clone)", PlayerPrefs.GetInt ("RoundWinner Ingreed(Clone)") + 1);
				Text IngreedScore = GameObject.Find ("TextIngreed(Clone)").GetComponent<Text>();
				IngreedScore.text = PlayerPrefs.GetInt ("RoundWinner Ingreed(Clone)").ToString();
				break;
			}
			end = true;
		}
		SettingText ();
		print (alive [0].gameObject.name);
	}
	#endregion
}
