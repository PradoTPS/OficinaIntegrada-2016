using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class Spawning : MonoBehaviour {
	#region Properties
	private GameObject[] spawners = new GameObject[4];
	public GameObject[] players;
	public GameObject playersLayer;
	#endregion

	#region Methods
	void Awake(){
		for (int i = 0; i < spawners.Length; i++) {
			spawners [i] = GameObject.Find ("Spawner" + (i + 1).ToString ());
		}
		Spawn ();
	}

	void Spawn(){
		for (int i = 0; i < spawners.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none" && PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "Random") {
				GameObject instance = Instantiate (players[(int.Parse(PlayerPrefs.GetString ("Player " + (i + 1).ToString ()).Substring (7)) - 1)], spawners [i].transform.position, Quaternion.identity) as GameObject;
				instance.transform.parent = playersLayer.transform;

				SetControll ("Player " + (i + 1).ToString (), instance);
				SetCollisionMask("Player " + (i + 1).ToString (), instance);

			} else if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) == "Random") {
				GameObject instance = Instantiate (players[Random.Range(1,5)], spawners [i].transform.position, Quaternion.identity) as GameObject;
				instance.transform.parent = playersLayer.transform;

				SetControll ("Player " + (i + 1).ToString (), instance);
				SetCollisionMask("Player " + (i + 1).ToString (), instance);
			}
		}
	}

	void SetControll(string key, GameObject player){
		if (PlayerPrefs.GetString(key + " isKeyboard") == "true") {
			switch (PlayerPrefs.GetInt(key + " Controller")) {
				case 1:
					player.GetComponent<Player> ().isKeyboard = true;
					player.GetComponent<Player> ().Kcontroller = KeyboardController.First;
					break;
				case 2:
					player.GetComponent<Player> ().isKeyboard = true;
					player.GetComponent<Player> ().Kcontroller = KeyboardController.Second;
					break;
			}
		} else {
			switch (PlayerPrefs.GetInt(key + " Controller")) {
				case 1:
					player.GetComponent<Player> ().isKeyboard = false;
					player.GetComponent<Player> ().Xcontroller = XboxController.First;
					break;
				case 2:
					player.GetComponent<Player> ().isKeyboard = false;
					player.GetComponent<Player> ().Xcontroller = XboxController.Second;
					break;
				case 3:
					player.GetComponent<Player> ().isKeyboard = false;
					player.GetComponent<Player> ().Xcontroller = XboxController.Third;
					break;
				case 4:
					player.GetComponent<Player> ().isKeyboard = false;
					player.GetComponent<Player> ().Xcontroller = XboxController.Fourth;
					break;
			}
		}
	}

	void SetCollisionMask(string key, GameObject player){
		player.layer = LayerMask.NameToLayer(key);

		for(int i = 0; i < players.Length; i++){
			if (key != "Player " + (i + 1).ToString ()) {
				player.GetComponent<Controller2D> ().collisionMask.value += LayerMask.GetMask("Player " + (i + 1).ToString ());
			}
		}
		player.GetComponent<Controller2D> ().collisionMask.value += LayerMask.GetMask("Ground");
	}
	#endregion
}