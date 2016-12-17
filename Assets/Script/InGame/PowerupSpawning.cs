using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class PowerupSpawning : MonoBehaviour {

	public float firstSpawnMinTime;
	public float firstSpawnMaxTime;
	public float spawnMinTime;
	public float spawnMaxTime;
	public float timeToDestroy;

	private float timer = 0f;

	private bool isSpawned = false;

	public GameObject[] spawners;
	private GameObject spawnedPowerUp;

	public GameObject powerUp;


	void Awake () {
		TimerRandomizer (firstSpawnMinTime, firstSpawnMaxTime);
		for (int i = 0; i < spawners.Length; i++) {
			spawners [i] = GameObject.Find ("Spawn" + (i + 1).ToString ());
		}
	}

	void Spawn (){
		int choosenSpawn = Random.Range (1, 4);
		Debug.Log (choosenSpawn);
		Instantiate (powerUp, new Vector3 (spawners [choosenSpawn - 1].transform.position.x, spawners [choosenSpawn - 1].transform.position.y, 0), Quaternion.identity);
		spawnedPowerUp = GameObject.Find ("PowerUp(Clone)");
	}

	void TimerRandomizer(float a, float b){
		timer = Random.Range (a, b);
	}

	void PowerUpSpawnBehavior(){
		timer -= Time.deltaTime;
		//Debug.Log (timer);
		if (timer <= 0 && !isSpawned) {
			Spawn ();
			isSpawned = true;
			timer = timeToDestroy;
		}
		if (timer <= 0 && isSpawned) {
			Object.Destroy (spawnedPowerUp);
			isSpawned = false;
			TimerRandomizer (spawnMinTime, spawnMaxTime);
		}
	}

	void Update (){
		PowerUpSpawnBehavior ();
	}
}
