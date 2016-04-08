using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

public class Launching: MonoBehaviour {

	public XboxController Xcontroller;
	public KeyboardController Kcontroller;

	float horizontal;
	float vertical;
	[HideInInspector]
	public float direction = 0;
	float angle;

	public float pushForce;

	Player player ;
	float LaunchForce ;

	void Start(){
		player = GetComponentInParent<Player> ();
		LaunchForce = player.gravity + pushForce;
	}

	void setAngle () {

			if (player.isKeyboard) {
				vertical = (KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller) == 1) ? vertical = 90 : (KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller) == -1) ? vertical = 270 : vertical = horizontal;
				horizontal = (KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller) == 0) ? horizontal = 0 : horizontal = vertical;
			} else {
				vertical = (XCI.GetAxisRaw (XboxAxis.LeftStickY, Xcontroller) == 1) ? vertical = 90 : (XCI.GetAxisRaw (XboxAxis.LeftStickY, Xcontroller) == -1) ? vertical = 270 : vertical = horizontal;
				horizontal = (XCI.GetAxisRaw (XboxAxis.LeftStickY, Xcontroller) == 0) ? horizontal = 0 : horizontal = vertical;
				}
			
		angle = (horizontal + vertical) / 2;
	}
		
	void Update () {
		setAngle ();
		transform.rotation = Quaternion.Euler(0, 0, angle*-1);
	}
	void Launch(){
		if (player.curState == "Preparation") setAngle ();
		if ((vertical != 0 || horizontal != 0)  && player.curState == "Preparation") {
			player.curState = "Launching";
		}
	}
}