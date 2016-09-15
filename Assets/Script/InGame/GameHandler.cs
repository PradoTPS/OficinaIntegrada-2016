using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {
	#region Properties
	private List<Player> alive = new List<Player>();
	private string winner;

	public Text winnerTxt;
	public Text roundTxt;
	#endregion

	#region Methods
	void Start () {
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			alive.Add (GameObject.FindGameObjectsWithTag ("Player")[i].GetComponent<Player>());
		}

		roundTxt.text = "Round " + PlayerPrefs.GetInt ("Round").ToString ();
	}

	void Update(){
		WinnerCheck ();
		if (alive.Count == 1) {
			winner = alive [0].gameObject.name.Remove(8).ToString();
			StartCoroutine(callWinner ());
		}
	}

	void WinnerCheck (){
		for (int i = 0; i <= alive.Count - 1 ; i++) {
			if (alive [i].curState == "Dead") {
				alive.Remove (alive[i]);
			}
		}
	}

	void discoverName () {
		winner = (alive [0].gameObject.GetComponent<Animator> ().runtimeAnimatorController).ToString ().Remove(4);

		switch (winner) {
		case "Azul":
			winner = "Seaweed";
			break;

		case "Verd":
			winner = "Weed";
			break;

		case "Lara":
			winner = "Tedd";
			break;

		case "Roxo":
			winner = "Ingreed";
			break;
		}
	}

	IEnumerator callWinner(){
		discoverName ();

		if(winnerTxt.text != winner + " is the winner"){
			winnerTxt.text = winner + " is the winner";

			PlayerPrefs.SetInt ("Round", PlayerPrefs.GetInt ("Round") + 1);
		}
			
		yield return new WaitForSeconds (10f);

		if (PlayerPrefs.GetInt ("Round") <= 3) {
			SceneManager.LoadScene ("Game");
		} else {
			SceneManager.LoadScene ("SplashScreen");
			PlayerPrefs.SetInt ("Round", 0);
		}
	}
	#endregion
}
