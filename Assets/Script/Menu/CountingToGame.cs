using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CountingToGame : MonoBehaviour {
	#region Properties
	private GameObject[] selectables = new GameObject[4];
	private bool canPlay;
	private bool wasCalled = false;
	private GameObject animate;

	private int count = 0;
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < selectables.Length; i++) {
			selectables [i] = GameObject.Find("Selectable" + (i + 1).ToString());
		}

		animate = GameObject.Find ("Countdown");
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
			animate.GetComponent<Animator> ().enabled = true;
		} else {
			canPlay = false;
			animate.GetComponent<Animator> ().enabled = false;
		}

		if (canPlay && !wasCalled) {
			wasCalled = true;
			StartCoroutine("CountDown");
		}
	}

	IEnumerator CountDown(){
		if (canPlay) {
			
			if (count == 3) {
				PlayerPrefs.SetInt ("Round", 1);
				PlayerPrefs.SetFloat ("Countdown", CountingReady());
				SceneManager.LoadScene ("Game");
				count = 0;
			}

			animate.GetComponent<Text> ().text = (int.Parse (animate.GetComponent<Text> ().text) - 1).ToString();
			animate.GetComponent<Animation> ().Play ();

			count++;

			Debug.Log (count);

			yield return new WaitForSeconds (2f);
			StartCoroutine ("CountDown");

		} else {
			count = 0;
			wasCalled = false;
		}
	}

	void Update(){
		CanPlay ();
	}
	#endregion
}
