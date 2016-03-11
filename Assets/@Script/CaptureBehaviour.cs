using UnityEngine;
using System.Collections;

public class CaptureBehaviour : MonoBehaviour {

	// Use this for initialization

    public SpriteRenderer sprite;
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}

    void OnTriggerEnter2D(Collider2D other) { 
                
            if(other.gameObject.tag == "Player"){
                this.sprite.color = other.gameObject.GetComponent<SpriteRenderer>().color;
            }
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
