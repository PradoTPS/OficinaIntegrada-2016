using UnityEngine;
using System.Collections;

public class AimBehaviour : MonoBehaviour {

    private float position_x;
    private float position_y;

	void Update () {
        
        transform.Rotate(Vector3.forward * Time.deltaTime * 30, Space.World);

        position_x = Input.GetAxisRaw ("Horizontal");
        position_y = Input.GetAxisRaw ("Vertical");

        if (position_x != 0 || position_y != 0) {
            movePosition (position_x, position_y);
        } else {
            adjustToCenter();
        }

        //print ("Axis_x = " + (position_x) + " Axis_y = " + (position_y));
    }

    void movePosition (float speed_x, float speed_y) {

        if (GetComponent<SpriteRenderer> ().enabled == true) {

            transform.position = new Vector3 (transform.position.x + (speed_x / 7), transform.position.y + (speed_y / 7), 0);

            string pos = insideLimits();

            if (pos == "outside") {
                transform.position = new Vector3 (transform.position.x - (speed_x / 7), transform.position.y - (speed_y / 7), 0);
            } else if (pos == "limit") {
                //transform.position = new Vector3 (transform.position.x - (speed_x / 7), transform.position.y - (speed_y / 7), 0);
            }
        }
    }


    void adjustToCenter() {
        Vector3 parentPosition = transform.parent.position;
        transform.position = Vector3.Lerp(transform.position, parentPosition, .2f);
    }

    string insideLimits() {
        
        Vector3 parentPosition = transform.parent.position;
        int r = 1;

        if (Mathf.Pow((transform.position.x - parentPosition.x), 2) + Mathf.Pow((transform.position.y - parentPosition.y), 2) < Mathf.Pow(r, 2)) {
            return "inside";
        } else if (Mathf.Pow ((transform.position.x - parentPosition.x), 2) + Mathf.Pow ((transform.position.y - parentPosition.y), 2) == Mathf.Pow (r, 2)) {
            return "limit";
        } else {
            return "outside";
        }
        
    }

}
