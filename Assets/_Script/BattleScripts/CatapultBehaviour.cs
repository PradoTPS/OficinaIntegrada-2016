using UnityEngine;
using System.Collections;

public class CatapultBehaviour : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isPressed () == true) {
			changeMov (false);
		} else {
			changeMov (true);
		}
    } 

	private void changeMov(bool allow){
        if (allow == true) {
            gameObject.GetComponent<Player>().enabled = true;
        } else {
            gameObject.GetComponent<Player>().enabled = false;
        }
	}

	private bool isPressed(){
		if (Input.GetAxis ("CatapultTest") < 0 ) {
			return true;
		} else {
			return false;
		}
	}
}