using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public bool IsMoving {
        get {
            return moveDirection.Equals (Vector3.zero);
        }
    }

    public bool IsRunning {
        get {
            return false;   //TODO
        }
    }
    void Update () {
        ControlPlayer (GetComponent<CharacterController> ());
        SetAnimatorVariables (GetComponent<Animator> ());

    }
    void ControlPlayer (CharacterController controller) {
        if (controller.isGrounded) {
            moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
            moveDirection = transform.TransformDirection (moveDirection);
            moveDirection *= speed;
            if (Input.GetButton ("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move (moveDirection * Time.deltaTime);
    }

    void SetAnimatorVariables (Animator animator) {
        animator.SetBool ("IsMoving", IsMoving);
    }
}