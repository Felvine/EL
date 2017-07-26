using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalk : MonoBehaviour {

	public Animator animator;
	public float speed = 10f;


	void Update () {

		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			animator.SetBool ("PlayerWalking", true);

			if (Input.GetAxis ("Horizontal") < 0) {
				transform.rotation = Quaternion.Euler (0, 180, 0);
				transform.Translate (Vector3.right * Time.deltaTime * speed);

			}

			if (Input.GetAxis ("Horizontal") > 0) {
				transform.Translate (Vector3.right * Time.deltaTime * speed);
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}


			if (Input.GetAxis ("Vertical") > 0) {
				transform.Translate (Vector3.up * Time.deltaTime * speed);
			}

			if (Input.GetAxis ("Vertical") < 0) {
				transform.Translate (Vector3.down * Time.deltaTime * speed);
			}

			if (Input.GetAxis ("Horizontal") < 0 && Input.GetKeyDown (KeyCode.LeftShift)) {
				transform.rotation = Quaternion.Euler (0, 180, 0);
				transform.Translate (Vector3.right * Time.deltaTime * speed);
				animator.SetBool ("PlayerRunning", true);
				speed = 5f;

			}

			if  (Input.GetAxis ("Horizontal") > 0 && Input.GetKeyDown (KeyCode.LeftShift)) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
				transform.Translate (Vector3.right * Time.deltaTime * speed);
				animator.SetBool ("PlayerRunning", true);
				speed = 5f;
			}

			if (Input.GetAxis ("Vertical") > 0 && Input.GetKeyDown (KeyCode.LeftShift)) {
				transform.Translate (Vector3.up * Time.deltaTime * speed);
				animator.SetBool ("PlayerRunning", true);
				speed = 5f;
			}

			if (Input.GetAxis ("Vertical") < 0 && Input.GetKeyDown (KeyCode.LeftShift)) {
				transform.Translate (Vector3.down * Time.deltaTime * speed);
				animator.SetBool ("PlayerRunning", true);
				speed = 5f;
			}
				


		}

		else {
			animator.SetBool ("PlayerWalking", false);
			animator.SetBool ("PlayerRunning", false);
			}




			
		
		
		if (Input.GetButton ("Fire1")) 
			{
			animator.SetTrigger ("PlayerAttack");
			}



			}

		}
	




