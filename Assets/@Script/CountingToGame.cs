using UnityEngine;
using System.Collections;

public class CountingToGame : MonoBehaviour {
	#region Properties
	private GameObject[] selectables = new GameObject[4];
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < selectables.Length; i++) {
			selectables [i] = GameObject.Find("Selectable" + (i + 1).ToString());
		}
	}
	void Counting(){

	}
	#endregion
}
