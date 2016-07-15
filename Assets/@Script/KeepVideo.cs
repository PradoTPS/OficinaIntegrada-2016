using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public class KeepVideo : MonoBehaviour {

	void Start(){
		KeepOne ();
	}

	void Update(){
		KeepIt ();
	}

	void KeepIt(){
		if (EditorSceneManager.GetActiveScene ().name == "Game") {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
		}
	}

	void KeepOne(){
		if (GameObject.FindGameObjectsWithTag ("Video").Length > 1) {
			Destroy (gameObject);
		}
	}
}