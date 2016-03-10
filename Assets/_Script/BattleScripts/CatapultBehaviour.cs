using UnityEngine;
using System.Collections;

public class CatapultBehaviour : MonoBehaviour {

	void Update () {
        if (isPressed () == true) {
            setCatapult(false);
		} else {
            setCatapult(true);
		}
    } 

	private void setCatapult(bool allow){
        if (allow == true) {
            gameObject.GetComponent<Player>().enabled = true;
            gameObject.GetComponentInChildren<AimBehaviour>().enabled = false;
            gameObject.transform.Find("Player_Aim").GetComponent<SpriteRenderer>().enabled = false;            
        } else {
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponentInChildren<AimBehaviour>().enabled = true;
            gameObject.transform.Find("Player_Aim").GetComponent<SpriteRenderer>().enabled = true;
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