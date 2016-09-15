using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {
	#region Properties
	private List<Player> alive = new List<Player>();
	private string winner;

	//Remove these later
	private Text txt;
	private AudioSource src;
	public AudioClip A;
	#endregion

	#region Methods
	void Start () {
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			alive.Add (GameObject.FindGameObjectsWithTag ("Player")[i].GetComponent<Player>());
		}

		//Remove these l8tr
		txt = Canvas.FindObjectOfType<Text>();
		src = GetComponent<AudioSource> ();
	}

	void Update(){
		WinnerCheck ();
		if (alive.Count == 1) {
			winner = alive [0].gameObject.name.Remove(8).ToString();
			StartCoroutine(callWinner ());

			if (!src.isPlaying) {
				src.Play ();
			}
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

		if(txt.text != winner + " is the winner"){
			txt.text = winner + " is the winner"; 
		}

		yield return new WaitForSeconds (1f);
	}
	#endregion
}
