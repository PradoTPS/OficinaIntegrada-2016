using UnityEngine;
using System.Collections;

public class CollisionCorrector : MonoBehaviour {

	int XHittingDirection(Transform me, Transform other){

		Vector3 dif = other.position - me.position;
		int dir = 0; 
		dir = (dif.x > 0) ? 1 : -1;
		return dir;
	}

	void MoveBlock(Vector2 myInput, int collisionDir , Vector2 modifiedInput){

		float DirX = myInput.x;
		float DirY = myInput.y;
		float ModX = 0;

		if (DirX == collisionDir){ 
			Debug.Log ("Helow");
		}

	}
}
