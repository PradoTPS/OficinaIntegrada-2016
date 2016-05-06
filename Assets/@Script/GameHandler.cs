using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

	IList<Player> players = new List<Player> ();

	public int alive;

	void Start () {
		players = GetComponentsInChildren<Player> ();
	}

	void Update () {
		for (int i = 0; i == players.Count; i++) {
			Debug.Log ("Fuck this");
		}
	}
}
