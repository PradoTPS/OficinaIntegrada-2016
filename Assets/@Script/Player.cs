using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public XboxController Xcontroller;
	private static bool didQueryNumOfCtrlrs = false;

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	void Start() {
		controller = GetComponent<Controller2D> ();
	
		if(!didQueryNumOfCtrlrs)
		{
			didQueryNumOfCtrlrs = true;

			int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs();
			if(queriedNumberOfCtrlrs == 1)
			{
				Debug.Log("Only " + queriedNumberOfCtrlrs + " Xbox controller plugged in.");
			}
			else if (queriedNumberOfCtrlrs == 0)
			{
				Debug.Log("No Xbox controllers plugged in!");
			}
			else
			{
				Debug.Log(queriedNumberOfCtrlrs + " Xbox controllers plugged in.");
			}

			XCI.DEBUG_LogControllerNames();
		}


		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

	}

	void Update() {
		Vector2 input = new Vector2 (XCI.GetAxisRaw(XboxAxis.LeftStickX, Xcontroller), XCI.GetAxis(XboxAxis.LeftStickY, Xcontroller));
		int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0 && !controller.enterPlayersCollision) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

		if (XCI.GetButtonDown(XboxButton.A, Xcontroller)) {
			if (XCI.GetButtonDown(XboxButton.A, Xcontroller)) {
				if (wallSliding) {
					if (wallDirX == input.x) {
						velocity.x = -wallDirX * wallJumpClimb.x;
						velocity.y = wallJumpClimb.y;
					} else if (input.x == 0) {
						velocity.x = -wallDirX * wallJumpOff.x;
						velocity.y = wallJumpOff.y;
					} else {
						velocity.x = -wallDirX * wallLeap.x;
						velocity.y = wallLeap.y;
					}
				}
				if (controller.collisions.below) {
					velocity.y = maxJumpVelocity;
				}
			}
		}

		if (XCI.GetButtonUp(XboxButton.A, Xcontroller)) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}


		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

	}
}