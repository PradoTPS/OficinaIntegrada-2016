using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;



public class InstructionBehaviour : MonoBehaviour {

	private int instructionLevel;
	private string instruction;
	private XboxController Xcontroller;
	private GameObject player;
	public GameObject enemy;

	//Walk bools
	private bool left;
	private bool right;

	//Enemy bool
	private bool enemyOn;

	void Start () {
		instruction = "Hello! Welcome to Rumble Rumble! Why don't you try moving around a bit? Left and right so we can start!";
		player = GameObject.Find ("Weed");
		Xcontroller = player.GetComponent<Player> ().Xcontroller;
		instructionLevel = 0;
	}

	void verifyInstruction() {

		switch (instructionLevel) {
		case 0:
			verifyWalk ();
			break;
		case 1:
			instruction = "Great! Now how about jumping? You'll see great importance on it soon!";
			verifyJump ();
			break;
		case 2:
			instruction = "Now for some tricks: The wall jump! Get near a wall and you'll get a second jump!";
			verifyWallJump ();
			break;
		case 3:
			instruction = "Time to get brutal! The main objective in Rumble Rumble is to be last Rumble standing. How are we going to do that? By smashing others with your jump!";
			verifySmash ();
			break;
		case 4:
			instruction = "Never forget! You can punch you enemy with your action button!";
			verifyPunch ();
			break;
		case 5:
			StartCoroutine ("timeToRumble");
			break;
		case 6:
			instruction = "Great!";
			break;
		case 7:
			instruction = "Very Well!";
			break;
		case 8:
			instruction = "Not bad!";
			break;
		case 9:
			instruction = "Brutal!";
			enemyOn = false;
			break;
		case 10:
			instruction = "Hey hey! Why don't you try pushing your enemy by getting close to him and pressing you action button?";
			verifyPunch ();
			break;
		case 11:
			instruction = "Very well! You're ready for the Rumble Arena! IT'S TIME TO RUUUUUMBLE!";
			break;
		default:
			break;
		}
			
		GameObject.Find ("Instructions").GetComponent<Text> ().text = instruction;

	}

	void verifyWalk() {
		if(Input.GetKeyDown(KeyCode.A) || XCI.GetAxisRaw (XboxAxis.LeftStickX, Xcontroller) == -1f){
			left = true;
		}

		if(Input.GetKeyDown(KeyCode.D) || XCI.GetAxisRaw (XboxAxis.LeftStickX, Xcontroller) == 1f){
			right = true;
		}

		if (left && right) {
			StartCoroutine (passInstruction(1));
		}

	}
		
	void verifyJump() {
		if(Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown (XboxButton.A, Xcontroller)){
			StartCoroutine (passInstruction(2));
		}
	}

	void verifyWallJump() {
		bool wall = player.GetComponent<Player> ().onWall;
		if((Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown (XboxButton.A, Xcontroller)) && wall){
			StartCoroutine (passInstruction(3));
		}
	}

	void verifySmash() {

		if (!enemyOn) {
			setEnemy ();
			enemyOn = true;
		}

		if (transform.childCount == 1) {
			StartCoroutine (passInstruction(4));
		}
	}

	void verifyPunch() {
		bool push = player.GetComponent<Player> ().pushing;

		if (!enemyOn) {
			setEnemy ();
			enemyOn = true;
		}

		if ((Input.GetKeyDown(KeyCode.B) || XCI.GetButtonDown (XboxButton.B, Xcontroller)) && push) {
			StartCoroutine ("timeToRumble");
		} else if (!transform.FindChild("TutorialEnemy(Clone)")) {
			StartCoroutine ("correctPush");
		}
	}

	void setEnemy(){
		if (!transform.FindChild("TutorialEnemy(Clone)")) {
			player.transform.position = new Vector3 (-6, -2.597147f, -1);
			GameObject newEnemy = Instantiate (enemy);
			newEnemy.transform.parent = this.transform;
		}
	}

	IEnumerator correctPush() {
		instructionLevel = 10;
		yield return new WaitForSeconds (1.3f);
		setEnemy ();
	}

	IEnumerator timeToRumble(){
		instructionLevel = 11;
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene ("Menu");
	}

	IEnumerator passInstruction(int level){
		yield return new WaitForSeconds (1);
		instructionLevel = level + 5;
		yield return new WaitForSeconds (1.7f);
		instructionLevel = level;
	}

	void Update () {
		verifyInstruction ();
	}
}