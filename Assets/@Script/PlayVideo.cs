using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {
	
	public MovieTexture video;

	void Awake() {
		GetComponent<Renderer>().material.mainTexture = video;
		video.loop = true;
	}

	void Update(){
		Play ();
	}

	void Play(){
		if (!video.isPlaying) {
			video.Play ();
		}
	}
}