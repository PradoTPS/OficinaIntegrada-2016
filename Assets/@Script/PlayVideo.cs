using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {
	#region Properties
	public MovieTexture video;
	#endregion

	#region Methods
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
	#endregion
}