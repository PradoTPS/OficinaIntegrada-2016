using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class KeepAlive : MonoBehaviour {
	#region Properties
	public string tagName;
	#endregion

	#region Methods
	void Start(){
		KeepOne ();
	}

	void Update(){
		KeepIt ();
	}

	void KeepIt(){
		if (SceneManager.GetActiveScene ().name == "Game") {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
		}
	}

	void KeepOne(){
		if (GameObject.FindGameObjectsWithTag (tagName).Length > 1) {
			Destroy (gameObject);
		}
	}
	#endregion
}