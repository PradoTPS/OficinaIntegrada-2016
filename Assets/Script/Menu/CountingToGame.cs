using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CountingToGame : MonoBehaviour {
	#region Properties
	private GameObject[] selectables = new GameObject[4];

	private GameObject animate;
	private GameObject Handler;

	private int count = 0;

	private bool canPlay;
	private bool wasCalled = false;
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < selectables.Length; i++) {
			selectables [i] = GameObject.Find("Selectable" + (i + 1).ToString());
			PlayerPrefs.DeleteKey ("RoundWinner " + (i + 1).ToString ());
		}

		animate = GameObject.Find ("Countdown");
	}

	void Start(){
		Handler = GameObject.Find ("SelectionHandler");
	}

	int CountingReady(){
		int count = 0;

		for (int i = 0; i < selectables.Length; i++) {
			if (selectables [i].GetComponent<PlayerSelection> ().ready) {
				count += 1;
			}
		}

		return count;
	}

	int CountingSet(){
		int count = 0;

		for (int i = 0; i < selectables.Length; i++) {
			if (selectables [i].GetComponent<PlayerSelection> ().isSet) {
				count += 1;
			}
		}

		return count;
	}

	void CanPlay(){
		if (CountingReady () >= 2 && CountingSet() == CountingReady()) {
			canPlay = true;
		} else {
			canPlay = false;
		}

		if (canPlay && !wasCalled) {
			wasCalled = true;
			StartCoroutine("CountDown");
		}
	}

	IEnumerator CountDown(){
		if (canPlay) {
			animate.GetComponent<Animator> ().enabled = true;

			if (count == 3) {
				Handler.GetComponent<SelectionController> ().SettingRandom ();
				PlayerPrefs.SetInt ("Round", 1);
				PlayerPrefs.SetFloat ("Countdown", CountingReady ());
				PlayerPrefs.SetString("Scene to Load", "Game");
				SceneManager.LoadScene ("Loading");
				count = 0;
			} else if (count == 2) {
				GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [2].Play ();
			} else {
				GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [3].Play ();
			}

			animate.GetComponent<Text> ().text = (int.Parse (animate.GetComponent<Text> ().text) - 1).ToString();
			animate.GetComponent<Animation> ().Play ();

			count++;

			yield return new WaitForSeconds (2f);
			StartCoroutine ("CountDown");
		} else {
			count = 0;
			wasCalled = false;
			animate.GetComponent<Animator> ().enabled = false;
			animate.transform.position = new Vector2 (-430, 70);
			animate.GetComponent<Text> ().text = "4";
		}
	}

	void Update(){
		CanPlay ();
	}
	#endregion
}
