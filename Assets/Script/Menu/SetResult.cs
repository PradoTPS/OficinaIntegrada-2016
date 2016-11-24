using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetResult : MonoBehaviour {
	#region Properties
	public GameObject[] Score = new GameObject[4];
	public GameObject[] players;
	public GameObject container;

	public GameObject bar;
	public GameObject particle;
	public GameObject[] explosions;

	public Image winnerImage;
	public Sprite drawImage;
	public Text winnerText;

	private string winnerName;
	private bool isSet;


	#endregion

	#region Methods
	void Start () {
		container.transform.position = new Vector2 ((400 - (PlayerPrefs.GetInt ("numPlayers") * 100)) / 45, 0);	
		GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [15].Play ();
		StartCoroutine (changeBoolInSec (isSet, 5.65f));
		isSet = false;
		SettingPlayers ();
	}

	void SettingText() {
		for (int i = 0; i < players.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none") {
				Score [i].GetComponent<Text> ().text = PlayerPrefs.GetInt ("RoundWinner " + (i + 1).ToString ()).ToString ();
				Score [i].GetComponent<Text> ().color = new Vector4 (255, 255, 255, 255);
			}
		}
	}

	void SettingPlayers() {
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			players[i] = GameObject.Find("Player" + ( i + 1 ).ToString() + " " + "HUD");
			players [i].name = players [i].GetComponent<Image> ().sprite.name;
			players [i].GetComponentInChildren<SpriteRenderer> ().sprite = players [i].GetComponent<Image> ().sprite;
			players [i].GetComponent<Image> ().sprite = GetComponent<SetHud> ().HUDImages [11];
			Instantiate(particle, new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
		}
	}

	void SettingWinner() {
		int highestScore = 0;
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			if (highestScore == 0) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].name;
				winnerImage.sprite = players [i].GetComponentInChildren<SpriteRenderer> ().sprite;
				winnerText.text = winnerName + " is the Winner";
			} else if (highestScore < int.Parse (Score [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].name;
				winnerImage.sprite = players [i].GetComponentInChildren<SpriteRenderer> ().sprite;
				winnerText.text = winnerName + " is the Winner";
			} else if (highestScore == int.Parse (Score [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].name;
				winnerImage.sprite = drawImage;
				winnerText.text = "Ohhh! It's a draw!";
			}
		}
	}

	IEnumerator changeBoolInSec(bool boolean, float sec){
		yield return new WaitForSeconds (sec);
		isSet = true;
		FixPlayers ();
	}

	void randomNumbers( Text text) {
		text.text = Random.Range (0, 99).ToString();
	}

	void SetBarColor() {

		Component[] bars = bar.GetComponentsInChildren<SpriteRenderer> ();
		Color color = new Color();	

		for(int i = 0; i < 4; i++){
			//I hate myself for this.

			switch (winnerName) {
				case "Fred":
					color = new Color (254, 139, 174);
					break;
				case "Ingreed":
					color = new Color (136, 126, 168);
					break;
				case "Purpled":
					color = new Color (252, 110, 111);
					break;
				case "Red":
					color = new Color (131, 89, 152);
					break;
				case "Schnapps":
					color = new Color (216, 218, 218);
					break;
				case "Seaweed":
					color = new Color (69, 182, 187);
					break;
				case "Speed":
					color = new Color (255, 123, 72);
					break;
				case "Sr.Ed":
					color = new Color (59, 56, 56);
					break;
				case "Tedd":
					color = new Color (234f, 167, 71);
					break;
				case "Weed":
					color = new Color (152, 195, 80);
					break;
			}	

			//fix this
			bars [i].GetComponent<SpriteRenderer> ().color = color;
		}
	}

	void FixPlayers () {
		GameObject[] particles = GameObject.FindGameObjectsWithTag ("Particle");


		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			players [i].GetComponent<Image> ().sprite = players [i].GetComponentInChildren<SpriteRenderer> ().sprite;
			Destroy (particles [i]);
			switch (players[i].name) {
			case "Fred":
				Instantiate(explosions[6], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Ingreed":
				Instantiate(explosions[4], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Purpled":
				Instantiate(explosions[9], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Red":
				Instantiate(explosions[7], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Schnapps":
				Instantiate(explosions[2], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Seaweed":
				Instantiate(explosions[1], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Speed":
				Instantiate(explosions[3], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Sr.Ed":
				Instantiate(explosions[5], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Tedd":
				Instantiate(explosions[0], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			case "Weed":
				Instantiate(explosions[8], new Vector2(players [i].transform.position.x, players [i].transform.position.y), Quaternion.Euler(0, 0, 0));
				break;
			}
		}
	}

	void Update() {
		if (isSet) {
			SettingText ();
			SettingWinner ();
			SetBarColor ();
		} else {
			for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
				randomNumbers (players[i].GetComponentInChildren<Text>());
			}
		}
	}
	#endregion
}
