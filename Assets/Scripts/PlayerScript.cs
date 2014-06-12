using UnityEngine;
using System.Collections;

//player controller and behaviours

public class PlayerScript : MonoBehaviour {

	//player speed
	public Vector2 speed = new Vector2(20,20);
	//where player speed is stored
	private Vector2 movement;
	//player's XP and level
	//NOTE: public for testing purposes only
	public int xp = 0;
	public int level = 1;
	public int levelFactor = 10;

	// Update is called once per frame
	void Update () {
		//Horizontal and Vertical are defined in Unity w/
		//specific key bindings
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		//movement in each direction
		movement = new Vector2 (speed.x * inputX, speed.y * inputY);
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
