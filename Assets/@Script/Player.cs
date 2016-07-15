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

	public ParticleSystem deathParticle;

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	[HideInInspector] 
	public float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	float velocityXSmoothing;
	Vector3 velocity;

	Vector2 input;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	Vector3 lerpTarget;
	public Transform otherHorizontal;
	public Transform otherVertical;
	public bool pushing = false;
	public float distance = 0;
	private float lerpVelocity = 0;
	private float inittial;
	private float final;


	public string curState;

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
		
	void PlayParticles() {

		if (curState != "Dead") {
			Color playerColor = GetComponent<SpriteRenderer> ().color;
			deathParticle.startColor = new Color(playerColor[0], playerColor[1], playerColor[2], playerColor[3]);
			Instantiate(deathParticle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			gameObject.GetComponent<Controller2D>().enabled = false;
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void Walk(){
		if (isKeyboard) {
			input = new Vector2 (KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller), KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller));
		
		} else {
			input = new Vector2 (XCI.GetAxisRaw (XboxAxis.LeftStickX, Xcontroller), XCI.GetAxis (XboxAxis.LeftStickY, Xcontroller));
		}

	}

	void Jump(){
		int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

		bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0 && controller.horizontalLastHit.collider.tag != "Player" && controller.horizontalLastHit.collider.tag != "Slideless")
        {
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
	}

	void JumpOnEnemy(){

		velocity.x = velocity.x * 1.5f;

		velocity.y = maxJumpVelocity / 1.23f;

		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);
		
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
	}

	void Punch() {
		if (((XCI.GetButtonDown (XboxButton.X, Xcontroller) && !isKeyboard) || (KCI.GetButtonDown (KeyboardButton.Action, Kcontroller) && isKeyboard)) && controller.interPlayersCollision) {
			pushing = true;
			lerpVelocity = 0f;
			otherHorizontal = controller.horizontalLastHit.transform;
			inittial = otherHorizontal.position.x;
			distance = 1.5f;

			if (transform.position.x > inittial) {
				if (otherHorizontal.GetComponent<Controller2D> ().distanceWallLeft - otherHorizontal.GetComponent<Renderer>().bounds.size.x/2 < distance) {
					distance = otherHorizontal.GetComponent<Controller2D> ().distanceWallLeft - otherHorizontal.GetComponent<Renderer>().bounds.size.x/2;
				}
				if (otherHorizontal.GetComponent<Controller2D> ().distanceWallLeft <= otherHorizontal.GetComponent<Renderer>().bounds.size.x/2 + 0.1f) {
					distance = 0f;
				}
				distance *= -1;
			} else {
				if (otherHorizontal.GetComponent<Controller2D> ().distanceWallRight - otherHorizontal.GetComponent<Renderer>().bounds.size.x/2 < distance) {
					distance = otherHorizontal.GetComponent<Controller2D> ().distanceWallRight - otherHorizontal.GetComponent<Renderer>().bounds.size.x/2;
				}
				if (otherHorizontal.GetComponent<Controller2D> ().distanceWallRight <= otherHorizontal.GetComponent<Renderer>().bounds.size.x/2 + 0.1f) {
					distance = 0f;
				}
				distance *= 1;
			}

			final = inittial + distance;
				
			lerpTarget = new Vector3 (final, otherHorizontal.position.y, otherHorizontal.position.z);
		}

		if (pushing) {
			lerpVelocity += Time.deltaTime * 5f;
			if (lerpVelocity > 1f) {
				lerpVelocity = 1f;
			}

			float perc = lerpVelocity / 1f;

			if (distance >= 0f) {
				if (otherHorizontal.position.x >= final || perc == 1f) {
					pushing = false;
					distance = 0;
				}
			} else {
				if (otherHorizontal.position.x <= final || perc == 1f) {
					pushing = false;
					distance = 0;
				}
			}
			
			otherHorizontal.position = Vector3.Lerp (otherHorizontal.position, lerpTarget, perc);
		}
	}

	void Launching(){
		if (isKeyboard) {
			if (KCI.GetButtonDown (KeyboardButton.Launch, Kcontroller)) {
				if (controller.collisions.below && curState != "Preparation") {
					curState = "Preparation";
				}
			} 
			if (KCI.GetButtonUp (KeyboardButton.Launch, Kcontroller)) {
				if (curState == "Preparation") {
					curState = "Still";
				}
			}
		} else {
			if (XCI.GetButtonDown (XboxButton.B, Xcontroller)) {
				if (controller.collisions.below && curState != "Preparation") {
					curState = "Preparation";
				}
			} 
			if (XCI.GetButtonUp (XboxButton.B, Xcontroller)) {
				if (curState == "Preparation") {
					curState = "Still";
				
				}
			}
		}
	}

	void Death(){

		otherVertical = controller.verticalLastHit.transform;
		if (controller.collisions.below && otherVertical.gameObject.tag == "Player" && otherVertical.GetComponent<Player> ().curState != "Dead") {
			otherVertical.gameObject.GetComponent<Player> ().PlayParticles();
			otherVertical.gameObject.GetComponent<Player> ().curState = "Dead";
            Destroy(otherVertical.gameObject);
			JumpOnEnemy();
		}

	}

    public void Limits(float top, float left, float right) {

        otherVertical = controller.verticalLastHit.transform;
        otherHorizontal = controller.horizontalLastHit.transform;

        if (controller.collisions.below && otherVertical.gameObject.tag == "Bottom") { transform.position = new Vector2(transform.position.x, top); }
        if (controller.collisions.left && otherHorizontal.gameObject.tag == "Left") { transform.position = new Vector2(right, transform.position.y); }
        if (controller.collisions.right && otherHorizontal.gameObject.tag == "Right") { transform.position = new Vector2(left, transform.position.y); }


    }

	void Update() {

		if (curState != "Dead") {
			Launching ();
			if (curState != "Preparation") {
				Walk ();
				Jump ();
				Punch ();
			}
        }

		//Testing death stuff
        Limits(8, -8.4f, 8.4f);
		Death ();

	}
	#endregion
} 