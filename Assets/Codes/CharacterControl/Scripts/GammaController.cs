using UnityEngine;
using Actions;
using System;

class GammaController : ActionBasedController {
    public const float playerWalkSpeed = 6.0f;
    public const float playerRunSpeed = 10.0f;
    public const float playerRollDuration = 1.4f;
    public const float playerRollLength = 7.5f;
    public const float playerAttack1Duration = 1.7f;
    public const float playerAttack2Duration = 1.7f;
    public const float playerAttack4Duration = 1f;

    protected override void Start () {
        base.Start ();
        GetComponentInChildren<WeaponBehaviour> ().User = this.User;
        this.User.AddAction ("Walk", new MoveWithSpeed (this.User, actionMinimumStep, this.User.Animaton.GetClip("Player_Walk"), playerWalkSpeed));
        this.User.AddAction ("Run", new MoveWithSpeed (this.User, actionMinimumStep, this.User.Animaton.GetClip ("Player_Run"), playerRunSpeed));
        this.User.AddAction ("Roll", new MoveToDistance (this.User, playerRollDuration, this.User.Animaton.GetClip("Player_Roll"),playerRollLength, false));
        this.User.AddAction ("Idle", new Idle (this.User, actionMinimumStep, this.User.Animaton.GetClip ("Player_Idle")));
        this.User.AddAction ("Attack1", new Attack (this.User, playerAttack1Duration, this.User.Animaton.GetClip ("Player_Attack_1")));
        this.User.AddAction ("Attack2", new Attack (this.User, playerAttack2Duration, this.User.Animaton.GetClip ("Player_Attack_2")));
        this.User.AddAction ("Attack4", new CharacterActionSequence (this.User, this.User.Animaton.GetClip ("Player_Attack_4"),
                                                        new Idle (this.User, playerAttack4Duration / 3, null),
                                                        new Attack (this.User, playerAttack4Duration / 3, null),
                                                        new Idle (this.User, playerAttack4Duration / 3, null)));
    }

    protected override ICharacterAction DetermineAction () {
        return DetermineActionFromInputs ();
    }

    private ICharacterAction DetermineActionFromInputs () {
        Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        moveDirection.Normalize ();
        if (CurrentAction == null || CurrentAction.Priority == 0)
            this.User.Direction = moveDirection;
        if (moveDirection != Vector3.zero) {
            if (Input.GetKeyDown (KeyCode.Space))
                return this.User.GetAction ("Roll");
            else if (Input.GetKey (KeyCode.LeftShift))
                return this.User.GetAction ("Run");
            else
                return this.User.GetAction ("Walk");
        } else {
            if (Input.GetKeyDown (KeyCode.E)) {
                if (CurrentAction == this.User.GetAction ("Attack1")) {
                    Debug.Log ("C-C-COMBO");
                    return this.User.GetAction ("Attack2");
                } else
                    return this.User.GetAction ("Attack1");
            }
            if (Input.GetKeyDown (KeyCode.R))
                return this.User.GetAction ("Attack4");
        }
        return this.User.GetAction ("Idle");
    }
}

