using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {
	#region Properties
	private List<Player> alive = new List<Player>();
	private string winner;

	//Remove these later
	private TextMesh txt;
	private AudioSource src;
	public AudioClip A;
	#endregion

	#region Methods
	void Start () {
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			alive.Add (GameObject.FindGameObjectsWithTag ("Player")[i].GetComponent<Player>());
		}

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
	#endregion
}
