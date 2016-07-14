using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public string nextScene;

	public void GoTo(){
		SceneManager.LoadScene (nextScene);
	}
}
