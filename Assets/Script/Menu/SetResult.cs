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

	public Text winnerText;
	private List<Vector2> order = new List<Vector2>();

	private string winnerName;
	private bool isSet;


	#endregion

	#region Methods
	void Start () {
		//I mean, if you don't divide this by 45, makes perfect, PERFECT sense, but it returns 6524... Welp.
		float number = (280 - (70 * PlayerPrefs.GetInt ("numPlayers"))) / 45;
		container.transform.position = new Vector2 (number, container.transform.position.y);	
		GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [15].Play ();
		StartCoroutine (changeBoolInSec (isSet, 5.65f));
		isSet = false;
		SettingPositions ();
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

	void SettingPositions(){
		for (int i = 0; i < 4; i++) {
			Vector2 temp = new Vector2 (PlayerPrefs.GetInt ("RoundWinner " + (i + 1)), 1);
			order.Add (temp);
		}

		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			for (int j = 0; j < PlayerPrefs.GetInt ("numPlayers"); j++) {
				if (order [i].x < order [j].x) {
					Vector2 temp = new Vector2 (order [i].x, order [i].y + 1);
					order [i] = temp;
				}
			}
			print (order [i]);
		}
	}

	void SettingPlayers() {
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {

			players[i] = GameObject.Find("Player" + (order[i].y).ToString() + " " + "HUD");

			players [i].name = players [i].GetComponent<Image> ().sprite.name;
			players [i].GetComponentInChildren<SpriteRenderer> ().sprite = players [i].GetComponent<Image> ().sprite;
			players [i].GetComponent<Image> ().sprite = GetComponent<SetHud> ().HUDImages [11];

			//The particle on them
			InstantiateParticle(players [i].transform.position.x, players [i].transform.position.y, players [i].name);
		}
	}

	void InstantiateParticle(float x, float y, string name){

		GameObject temp;
		Color color;

		switch (name) {
		case "Fred":
			color = new Color (254, 139, 174);
			temp = Instantiate (particle, new Vector2 (x, y), Quaternion.Euler (0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Ingreed":
			color = new Color (136, 126, 168);
			temp = Instantiate (particle, new Vector2 (x, y), Quaternion.Euler (0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Purpled":
			color = new Color (252, 110, 111);
			temp = Instantiate(particle, new Vector2(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Red":
			color = new Color (131, 89, 152);
			temp = Instantiate(particle, new Vector2(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Schnapps":
			color = new Color (216, 218, 218);
			temp = Instantiate(particle, new Vector2(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Seaweed":
			color = new Color (69, 182, 187);
			temp = Instantiate(particle, new Vector2(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Speed":
			color = new Color (255, 123, 72);
			temp = Instantiate(particle, new Vector2(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Sr.Ed":
			color = new Color (59, 56, 56);
			temp = Instantiate(particle, new Vector2(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Tedd":
			color = new Color (234f, 167, 71);
			temp = Instantiate(particle, new Vector2(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = color;
			break;
		case "Weed":
			color = new Color (152, 195, 80);
			temp = Instantiate (particle, new Vector2 (x, y), Quaternion.Euler (0, 0, 0)) as GameObject;
			temp.GetComponent<ParticleSystem> ().startColor = new Color (152, 195, 80, 1f);
			break;
		}	
	}

	void SettingWinner() {
		int highestScore = 0;

		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			if (highestScore == 0) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].name;
				winnerText.text = winnerName + " Won!";
			} else if (highestScore < int.Parse (Score [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].name;
				winnerText.text = winnerName + " Won!";
			} else if (highestScore == int.Parse (Score [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].name;
				winnerText.text = "It's a draw!";
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
