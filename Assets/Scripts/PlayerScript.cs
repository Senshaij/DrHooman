using UnityEngine;
using System.Collections;

//player controller and behaviours

public class PlayerScript : MonoBehaviour {

	//player speed
	public float maxSpeed = 20.0F;
	public float moveForce = 200.0F;
	public float jumpForce = 1000.0F;
	private bool grounded = false;
	private bool jump = false;
	private Transform groundCheck;
	//player's XP and level
	//NOTE: public for testing purposes only
	public int xp = 0;
	public int level = 1;
	public int levelFactor = 10;

	void Awake() {
		//set up references
		groundCheck = transform.Find ("groundCheck");
		//TODO: animator will go here later
	}

	// Update is called once per frame
	void Update () {
		/*
		 * 3D-based movement
		CharacterController controller = GetComponent<CharacterController> ();

		if (controller.isGrounded) {
			movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			movement = transform.TransformDirection(movement);
			movement *= speed;

			if (Input.GetButton("Jump")) {
				movement.y = jumpSpeed;
			}
		}

		movement.y -= gravity * Time.deltaTime;
		controller.Move (movement * Time.deltaTime);
		*/

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");

		if (h * rigidbody2D.velocity.x < maxSpeed) {
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		}

		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

		//TODO: add flipping to player to face the right direction

		if (jump) {
			//add a vertical force
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	public void GainXP (int xpGained) {
		xp += xpGained;

		if (xp >= (level * levelFactor)) {
			level += 1;
			xp = 0;
		}
	}

	//did player pick up a component?
	void OnTriggerEnter2D (Collider2D collider) {
		ComponentScript comp = collider.gameObject.GetComponent<ComponentScript> ();
		
		if (comp != null) {
			GainXP (comp.xpGrant);
			Destroy (comp.gameObject);
		}
	}
}
