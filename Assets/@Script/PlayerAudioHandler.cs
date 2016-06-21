using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerAudioHandler : MonoBehaviour {

	AudioSource audio;
	Player player;

	void Start () {
		audio = GetComponent<AudioSource> ();
		player = GetComponent<Player> ();
	}
	

	void Update () {
		if (player.curState == "Kill") {
			//audio.
		}

	}
}
