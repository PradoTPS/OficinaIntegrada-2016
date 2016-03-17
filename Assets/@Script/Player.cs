using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using KeyboardInput;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {
	#region Properties
	public bool isKeyboard = false;
	private static bool didQueryNumOfCtrlrs = false;

	public XboxController Xcontroller;
	public KeyboardController Kcontroller;

	Controller2D controller;

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	float velocityXSmoothing;
	Vector3 velocity;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float knockbackForce;
	public bool knocked;
	#endregion

	#region Methods
	void Start() {
		controller = GetComponent<Controller2D> ();
	
		if(!didQueryNumOfCtrlrs) {
			didQueryNumOfCtrlrs = true;
			int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs ();

			if(queriedNumberOfCtrlrs == 1) {
				Debug.Log("Only " + queriedNumberOfCtrlrs + " Xbox controller plugged in.");
			} else if (queriedNumberOfCtrlrs == 0) {
				Debug.Log("No Xbox controllers plugged in!");
			} else {
				Debug.Log(queriedNumberOfCtrlrs + " Xbox controllers plugged in.");
			}

			XCI.DEBUG_LogControllerNames();
		}
			
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	int XHittingDirection (Transform From , Transform To){

		Vector3 fk = To.position - From.position;
		int dir = 0;
		dir = (fk.x > 0) ? 1 : -1;
		return dir;
	}


	void Update() {
		Vector2 input;
		if (isKeyboard) {
			input = new Vector2 (KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller), KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller));
		} else {
			input = new Vector2 (XCI.GetAxisRaw (XboxAxis.LeftStickX, Xcontroller), XCI.GetAxis (XboxAxis.LeftStickY, Xcontroller));
		}

		int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0 && controller.lastHit.collider.tag != "Player") {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				} else {
					timeToWallUnstick = wallStickTime;
				}
			} else {
				timeToWallUnstick = wallStickTime;
			}

		}

		if ((XCI.GetButtonDown (XboxButton.A, Xcontroller) && !isKeyboard) || (KCI.GetButtonDown(KeyboardButton.Jump, Kcontroller) && isKeyboard)) {
			if ((XCI.GetButtonDown (XboxButton.A, Xcontroller) && !isKeyboard) || (KCI.GetButtonDown(KeyboardButton.Jump, Kcontroller) && isKeyboard)) {
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

		if ((XCI.GetButtonUp (XboxButton.A, Xcontroller) && !isKeyboard) || (KCI.GetButtonUp(KeyboardButton.Jump, Kcontroller) && isKeyboard)){
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}

		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}



		if (((XCI.GetButtonDown (XboxButton.X, Xcontroller) && !isKeyboard) || (KCI.GetButtonDown(KeyboardButton.Action, Kcontroller) && isKeyboard)) && controller.interPlayersCollision) {
			controller.lastHit.collider.gameObject.GetComponent<Player> ().knocked = true;
			Debug.Log ("1");
			Knocking ();

		}

	}

	IEnumerator Waiter ()
	{
		Debug.Log ("4");
		yield return new WaitForSeconds(.5f);
		controller.lastHit.collider.gameObject.GetComponent<Player> ().knocked = false;
		Debug.Log ("Other: " + controller.lastHit.collider.gameObject.GetComponent<Player> ().knocked);

	}


	void Knocking (){
		Debug.Log ("2");
		if (controller.lastHit.transform.gameObject.GetComponent<Player> ().knocked ) {
			Debug.Log ("3");
			float i = 1f;
			Vector3 other = controller.lastHit.transform.position;
			int direction = XHittingDirection (this.transform, controller.lastHit.transform);

			other.x = Mathf.Lerp (other.x, other.x + knockbackForce * direction, i * Time.deltaTime);
			controller.lastHit.transform.position = other;
			Debug.Log("Other: " + controller.lastHit.collider.gameObject.GetComponent<Player>().knocked + " // " + "lasthit " + controller.lastHit.collider.gameObject.name);
			StartCoroutine(Waiter ());

		}
	
	}

	#endregion


} 