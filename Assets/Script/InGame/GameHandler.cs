using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {
	#region Properties
	private string winnerName;

	public List<Player> alive = new List<Player>();

	public Text winnerText;
	public Text roundText;
	#endregion

	#region Methods
	void Start () {
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			alive.Add (GameObject.FindGameObjectsWithTag ("Player")[i].GetComponent<Player>());
		}

		roundText.text = "Round " + PlayerPrefs.GetInt ("Round").ToString ();
	}

	void CheckingAlive (){
		for (int i = 0; i <= alive.Count - 1 ; i++) {
			if (alive [i].curState == "Dead") {
				alive.Remove (alive[i]);
			}
		}

		if (alive.Count == 1) {
			StartCoroutine(CallWinner ());
		}
	}

	IEnumerator CallWinner(){
		DiscoverName ();

		if(winnerText.text != winnerName + " is the winner"){
			winnerText.text = winnerName + " is the winner";
			PlayerPrefs.SetInt ("Round", PlayerPrefs.GetInt ("Round") + 1);
			PlayerPrefs.SetInt("RoundWinner " + alive[0].playerNumber.ToString(), PlayerPrefs.GetInt("RoundWinner " + alive[0].playerNumber) + 1);
			GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [0].Play ();				
		}
			
		yield return new WaitForSeconds (5f);

		if (PlayerPrefs.GetInt ("Round") <= PlayerPrefs.GetInt("NumberOfRounds")) {
			SceneManager.LoadScene ("Game");
		} else {
			SceneManager.LoadScene ("Result");
			PlayerPrefs.SetInt ("Round", 0);
			PlayerPrefs.SetInt ("NumberOfRounds", 3);

		}
	}

	void DiscoverName () {
		for (int i = 0; i < 7; i++) {
			winnerName = alive [0].gameObject.name.Remove (alive [0].gameObject.name.Length - (i + 1)).ToString ();
		}

		switch (winnerName) {
			case "Azul":
				winnerName = "Seaweed";
				break;

			case "Verde":
				winnerName = "Weed";
				break;

			case "Laranja":
				winnerName = "Tedd";
				break;

			case "Roxo":
				winnerName = "Ingreed";
				break;
		}
	}

	void Update(){
		CheckingAlive ();
	}
	#endregion
}
