using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
	
	private GameObject[] spawners;
	private GameObject p1, p2, p3, p4;
	public GameObject player1, player2, player3, player4;

	void Awake(){
		spawners = GameObject.FindGameObjectsWithTag ("Spawn");
		Spawn ();
	}

	void Spawn(){
		Instantiate (player1, spawners [0].transform.position, Quaternion.identity);
		Instantiate (player2, spawners [1].transform.position, Quaternion.identity);
		Instantiate (player3, spawners [2].transform.position, Quaternion.identity);
		Instantiate (player4, spawners [3].transform.position, Quaternion.identity);
	}
}
