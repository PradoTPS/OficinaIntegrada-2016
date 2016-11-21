using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingBehaviour : MonoBehaviour {
	#region Properties
	public Text loadText;
	#endregion

	#region Methods
	void Start(){
		StartCoroutine("LoadAnim");
		SceneManager.LoadSceneAsync (PlayerPrefs.GetString("Scene to Load"));
	}

	IEnumerator LoadAnim(){
		yield return new WaitForSeconds (0.2f);
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
