using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour {

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
