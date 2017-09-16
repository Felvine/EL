using UnityEngine;
using Actions;
using System;

class GammaController : ActionBasedController {
    public const float playerWalkSpeed = 6.0f;
    public const float playerRunSpeed = 10.0f;
    public const float playerRollDuration = 1.4f;
    public const float playerRollLength = 7.5f;
    public const float playerAttack1Duration = 1.7f;

    protected override void Start () {
        base.Start ();
        GetComponentInChildren<WeaponBehaviour> ().User = this.User;
        this.User.AddAction ("Walk", new MoveWithSpeed (this.User, actionMinimumStep, this.User.Animaton.GetClip("Player_Walk"), playerWalkSpeed));
        this.User.AddAction ("Run", new MoveWithSpeed (this.User, actionMinimumStep, this.User.Animaton.GetClip ("Player_Run"), playerRunSpeed));
        this.User.AddAction ("Roll", new MoveToDistance (this.User, playerRollDuration, this.User.Animaton.GetClip("Player_Roll"),playerRollLength, false));
        this.User.AddAction ("Idle", new Idle (this.User, actionMinimumStep, this.User.Animaton.GetClip ("Player_Idle")));
        this.User.AddAction ("Attack1", new Attack (this.User, playerAttack1Duration, this.User.Animaton.GetClip ("Player_Attack_1")));
    }

    protected override ICharacterAction DetermineAction () {
        return DetermineActionFromInputs ();
    }

    private ICharacterAction DetermineActionFromInputs () {
        Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        moveDirection.Normalize ();
        if (CurrentAction == null || ((CharacterAction)CurrentAction).Priority == 0)
            this.User.Direction = moveDirection;
        if (moveDirection != Vector3.zero) {
            if (Input.GetKeyDown (KeyCode.Space))
                return this.User.GetAction ("Roll");
            else if (Input.GetKey (KeyCode.LeftShift))
                return this.User.GetAction ("Run");
            else
                return this.User.GetAction ("Walk");
        } else {
            if (Input.GetKeyDown (KeyCode.E))
                return this.User.GetAction ("Attack1");
        }
        return this.User.GetAction ("Idle");
    }
}

