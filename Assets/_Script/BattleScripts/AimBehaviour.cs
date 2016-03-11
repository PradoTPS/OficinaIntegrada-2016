using UnityEngine;
using System.Collections;

public class AimBehaviour : MonoBehaviour {

    private float position_x;
    private float position_y;

	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * 30, Space.World);
        
        position_x = Input.GetAxis("Horizontal");
        position_y = Input.GetAxis("Vertical");

        if (position_x != 0 || position_y != 0) {
            transform.position = new Vector3(transform.position.x + position_x, transform.position.y + position_y, 0);
            adjustLimits();
        } else {
            adjustToCenter();
        }
    }

    void adjustToCenter() {
        print("Hey, need to adjust to center!");
        Vector3 parentPosition = transform.parent.position;
        transform.position = new Vector3(transform.position.x + position_x, transform.position.y + position_y, 0);
    }

    void adjustLimits() {
        print("Hey, need to adjust the limits!");
        Vector3 parentPosition = transform.parent.position;
    }
}
