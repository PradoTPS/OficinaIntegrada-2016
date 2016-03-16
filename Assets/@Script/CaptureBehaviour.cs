using UnityEngine;
using System.Collections;

public class CaptureBehaviour : MonoBehaviour {
	#region Properties
	private SpriteRenderer sprite;
	#endregion

	#region Methods
	void Start() {
        sprite = GetComponent<SpriteRenderer> ();
	}

    void OnTriggerEnter2D(Collider2D other) {       
        if(other.gameObject.tag == "Player") {
            this.sprite.color = other.gameObject.GetComponent<SpriteRenderer> ().color;
        }
    }
	#endregion
}
