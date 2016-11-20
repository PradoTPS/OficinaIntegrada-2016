using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingBehaviour : MonoBehaviour {
	#region Methods
	public Text loadText;
	#endregion

	#region Methods
	void Start(){
		StartCoroutine("LoadAnim");
		SceneManager.LoadScene (PlayerPrefs.GetString("Scene to Load"));
	}

	IEnumerator LoadAnim(){
		yield return new WaitForSeconds (0.4f);
		loadText.GetComponent<Text> ().text += ".";

		if (loadText.GetComponent<Text> ().text != "Loading....") {
			StartCoroutine("LoadAnim");
		} else {
			loadText.GetComponent<Text> ().text = "Loading";
			StartCoroutine("LoadAnim");
		}
	}
	#endregion
}
