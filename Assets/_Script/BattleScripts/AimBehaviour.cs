using UnityEngine;
using System.Collections;

public class AimBehaviour : MonoBehaviour {

    private float position_x;
    private float position_y;
    private bool adjustOk;

	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * 30, Space.World);
        
        position_x = Input.GetAxis("Horizontal") / 7;
        position_y = Input.GetAxis("Vertical") / 7;

        if ((position_x != 0 || position_y != 0) && insideLimits()) {
            transform.position = new Vector3(transform.position.x + position_x, transform.position.y + position_y, 0);
        } else {
            adjustToCenter();
        }
    }

    void adjustToCenter() {
        Vector3 parentPosition = transform.parent.position;
        transform.position = Vector3.Lerp(transform.position, parentPosition, .2f);
    }

    bool insideLimits() {
        Vector3 parentPosition = transform.parent.position;
        int r = 1;
        if (Mathf.Pow((transform.position.x - parentPosition.x), 2) + Mathf.Pow((transform.position.y - parentPosition.y), 2) <= Mathf.Pow(r, 2)) {
            return true;
        } else {
            return false;
        }
        
    }
}
