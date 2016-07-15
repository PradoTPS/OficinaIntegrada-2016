using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

	List<Player> alive = new List<Player>();
	string winner;


	//Remove these later
	TextMesh txt;
	AudioSource src;
	public AudioClip A;

	void Awake () {
		alive.Add(GameObject.Find ("Player 1").GetComponent<Player>());
		alive.Add(GameObject.Find ("Player 2").GetComponent<Player>());
		alive.Add(GameObject.Find ("Player 3").GetComponent<Player>());
		alive.Add(GameObject.Find ("Player 4").GetComponent<Player>());

		//Remove these l8tr
		txt = GetComponent<TextMesh>();
		src = GetComponent<AudioSource> ();

	}
	void Update(){
		WinnerCheck ();
		if (alive.Count == 1) {

			winner = alive [0].gameObject.name;

			StartCoroutine(fku ());
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

	IEnumerator fku(){
		if(txt.text != winner + " is the winner"){
			txt.text = winner + " is the winner"; 
		}
		yield return new WaitForSeconds (1f);
		if(txt.text != winner + " is the winner"){
			txt.text +=""; 
		}
	}

}
