using UnityEngine;
using System.Collections;

//player controller and behaviours

public class PlayerScript : MonoBehaviour {

	//player speed
	public float speed = 20.0F;
	public float jumpSpeed = 10.0F;
	public float gravity = 20.0F;
	//where player speed is stored
	private Vector2 movement = Vector2.zero;
	//player's XP and level
	//NOTE: public for testing purposes only
	public int xp = 0;
	public int level = 1;
	public int levelFactor = 10;

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


		//Horizontal and Jump are defined in Unity w/
		//specific key bindings
		movement = new Vector2 (Input.GetAxis ("Horizontal"), 0);
		movement *= speed;

		if (rigidbody2D.velocity.y == 0.0) {
			if (Input.GetButton ("Jump")) {
				movement.y = jumpSpeed;
			}
		}

		//movement.y -= gravity * Time.deltaTime;
		//movement in each direction
		//movement = new Vector2 (speed.x * inputX, speed.y * inputY);
	}

	void FixedUpdate () {
		//move the game object
		rigidbody2D.velocity = movement;
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
