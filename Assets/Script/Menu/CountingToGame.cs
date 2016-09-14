using UnityEngine;
using System.Collections;

public class CountingToGame : MonoBehaviour {
	#region Properties
	private GameObject[] selectables = new GameObject[4];
	public bool canPlay;
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < selectables.Length; i++) {
			selectables [i] = GameObject.Find("Selectable" + (i + 1).ToString());
		}
	}

	int Counting(){
		int count = 0;

		for (int i = 0; i < selectables.Length; i++) {
			if (selectables [i].GetComponent<PlayerSelection> ().ready) {
				count += 1;
			}
		}

		return count;
	}

	void Update(){
		if (Counting () >= 2) {
			canPlay = true;
		} else {
			canPlay = false;
		}
	}
	#endregion
}
