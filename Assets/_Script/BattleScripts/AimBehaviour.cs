using UnityEngine;
using System.Collections;

public class AimBehaviour : MonoBehaviour {

    private float position_x;
    private float position_y;

	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * 30, Space.World);
        
        position_x = Input.GetAxis("Horizontal");
        position_y = Input.GetAxis("Vertical");

        transform.position = new Vector3(transform.position.x + position_x, transform.position.y + position_y, 0);
    }
}
