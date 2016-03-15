using UnityEngine;
using System.Collections;

public class CatapultBehaviour : MonoBehaviour {

	void Update () {
        if (isPressed () == true) {
            setCatapult(true);
            getPower ();
		} else {
            setCatapult(false);
		}
    } 

	private void setCatapult(bool allow){
        if (allow) {
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.transform.Find("Player_Aim").GetComponent<SpriteRenderer>().enabled = true;            
        } else {
            gameObject.GetComponent<Player>().enabled = true;
            gameObject.transform.Find("Player_Aim").GetComponent<SpriteRenderer>().enabled = false;
        }
	}

    private void getPower () {
        float bar = Input.GetAxis ("CatapultPower");
        print (bar);
    }

	private bool isPressed(){
		if (Input.GetAxisRaw ("CatapultTest") == -1) {
			return true;
		} else {
			return false;
		}
	}
}