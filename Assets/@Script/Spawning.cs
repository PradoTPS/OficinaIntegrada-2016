using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
	#region Properties
	private GameObject[] spawners;
	private GameObject p1, p2, p3, p4;
	public GameObject player1, player2, player3, player4;
	public GameObject playersLayer;
	#endregion

	#region Methods
	void Awake(){
		spawners = GameObject.FindGameObjectsWithTag ("Spawn");
		Spawn ();
	}

	void Spawn(){
		p1 = Instantiate (player1, spawners [0].transform.position, Quaternion.identity) as GameObject;
		p2 = Instantiate (player2, spawners [1].transform.position, Quaternion.identity) as GameObject;
		p3 = Instantiate (player3, spawners [2].transform.position, Quaternion.identity) as GameObject;
		p4 = Instantiate (player4, spawners [3].transform.position, Quaternion.identity) as GameObject;

		p1.transform.parent = playersLayer.transform;
		p2.transform.parent = playersLayer.transform;
		p3.transform.parent = playersLayer.transform;
		p4.transform.parent = playersLayer.transform;
	}
	#endregion
}
