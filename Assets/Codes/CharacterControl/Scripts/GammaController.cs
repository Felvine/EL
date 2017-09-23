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
        this.User.AddAction ("Walk", new MoveWithSpeed (this.User, actionMinimumStep, this.User.Animation.GetClip("Player_Walk"), playerWalkSpeed));

        this.User.AddAction ("Run", new MoveWithSpeed (this.User, actionMinimumStep, this.User.Animation.GetClip ("Player_Run"), playerRunSpeed));

        this.User.AddAction ("Roll", new MoveToDistance (this.User, this.User.Animation.GetClip ("Player_Roll").length, this.User.Animation.GetClip("Player_Roll"),playerRollLength, false));

        this.User.AddAction ("Idle", new Idle (this.User, actionMinimumStep, this.User.Animation.GetClip ("Player_Idle")));

        ComboAction attack1ending = new ComboAction (new Idle (this.User, this.User.Animation.GetClip ("Player_Attack_Ender").length, this.User.Animation.GetClip ("Player_Attack_Ender")),
                                                     new Attack (this.User, this.User.Animation.GetClip ("Player_Attack_3").length, this.User.Animation.GetClip ("Player_Attack_3")),
                                                     new Attack (this.User, this.User.Animation.GetClip ("Player_Attack_2").length, this.User.Animation.GetClip ("Player_Attack_2")));
                                                     
        this.User.AddAction ("ComboAttack", new CharacterActionSequence (this.User, null,
                                                        new Attack (this.User, this.User.Animation.GetClip ("Player_Attack_1").length, this.User.Animation.GetClip ("Player_Attack_1")),
                                                        attack1ending));

        float attack4duration = this.User.Animation.GetClip ("Player_Attack_4").length;
        this.User.AddAction ("Attack4", new CharacterActionSequence (this.User, this.User.Animation.GetClip ("Player_Attack_4"),
                                                        new Idle (this.User, attack4duration / 3, null),
                                                        new Attack (this.User, attack4duration / 3, null),
                                                        new Idle (this.User, attack4duration / 3, null)));
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
                return this.User.GetAction ("ComboAttack");
            }
            if (Input.GetKeyDown (KeyCode.R))
                return this.User.GetAction ("Attack4");
        }
        return this.User.GetAction ("Idle");
    }

    protected override void BranchComboAttacks () {
        CharacterActionSequence currentActionSequence = (CurrentAction as CharacterActionSequence);
        if (currentActionSequence != null) {
            if (currentActionSequence.Actions[currentActionSequence.Step].IsFinishing () && (currentActionSequence.Step + 1) < currentActionSequence.Actions.Count) {
                ComboAction nextComboAction = (currentActionSequence.Actions[currentActionSequence.Step + 1] as ComboAction);
                if (nextComboAction != null) {
                    if (Input.GetKeyDown (KeyCode.E))
                        nextComboAction.Selected = 1;
                    else if (Input.GetKeyDown (KeyCode.R))
                        nextComboAction.Selected = 2;
                }
            }
        }
    }
}

