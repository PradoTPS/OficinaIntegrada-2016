using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountingToGame : MonoBehaviour {
	#region Properties
	private GameObject[] selectables = new GameObject[4];
	private bool canPlay;
	private bool wasCalled = false;

	private int count = 0;
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < selectables.Length; i++) {
			selectables [i] = GameObject.Find("Selectable" + (i + 1).ToString());
		}
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
			StartCoroutine("CountDowm");
		}
	}

	IEnumerator CountDowm(){
		if (canPlay) {
			if (count == 3) {
				SceneManager.LoadScene ("Game");
				count = 0;
			}
			count++;

			Debug.Log (count);

			yield return new WaitForSeconds (2f);
			StartCoroutine ("CountDowm");
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
