using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InstructionBehaviour : MonoBehaviour {

	private int instructionLevel;
	private string instruction;
	GameObject player;

	void Start () {
		player = GameObject.Find ("Weed");

		GameObject.Find ("Instructions").GetComponent<Text> ().text = "Hello! Welcome to Rumble Rumble! Why don't you try moving around a bit? Left and right so we can start!";
		instructionLevel = 0;
	}

	void verifyWalk() {
		if (instructionLevel == 0) {

		}
	}

	void verifyJump() {
		if (instructionLevel == 1) {
			
		}
	}

	void Update () {
		verifyWalk ();
		verifyJump ();
	}
}

/*
	Hello! Welcome to Rumble Rumble! Why don't you try moving around a bit? Left and right so we can start!

	Great! Now how about jumping? You'll see great importance on it soon!

	Now for some tricks: The wall jump! Get near a wall and you'll get a second jump!

	Time to get brutal! The main objective in Rumble Rumble is to be last Rumble standing. How are we going to do that? By smashing others with your jump!

	Never forget! You can punch you enemy with your action button! 

	Very well! You're ready for the Rumble Arena! IT'S TIME TO RUUUUUMBLE!
 */