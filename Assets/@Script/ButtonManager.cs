using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	#region Properties
	public string nextScene;
	#endregion

	#region Methods
	public void GoTo(){
		SceneManager.LoadScene (nextScene);
	}
	#endregion
}
